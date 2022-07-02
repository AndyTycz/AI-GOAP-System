using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseNode : Node
{
    public bool constructed;
    public bool owned;
    public string keyId;

 
    void Awake()
    {
        NodeList.Register(this);
        color = Color.magenta;
    }

    private void Update()
    {
        if (constructed && !transform.GetChild(0).gameObject.activeSelf) transform.GetChild(0).gameObject.SetActive(true);
        else if (!constructed && transform.GetChild(0).gameObject.activeSelf) transform.GetChild(0).gameObject.SetActive(false);
    }
}
