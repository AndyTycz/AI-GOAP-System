using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_PickHammerAction", menuName = "Scriptable Objects/GOAPActions/PickHammer")]
public class SO_PickHammerAction : ScriptableObject
{
    [Header("Cost")]
    public float cost = .5f;

    [Header("Preconditions")]
    public bool precondition_HasHammer = false;
    public int precondition_PatienceRate = 1;

    [Header("Effects")]
    public bool effect_HasHammer = true;
    public int effect_PatienceRate = -1;
}
