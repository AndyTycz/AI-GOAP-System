using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_OpenHouseAction", menuName = "Scriptable Objects/GOAPActions/OpenHouse")]
public class SO_OpenHouseAction : ScriptableObject
{
    [Header("Cost")]
    public float cost = .5f;

    [Header("Preconditions")]
    public string precondition_houseKey = "House 1";

    [Header("Effects")]
    public bool effect_HouseOwned = true;
}
