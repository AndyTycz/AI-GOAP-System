using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_PickAxeAction", menuName = "Scriptable Objects/GOAPActions/PickAxe")]
public class SO_PickAxeAction : ScriptableObject
{
    [Header("Cost")]
    public float cost = .5f;

    [Header("Preconditions")]
    public bool precondition_HasAxe = false;
    public int precondition_PatienceRate = 1;

    [Header("Effects")]
    public bool effect_HasAxe = true;
    public int effect_PatienceRate = -1;
}
