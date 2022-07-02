using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActionRoutines
{
    public static IEnumerator BuildHouseCoroutine(GOAPEntity ent)
    {
        ent.currentStateText.text = "Build House";
        Node startingPoint = NodeList.allNodes.OrderBy(a => { return Vector3.Distance(a.transform.position, ent.transform.position); }).First();
        HouseNode target = NodeList.allNodes
            .Select(a => { return a.GetComponent<HouseNode>(); })
            .Where(a => a != null && !a.owned && !a.constructed)
            .OrderBy(a => { return Vector3.Distance(a.transform.position, ent.transform.position); })
            .First();


        var path = Algorithms.AStar(
            startingPoint,
            a => a == target,
            a => Vector3.Distance(target.transform.position, a.transform.position),
            a =>
            {
                return a.neighbors.Where(b  => b.gameObject.activeSelf).Select(n => new Arc<Node>().SetArc(n, Vector3.Distance(n.transform.position, a.transform.position)));
            }
            );

        if(path != null)
        while (path.Count > 0)
        {

            ent.transform.forward = new Vector3(path.Peek().transform.position.x - ent.transform.position.x, ent.transform.forward.y, path.Peek().transform.position.z - ent.transform.position.z);
            ent.transform.position += ent.transform.forward * Mathf.Min(Vector3.Distance(path.Peek().transform.position, ent.transform.position), ent.speed * Time.deltaTime);
            if (Vector3.Distance(path.Peek().transform.position, ent.transform.position) <= 2f) path.Pop();
            yield return new WaitForEndOfFrame();
        }
        target.constructed = true;
        target.owned = true;

        ent.Sequence.First().Effects(ent.Current, ent.Current);
        ent.Sequence = ent.Sequence.Skip(1);
        if (ent.Sequence.Count() > 0) ent.Sequence.First().Act(ent);
        else Debug.Log("SATISFIED! :)");
    }

    public static IEnumerator GatherWoodCoroutine(GOAPEntity ent)
    {
        ent.currentStateText.text = "Gather Wood";
        Node startingPoint = NodeList.allNodes.OrderBy(a => { return Vector3.Distance(a.transform.position, ent.transform.position); }).First();
        TreeNode target = NodeList.allNodes
            .Select(a => { return a.GetComponent<TreeNode>(); })
            .Where(a => a != null && a.gameObject.activeSelf)
            .OrderBy(a => { return Vector3.Distance(a.transform.position, ent.transform.position); })
            .First();


        var path = Algorithms.AStar(
            startingPoint,
            a => a == target,
            a => Vector3.Distance(target.transform.position, a.transform.position),
            a =>
            {
                return a.neighbors.Where(b  => b.gameObject.activeSelf).Select(n => new Arc<Node>().SetArc(n, Vector3.Distance(n.transform.position, a.transform.position)));
            }
            );

        if(path != null)
        while (path.Count > 0)
        {

            ent.transform.forward = new Vector3(path.Peek().transform.position.x - ent.transform.position.x, ent.transform.forward.y, path.Peek().transform.position.z - ent.transform.position.z);
            ent.transform.position += ent.transform.forward * Mathf.Min(Vector3.Distance(path.Peek().transform.position, ent.transform.position), ent.speed * Time.deltaTime);
            if (Vector3.Distance(path.Peek().transform.position, ent.transform.position) <= 2f) path.Pop();
            yield return new WaitForEndOfFrame();
        }

        target.gameObject.SetActive(false);

        ent.Sequence.First().Effects(ent.Current, ent.Current);
        ent.Sequence = ent.Sequence.Skip(1);
        if (ent.Sequence.Count() > 0) ent.Sequence.First().Act(ent);
        else Debug.Log("SATISFIED! :)");
    }

    public static IEnumerator GetToolCoroutine(GOAPEntity ent, ToolType type)
    {
        ent.currentStateText.text = "Pick " + type;
        Node startingPoint = NodeList.allNodes.OrderBy(a => { return Vector3.Distance(a.transform.position, ent.transform.position); }).First();
        ToolNode target = NodeList.allNodes
            .Select(a => { return a.GetComponent<ToolNode>(); })
            .Where(a => a != null && a.gameObject.activeSelf && a.type == type)
            .OrderBy(a => { return Vector3.Distance(a.transform.position, ent.transform.position); })
            .First();

        var path = Algorithms.AStar(
            startingPoint,
            a => a == target,
            a => Vector3.Distance(target.transform.position, a.transform.position),
            a =>
            {
                
                return a.neighbors.Where(b  => b.gameObject.activeSelf).Select(n => new Arc<Node>().SetArc(n, Vector3.Distance(n.transform.position, a.transform.position)));
            }
            );

        

        if(path != null)
        while (path.Count > 0)
        {

            ent.transform.forward = new Vector3(path.Peek().transform.position.x - ent.transform.position.x, ent.transform.forward.y, path.Peek().transform.position.z - ent.transform.position.z);
            ent.transform.position += ent.transform.forward * Mathf.Min(Vector3.Distance(path.Peek().transform.position, ent.transform.position), ent.speed * Time.deltaTime);
            if (Vector3.Distance(path.Peek().transform.position, ent.transform.position) <= 2f) path.Pop();
            yield return new WaitForEndOfFrame();
        }
        target.transform.GetChild(0).parent = ent.transform;
        target.gameObject.SetActive(false);

        ent.Sequence.First().Effects(ent.Current, ent.Current);
        ent.Sequence = ent.Sequence.Skip(1);
        if (ent.Sequence.Count() > 0) ent.Sequence.First().Act(ent);
        else Debug.Log("SATISFIED! :)");
    }

    public static IEnumerator SleepCoroutine(GOAPEntity ent)
    {
        ent.currentStateText.text = "Sleep";
        Node startingPoint = NodeList.allNodes.OrderBy(a => { return Vector3.Distance(a.transform.position, ent.transform.position); }).First();
        BedNode target = NodeList.allNodes
            .Select(a => { return a.GetComponent<BedNode>(); })
            .Where(a => a != null)
            .OrderBy(a => { return Vector3.Distance(a.transform.position, ent.transform.position); })
            .First();


        var path = Algorithms.AStar(
            startingPoint,
            a => a == target,
            a => Vector3.Distance(target.transform.position, a.transform.position),
            a =>
            {
                return a.neighbors.Where(b  => b.gameObject.activeSelf).Select(n => new Arc<Node>().SetArc(n, Vector3.Distance(n.transform.position, a.transform.position)));
            }
            );

        if(path != null)
        while (path.Count > 0)
        {

            ent.transform.forward = new Vector3(path.Peek().transform.position.x - ent.transform.position.x, ent.transform.forward.y, path.Peek().transform.position.z - ent.transform.position.z);
            ent.transform.position += ent.transform.forward * Mathf.Min(Vector3.Distance(path.Peek().transform.position, ent.transform.position), ent.speed * Time.deltaTime);
            if (Vector3.Distance(path.Peek().transform.position, ent.transform.position) <= 2f) path.Pop();
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(3f);

        ent.Sequence.First().Effects(ent.Current, ent.Current);
        ent.Sequence = ent.Sequence.Skip(1);
        if (ent.Sequence.Count() > 0) ent.Sequence.First().Act(ent);
        else Debug.Log("SATISFIED! :)");
    }

    public static IEnumerator BuildBed(GOAPEntity ent)
    {
        ent.currentStateText.text = "Build Bed";
        var bed = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bed.transform.position = new Vector3(
            ent.transform.position.x + Random.Range(-2f, 2f),
            0,
            ent.transform.position.z + Random.Range(-2f, 2f)
            );
        bed.GetComponent<Collider>().isTrigger = true;
        bed.AddComponent<BedNode>();
        bed.GetComponent<BedNode>().neighbors = new Node[] { };

        var neighbors = NodeList.allNodes
            .Where(a => Vector3.Distance(a.transform.position, bed.transform.position) < 15f && a != bed);
        foreach (var n in neighbors)
        {
            n.neighbors = n.neighbors.Concat(new[] { bed.GetComponent<BedNode>() }).ToArray();
        }

        yield return new WaitForSeconds(1f);

        bed.GetComponent<BedNode>().neighbors = neighbors.ToArray();

        ent.Sequence.First().Effects(ent.Current, ent.Current);
        ent.Sequence = ent.Sequence.Skip(1);
        if (ent.Sequence.Count() > 0) ent.Sequence.First().Act(ent);
        else Debug.Log("SATISFIED!!!!!!!!!!!!!!");
    }

    public static IEnumerator ActivateCheatsCoroutine(GOAPEntity ent)
    {
        ent.currentStateText.text = "COWARD! (Using cheats)";
        Node startingPoint = NodeList.allNodes.OrderBy(a => { return Vector3.Distance(a.transform.position, ent.transform.position); }).First();
        HouseNode target = NodeList.allNodes
            .Select(a => { return a.GetComponent<HouseNode>(); })
            .Where(a => a != null && !a.owned && !a.constructed)
            .OrderBy(a => { return Vector3.Distance(a.transform.position, ent.transform.position); })
            .First();


        var path = Algorithms.AStar(
            startingPoint,
            a => a == target,
            a => Vector3.Distance(target.transform.position, a.transform.position),
            a =>
            {
                return a.neighbors.Where(b  => b.gameObject.activeSelf).Select(n => new Arc<Node>().SetArc(n, Vector3.Distance(n.transform.position, a.transform.position)));
            }
            );

        while (path.Count > 0)
        {

            ent.transform.forward = new Vector3(path.Peek().transform.position.x - ent.transform.position.x, ent.transform.forward.y, path.Peek().transform.position.z - ent.transform.position.z);
            ent.transform.position += ent.transform.forward * Mathf.Min(Vector3.Distance(path.Peek().transform.position, ent.transform.position), ent.speed * Time.deltaTime);
            if (Vector3.Distance(path.Peek().transform.position, ent.transform.position) <= 2f) path.Pop();
            yield return new WaitForEndOfFrame();
        }
        var cheatVisual = ent.transform.GetChild(0).gameObject;
        cheatVisual.SetActive(true);
        target.constructed = true;
        target.owned = true;

        Debug.Log("SATISFIED BY USING CHEATS! (my game my rules)");

        while (true)
        {
            cheatVisual.transform.eulerAngles += cheatVisual.transform.forward * 50f * Time.deltaTime;
            cheatVisual.transform.localScale += (Vector3.one * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        ent.Sequence.First().Effects(ent.Current, ent.Current);
        ent.Sequence = ent.Sequence.Skip(1);
        if (ent.Sequence.Count() > 0) ent.Sequence.First().Act(ent);
    }

    public static IEnumerator KillEnemyCoroutine(GOAPEntity ent)
    {
        ent.currentStateText.text = "Kill Enemy";
        Node startingPoint = NodeList.allNodes.OrderBy(a => { return Vector3.Distance(a.transform.position, ent.transform.position); }).First();

        var houses = NodeList.allNodes
            .Select(a => { return a.GetComponent<HouseNode>(); })
            .Where(a => a != null && a.owned && a.constructed)
            .Select(a => a.keyId);

        EnemyWaypoint target = NodeList.allNodes
            .Select(a => { return a.GetComponent<EnemyWaypoint>(); })
            .Where(a => a != null && a.gameObject.activeSelf && houses.Contains(a.keyId))
            .OrderBy(a => { return Vector3.Distance(a.transform.position, ent.transform.position); })
            .First();


        var path = Algorithms.AStar(
            startingPoint,
            a => a == target,
            a => Vector3.Distance(target.transform.position, a.transform.position),
            a =>
            {
                return a.neighbors.Where(b  => b.gameObject.activeSelf).Select(n => new Arc<Node>().SetArc(n, Vector3.Distance(n.transform.position, a.transform.position)));
            }
            );

        while (path.Count > 0)
        {

            ent.transform.forward = new Vector3(path.Peek().transform.position.x - ent.transform.position.x, ent.transform.forward.y, path.Peek().transform.position.z - ent.transform.position.z);
            ent.transform.position += ent.transform.forward * Mathf.Min(Vector3.Distance(path.Peek().transform.position, ent.transform.position), ent.speed * Time.deltaTime);
            if (Vector3.Distance(path.Peek().transform.position, ent.transform.position) <= 2f) path.Pop();
            yield return new WaitForEndOfFrame();
        }

        ent.Current.Keys.Add(target.keyId);
        target.gameObject.SetActive(false);

        ent.Sequence.First().Effects(ent.Current, ent.Current);
        ent.Sequence = ent.Sequence.Skip(1);
        if (ent.Sequence.Count() > 0) ent.Sequence.First().Act(ent);
        else Debug.Log("SATISFIED!!!!!!!!!!!!!!");
    }

    public static IEnumerator OpenHouseCoroutine(GOAPEntity ent)
    {
        ent.currentStateText.text = "Open House";
        Node startingPoint = NodeList.allNodes.OrderBy(a => { return Vector3.Distance(a.transform.position, ent.transform.position); }).First();
        HouseNode target = NodeList.allNodes
            .Select(a => { return a.GetComponent<HouseNode>(); })
            .Where(a => a != null && ent.Current.Keys.Contains(a.keyId))
            .OrderBy(a => { return Vector3.Distance(a.transform.position, ent.transform.position); })
            .First();


        var path = Algorithms.AStar(
            startingPoint,
            a => a == target,
            a => Vector3.Distance(target.transform.position, a.transform.position),
            a =>
            {
                return a.neighbors.Where(b  => b.gameObject.activeSelf).Select(n => new Arc<Node>().SetArc(n, Vector3.Distance(n.transform.position, a.transform.position)));
            }
            );

        while (path.Count > 0)
        {

            ent.transform.forward = new Vector3(path.Peek().transform.position.x - ent.transform.position.x, ent.transform.forward.y, path.Peek().transform.position.z - ent.transform.position.z);
            ent.transform.position += ent.transform.forward * Mathf.Min(Vector3.Distance(path.Peek().transform.position, ent.transform.position), ent.speed * Time.deltaTime);
            if (Vector3.Distance(path.Peek().transform.position, ent.transform.position) <= 2f) path.Pop();
            yield return new WaitForEndOfFrame();
        }
        target.constructed = true;
        target.owned = true;

        ent.Sequence.First().Effects(ent.Current, ent.Current);
        ent.Sequence = ent.Sequence.Skip(1);
        if (ent.Sequence.Count() > 0) ent.Sequence.First().Act(ent);
        else Debug.Log("SATISFIED!!!!!!!!!!!!!!");
    }
}