using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPAction
{
    Func<GOAPState, bool> preconditions;
    public Func<GOAPState, bool> Preconditions { get { return preconditions; } }
    Action<GOAPState, GOAPState> effects;
    public Action<GOAPState, GOAPState> Effects { get => effects; }
    private Action<GOAPEntity> act;
    public Action<GOAPEntity> Act { get => act; }
    string actionName;
    public string Name { get { return actionName; } }
    float cost;
    public float Cost { get { return cost; } }


    public GOAPAction (string name)
    {
        this.actionName = name;
        cost = 1f;
    }

    public GOAPAction SetCost (float cost)
    {
        this.cost = cost;
        return this;
    }

    public GOAPAction SetPreconditions(Func<GOAPState, bool> preconditions)
    {
        this.preconditions = preconditions;
        return this;
    }

    public GOAPAction SetEffects(Action<GOAPState, GOAPState> effects)
    {
        this.effects = effects;
        return this;
    }

    public GOAPAction SetAct(Action<GOAPEntity> act)
    {
        this.act = act;
        return this;
    }
}
