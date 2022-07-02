using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPPreconditions
{
    float energy;
    int wood;
    bool requireAxe;
    bool hasAxe;
    bool requireHammer;
    bool hasHammer;
    string equipped;
    public float Energy { get => energy; set => energy = value; }
    public int Wood { get => wood; set => wood = value; }
    public bool HasAxe { get => hasAxe; set => hasAxe = value; }
    public bool HasHammer { get => hasHammer; set => hasHammer = value; }
    public string Equipped { get => equipped; set => equipped = value; }
    public bool RequireAxe { get => requireAxe; set => requireAxe = value; }
    public bool RequireHammer { get => requireHammer; set => requireHammer = value; }

    public GOAPPreconditions(float energy = 0, int wood = 0, bool requireAxe = false, bool hasAxe = false, bool requireHammer = false, bool hasHammer = false, string equipped = "")
    {
        this.energy = energy;
        this.wood = wood;
        this.hasAxe = hasAxe;
        this.hasHammer = hasHammer;
        this.equipped = equipped;
        this.requireAxe = requireAxe;
        this.requireHammer = requireHammer;
    }
}
