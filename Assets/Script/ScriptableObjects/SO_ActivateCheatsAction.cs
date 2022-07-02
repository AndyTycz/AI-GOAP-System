using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_ActivateCheats", menuName = "Scriptable Objects/GOAPActions/ActivateCheats")]
public class SO_ActivateCheatsAction : ScriptableObject
{
    [Header("Cost")]
    public float cost = 10f;

    [Header("Preconditions")]
    public float precondition_PatienceRate = 0;

    [Header("Effects")]
    public float effect_Energy = 100f;
    public bool effect_HouseOwned = true;
}
