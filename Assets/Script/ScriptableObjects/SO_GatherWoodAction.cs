using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_GatherWoodAction", menuName = "Scriptable Objects/GOAPActions/GatherWood")]
public class SO_GatherWoodAction : ScriptableObject
{
    [Header("Cost")]
    public float cost = 3.5f;

    [Header("Preconditions")]
    public bool precondition_HasAxe = true;
    public float precondition_Energy = 40f;

    [Header("Effects")]
    public float effect_Energy = -35f;
    public int effect_Wood = 10;
}
