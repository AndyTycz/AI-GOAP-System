using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypoint : Node
{	 
    public string keyId;

    private void Awake()
    {
        NodeList.Register(this);
        color = Color.red;
        walkable = false;
    }
}
