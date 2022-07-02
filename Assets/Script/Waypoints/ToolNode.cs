using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolNode : Node
{
    public ToolType type;

    private void Awake()
    {
        NodeList.Register(this);
        color = Color.blue;
    }
}

public enum ToolType
{
    HAMMER,
    AXE,
    KEY,
    PACTWITHTHEDEVIL
}

 