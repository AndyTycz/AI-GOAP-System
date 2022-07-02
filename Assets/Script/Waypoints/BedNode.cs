using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedNode : Node
{
    private void Awake()
    {
        NodeList.Register(this);
        color = Color.cyan;
    }
	 
}
