using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode : Node
{
    private void Awake()
    {
        NodeList.Register(this);
        color = Color.green;
        walkable = false;
    }
}
	 
