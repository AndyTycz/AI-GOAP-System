using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Algorithms
{
    public static IEnumerable<T> DFS<T>(T start, Func<T, bool> process, Func<T, IEnumerable<T>> explode)
    {
        var result = new List<T>();
        var stack = new Stack<T>();
	
        stack.Push(start);

        while (stack.Count > 0)
        {
            var poped = stack.Pop();

            result.Add(poped);

            if (process(poped))
                return result;

            var toStack = explode(poped);

            foreach (var elem in toStack)
                stack.Push(elem);
        }

        return result;
    }

    public static IEnumerable<T> BFS<T>(T start, Func<T, bool> process, Func<T, IEnumerable<T>> explode)
    {
        var result = new List<T>() { start };
        var queue = new Queue<T>();

        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var dequeued = queue.Dequeue();

            if (process(dequeued))
                return result;

            var toEnqueue = explode(dequeued);

            foreach (var elem in toEnqueue)
            {
                result.Add(elem);
                queue.Enqueue(elem);
            }
        }
        return result;
    }

    public static Stack<T> AStar<T>(T start, Func<T, bool> isGoal, Func<T, float> heuristic, Func<T, IEnumerable<Arc<T>>> explode)
    {
        var queue = new PriorityQueue<T>();
        var distances = new Dictionary<T, float>();
        var parents = new Dictionary<T, T>();
        var visited = new List<T>();

        distances[start] = 0;
        queue.Enqueue(new Arc<T>().SetArc(start, 0));

        while (!queue.IsEmpty)
        {
            var poped = queue.Dequeue();
            visited.Add(poped.Element);

            if (isGoal(poped.Element)) return CreatePath(parents, poped.Element);

            var toEnqueue = explode(poped.Element);
            foreach (var item in toEnqueue)
            {
                var element = item.Element;
                var elementToPopedDistance = item.Weight;
                var startToElementDistance = distances.ContainsKey(element) ? distances[element] : float.MaxValue;
                var startToPopedDistance = distances[poped.Element];
                var newDist = startToPopedDistance + elementToPopedDistance;

                if (!visited.Contains(element) && startToElementDistance > newDist)
                {
                    ListHandling.UpdateDictionary(distances, element, newDist);
                    ListHandling.UpdateDictionary(parents, element, poped.Element);
                    queue.Enqueue(new Arc<T>().SetArc(element, newDist + heuristic(element)));
                }

                //foreach (T prev in visited)
                //{
                //    if(compare(prev, element))
                //    {
                //        ListHandling.UpdateDictionary(distances, element, newDist);
                //        ListHandling.UpdateDictionary(parents, element, poped.Element);
                //        queue.Enqueue(new Arc<T>().SetArc(element, newDist + heuristic(element)));
                //    }
                //}
            }
        }
        return null;
    }

    public static Stack<T> ThetaStar<T>(T start, Func<T, bool> isGoal, Func<T, float> heuristic, Func<T,T, bool> lineOfSight, Func<T, IEnumerable<Arc<T>>> explode)
    {
        var queue = new PriorityQueue<T>();
        var distances = new Dictionary<T, float>();
        var parents = new Dictionary<T, T>();
        var visited = new HashSet<T>();

        distances[start] = 0;
        queue.Enqueue(new Arc<T>().SetArc(start, 0));

        while (!queue.IsEmpty)
        {
            var poped = queue.Dequeue();
            visited.Add(poped.Element);

            if (isGoal(poped.Element)) return CreatePath(parents, poped.Element);

            var toEnqueue = explode(poped.Element);
            foreach (var item in toEnqueue)
            {
                var element = item.Element;
                if(parents.ContainsKey(poped.Element) && !lineOfSight(parents[poped.Element], element))
                {
                    var elementToParentDistance = item.Weight + poped.Weight;
                    var startToElementDistance = distances.ContainsKey(element) ? distances[element] : float.MaxValue;
                    var startToParentDistance = distances[parents[poped.Element]];
                    var newDist = startToParentDistance + elementToParentDistance;

                    if (!visited.Contains(element) && startToElementDistance > newDist)
                    {
                        ListHandling.UpdateDictionary(distances, element, newDist);
                        ListHandling.UpdateDictionary(parents, element, parents[poped.Element]);
                        queue.Enqueue(new Arc<T>().SetArc(element, newDist + heuristic(element)));
                    }
                }
                else
                {
                    var elementToPopedDistance = item.Weight;
                    var startToElementDistance = distances.ContainsKey(element) ? distances[element] : float.MaxValue;
                    var startToPopedDistance = distances[poped.Element];
                    var newDist = startToPopedDistance + elementToPopedDistance;

                    if (!visited.Contains(element) && startToElementDistance > newDist)
                    {
                        ListHandling.UpdateDictionary(distances, element, newDist);
                        ListHandling.UpdateDictionary(parents, element, poped.Element);
                        queue.Enqueue(new Arc<T>().SetArc(element, newDist + heuristic(element)));
                    }
                }
            }
        }
        return null;
    }

    static Stack<T> CreatePath<T> (Dictionary<T, T> parents, T goal)
    {
        var path = new Stack<T>();
        var current = goal;

        path.Push(goal);

        while (parents.ContainsKey(current) && parents.ContainsKey(parents[current]))
        {
            path.Push(parents[current]);
            current = parents[current];
        }

        return path;
    }

    //Stop! Linq time!
    public static IEnumerable<Tuple<T, IEnumerable<T>>> AStarNew<T>(
           T start
         , Func<T, bool> satisfies
         , Func<T, T, bool> visited
         , Func<T, float> heuristic
         , Func<T, IEnumerable<Tuple<T, float>>> expand
    )
    {
        var closed = new HashSet<T>();
        var open = new HashSet<T>();
        var parents = new Dictionary<T, T>();
        var gs = new Dictionary<T, float>();
        gs[start] = 0f;
        open.Add(start);
        while (open.Any())
        {
            var current = open.OrderBy(x => gs[x] + heuristic(x)).First();
            open.Remove(current);
            closed.Add(current);

            if (satisfies(current))
            {
                var path = Generate(current, n => parents[n])
                    .TakeWhile(n => parents.ContainsKey(n))
                    .Reverse();

                yield return Tuple.Create(current, path);
            }

            yield return Tuple.Create(current, default(IEnumerable<T>));
            var expandList = expand(current)
                .Where(x => !closed.Contains(x.Item1));
            foreach (var adj in expandList)
            {
                var altCost = gs[current] + adj.Item2;
                var adjNode = adj.Item1;
                if (!gs.ContainsKey(adjNode) || gs[adjNode] > altCost)
                {
                    gs[adjNode] = altCost;
                    parents[adjNode] = current;
                    open.Add(adjNode);
                }
            }
        }
    }

    public static IEnumerable<T> Generate<T>(T seed, Func<T, T> mutate)
    {
        var accum = seed;
        while (true)
        {
            yield return accum;
            accum = mutate(accum);
        }
    }
}
