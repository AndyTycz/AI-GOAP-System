using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_BuildHouseAction", menuName = "Scriptable Objects/GOAPActions/BuildHouse")]
public class SO_BuildHouseAction : ScriptableObject
{
    [Header("Cost")]
    public float cost = 5f;

    [Header("Preconditions")]
    public bool precondition_HasHammer = true;
    public float precondition_Energy = 65f;
    public int precondition_Wood = 10;
    public float precondition_PatienceRate = 2;
    public bool precondition_HouseOwned = false;

    [Header("Effects")]
    public float effect_Energy = -65f;
    public int effect_Wood = -10;
    public bool effect_HouseOwned = true;
}
