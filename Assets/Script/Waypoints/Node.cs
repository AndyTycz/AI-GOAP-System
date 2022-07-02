using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    protected bool walkable = true;
    protected Color color = new Color(1f, 1f, 0f);
    public Node[] neighbors;

    void Awake()
    {
        NodeList.Register(this);
    }

 
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, 2f);
        for (int i = 0; i < neighbors.Length; i++)
        {
            Gizmos.DrawLine(transform.position, neighbors[i].transform.position);
        }
    }
}

public static class NodeList
{
    public static List<Node> allNodes;

    public static void Register(Node node)
    {
        if (allNodes == null) allNodes = new List<Node>();
        allNodes.Add(node);
    }
}
