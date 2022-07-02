using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_BuildBedAction", menuName = "Scriptable Objects/GOAPActions/BuildBed")]
public class SO_BuildBedAction : ScriptableObject
{
    [Header("Cost")]
    public float cost = 2f;

    [Header("Preconditions")]
    public bool precondition_BedBuilt = false;
    public float precondition_Energy = 40f;
    public int precondition_Wood = 3;
    public float precondition_PatienceRate = 1;

    [Header("Effects")]
    public float effect_Energy = -40f;
    public int effect_PatienceRate = -1;
    public bool effect_BedBuilt = true; 
}
