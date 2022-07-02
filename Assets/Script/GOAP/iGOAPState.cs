using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iGOAPState
{
    float CalculateHeuristics();
    bool Equals(iGOAPState stateToCompare);
    void UpdateValues();
}
