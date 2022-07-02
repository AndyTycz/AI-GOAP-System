using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GOAPState
{
    float energy;
    int wood;
    bool hasAxe;
    bool hasHammer;
    bool bedBuilt;
    bool houseBuilt;
    string equipped;
    List<string> keys;
    int patienceRate;
    public float Energy { get => energy; set => energy = Mathf.Max(Mathf.Min(value, 100),0); }
    public int Wood { get => wood; set => wood = Mathf.Max(value, 0); }
    public bool HasAxe { get => hasAxe; set => hasAxe = value; }
    public bool HasHammer { get => hasHammer; set => hasHammer = value; }
    public string Equipped { get => equipped; set => equipped = value; }
    public bool BedBuilt { get => bedBuilt; set => bedBuilt = value; }
    public bool HouseOwned { get => houseBuilt; set => houseBuilt = value; }
    public List<string> Keys { get => keys; set => keys = value; }
    public int PatienceRate { get => patienceRate; set => patienceRate = Mathf.Max(Mathf.Min(value, 10), 0); }

    public GOAPAction generatingAction = null;
    public int stepId = 0;


    public GOAPState(float energy = 0, int wood = 0, int patienceRate = 10, bool hasAxe = false, bool hasHammer = false, bool bedBuilt = false, bool houseBuilt = false, string equipped = "", List<string> keys = null)
    {
        this.energy = energy;
        this.wood = wood;
        this.hasAxe = hasAxe;
        this.hasHammer = hasHammer;
        this.bedBuilt = bedBuilt;
        this.houseBuilt = houseBuilt;
        this.equipped = equipped;
        this.patienceRate = patienceRate;
        if (keys == null) this.keys = new List<string>() { "" };
        else this.keys = keys;
    }

    public GOAPState(GOAPState toClone)
    {
        this.energy = toClone.energy;
        this.wood = toClone.wood;
        this.hasAxe = toClone.hasAxe;
        this.hasHammer = toClone.hasHammer;
        this.bedBuilt = toClone.bedBuilt;
        this.houseBuilt = toClone.houseBuilt;
        this.equipped = toClone.equipped;
        this.patienceRate = toClone.patienceRate;
        this.keys = toClone.keys;
    }

    //Simply replace values
    public GOAPState UpdateValues(GOAPState baseState)
    {
        energy = baseState.Energy;
        wood = baseState.Wood;
        hasAxe = baseState.HasAxe;
        hasHammer = baseState.HasHammer;
        equipped = baseState.Equipped;
        return this;
    }

    public bool Equals(GOAPState toCompare)
    {
        return toCompare == this;
            //energy == toCompare.energy
            //&& wood == toCompare.wood
            //&& hasAxe == toCompare.hasAxe
            //&& hasHammer == toCompare.hasHammer
            //&& bedBuilt == toCompare.bedBuilt
            //&& houseBuilt == toCompare.houseBuilt
            //&& equipmentCount == toCompare.equipmentCount
            //&& equipped == toCompare.equipped
            //&& generatingAction == toCompare.generatingAction;
    }
}