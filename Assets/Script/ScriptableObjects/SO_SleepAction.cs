using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_SleepAction", menuName = "Scriptable Objects/GOAPActions/Sleep")]
public class SO_SleepAction : ScriptableObject
{
    [Header("Cost")]
    public float cost = 1f;

    [Header("Preconditions")]
    public bool precondition_bedBuilt = true;
    public float precondition_Energy = 50f;
    public int precondition_PatienceRate = 2;

    [Header("Effects")]
    public float effect_Energy = 50f;
    public int effect_Patience = 2;
}
