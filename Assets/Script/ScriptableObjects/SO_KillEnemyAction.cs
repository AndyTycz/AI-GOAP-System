using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_KillEnemyAction", menuName = "Scriptable Objects/GOAPActions/KillEnemy")]
public class SO_KillEnemyAction : ScriptableObject
{
    [Header("Cost")]
    public float cost = 7.5f;

    [Header("Preconditions")]
    public bool precondition_HasAxe = true;
    public float precondition_EnergyMax = 70f;
    public float precondition_EnergyMin = 15f;
    public float precondition_PatienceRate = 3;

    [Header("Effects")]
    public float effect_Energy = -30f;
    public int effect_PatienceRate = 10;
    public string effect_HouseKeys = "House 1";
}
 