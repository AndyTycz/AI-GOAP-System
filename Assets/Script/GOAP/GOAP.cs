using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GOAP
{
    public static IEnumerable<GOAPAction> RunGOAP(GOAPEntity entity, GOAPState from, IEnumerable<GOAPAction> actions, int watchdog = 6000)
    {
        int watchdogCount = 0;

        var sequence = Algorithms.AStarNew<GOAPState>(
            from,
            current => entity.Satisfies(current),
            (current, toCompare) => current.Equals(toCompare),
            current => entity.Heuristics(current),
            current =>
                {
                    return actions
                        .Where(a => a.Preconditions(current))
                        .Aggregate(
                            new FList<Tuple<GOAPState, float>>(),
                            (possibleList, a) =>
                            {
                                if (watchdogCount < watchdog)
                                {
                                    var st = new GOAPState(current);
                                    st.generatingAction = a;
                                    a.Effects(current, st);
                                    st.stepId = current.stepId + 1;
                                    watchdogCount++;
                                    return possibleList + Tuple.Create(st, a.Cost);
                                }
                                else return possibleList;
                            }
                        );
                }
            );



        if (sequence == null) return null;

        Debug.Log("Watchdog: " + watchdogCount);
        return sequence.Skip(1).Select(x => { Debug.Log(x.Item1.generatingAction.Name); return x.Item1.generatingAction; });
    }

    public static IEnumerable<GOAPAction> RunGOAPOld(GOAPEntity entity, GOAPState from, IEnumerable<GOAPAction> actions, int watchdog = 6000)
    {
        int watchdogCount = watchdog;

        var sequence = Algorithms.AStar<GOAPState>(
            from,
            current => entity.Satisfies(current),
            current => entity.Heuristics(current),
            current =>
            {
                var arcs = new List<Arc<GOAPState>>();
                if (watchdogCount == 0)
                {
                    Debug.Log("Out of iterations");
                    return arcs;
                }
                else watchdogCount--;

                foreach (GOAPAction act in actions)
                {
                    if (act.Preconditions(current))
                    {
                        var st = new GOAPState(current);
                        st.generatingAction = act;
                        act.Effects(current, st);
                        st.stepId = current.stepId + 1;
                        arcs.Add(new Arc<GOAPState>().SetArc(st, act.Cost));
                    }
                }
                return arcs;
            }
            );
        
        if (sequence == null) return null;

        foreach (var step in sequence)
        {
            Debug.Log(step.generatingAction.Name);
        }
        Debug.Log("Watchdog: " + watchdogCount);

        return sequence.Select(x => x.generatingAction);
    }
}