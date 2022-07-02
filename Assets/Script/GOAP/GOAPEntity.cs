using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class GOAPEntity : MonoBehaviour
{
    public float speed;
    public Text energyText;
    public Text patienceText;
    public Text woodText;
    public Text houseText;
    public Text bedText;
    public Text hammerText;
    public Text axeText;
    public Text currentStateText;

    [Header("Finish when character has: ")]
    [Range(0, 100)]
    public float energyNeededToFinish;
    public bool house;
    public bool wood;
    [Range(0, 40)]
    public int finishWithWoodAmount = 0;
    public bool hammer;
    public bool axe;

    [Header("Initial Values")]
    public bool hasHouse;
    public bool hasHammer;
    public bool hasAxe;
    [Range(75, 100)]
    public float energyAmount = 75f;
    public int initialWood;
    public int patienceAmount;

    List<GOAPAction> actions;

    GOAPState current;
    public GOAPState Current { get { return current; } set { current = value; } }

    Func<GOAPState, bool> satisfies;
    public Func<GOAPState, bool> Satisfies { get { return satisfies; } }
    Func<GOAPState, float> heuristics;
    public Func<GOAPState, float> Heuristics { get { return heuristics; } }


    IEnumerable<GOAPAction> sequence;
    public IEnumerable<GOAPAction> Sequence { get { return sequence; } set { sequence = value; } }

    [Header("Scriptable Objects")]
    public SO_BuildHouseAction buildHouseSO;
    public SO_GatherWoodAction gatherWoodSO;
    public SO_BuildBedAction buildBedSO;
    public SO_PickAxeAction pickAxeSO;
    public SO_PickHammerAction pickHammerSO;
    public SO_SleepAction sleepSO;
    public SO_KillEnemyAction killEnemySO;
    public SO_OpenHouseAction openHouseSO;
    public SO_ActivateCheatsAction activateCheatsSO;

    private void Start()
    {
        buildHouseSO = Resources.Load<SO_BuildHouseAction>("SO_" + "BuildHouse" + "Action");
        gatherWoodSO = Resources.Load<SO_GatherWoodAction>("SO_" + "GatherWood" + "Action");
        buildBedSO = Resources.Load<SO_BuildBedAction>("SO_" + "BuildBed" + "Action");
        pickAxeSO = Resources.Load<SO_PickAxeAction>("SO_" + "PickAxe" + "Action");
        pickHammerSO = Resources.Load<SO_PickHammerAction>("SO_" + "PickHammer" + "Action");
        sleepSO = Resources.Load<SO_SleepAction>("SO_" + "Sleep" + "Action");
        killEnemySO = Resources.Load<SO_KillEnemyAction>("SO_" + "KillEnemy" + "Action");
        openHouseSO = Resources.Load<SO_OpenHouseAction>("SO_" + "OpenHouse" + "Action");
        activateCheatsSO = Resources.Load<SO_ActivateCheatsAction>("SO_" + "ActivateCheats" + "Action");

        actions = new List<GOAPAction>()
        {
            new GOAPAction("BuildHouse").SetPreconditions
                (
                    (a) =>
                    {
                        return a.HasHammer == buildHouseSO.precondition_HasHammer &&
                            a.Energy >= buildHouseSO.precondition_Energy &&
                            a.Wood >= buildHouseSO.precondition_Wood &&
                            a.PatienceRate >= buildHouseSO.precondition_PatienceRate &&
                            a.HouseOwned == buildHouseSO.precondition_HouseOwned;
                    }
                ).SetEffects
                (
                    (a, b) =>
                    {
                        b.Energy = a.Energy + buildHouseSO.effect_Energy;
                        b.Wood = a.Wood + buildHouseSO.effect_Wood;
                        b.HouseOwned = buildHouseSO.effect_HouseOwned;
                        energyText.text = a.Energy.ToString();
                        woodText.text = a.Wood.ToString();
                        houseText.text = a.HouseOwned.ToString();
                    }
                ).SetCost(buildHouseSO.cost)
                .SetAct(a => StartCoroutine(ActionRoutines.BuildHouseCoroutine(a))),
            new GOAPAction("GatherWood").SetPreconditions
                (
                    (a) =>
                    {
                        return
                            a.HasAxe == gatherWoodSO.precondition_HasAxe &&
                            a.Energy >= gatherWoodSO.precondition_Energy;
                    }
                ).SetEffects
                (
                    (a,b) =>
                    {
                        b.Energy = a.Energy + gatherWoodSO.effect_Energy;
                        b.Wood = a.Wood + gatherWoodSO.effect_Wood;
                        energyText.text = a.Energy.ToString();
                        woodText.text = a.Wood.ToString();
                    }
                ).SetCost(gatherWoodSO.cost)
                .SetAct(a => StartCoroutine(ActionRoutines.GatherWoodCoroutine(a))),
            new GOAPAction("PickAxe").SetPreconditions
                (
                    (a) =>
                    {
                        return
                            a.HasAxe == pickAxeSO.precondition_HasAxe &&
                            a.PatienceRate >= pickAxeSO.precondition_PatienceRate;
                    }
                ).SetEffects
                (
                    (a,b) =>
                    {
                        b.HasAxe = pickAxeSO.effect_HasAxe;
                        b.PatienceRate = a.PatienceRate + pickAxeSO.precondition_PatienceRate;
                        patienceText.text = a.PatienceRate.ToString();
                        axeText.text = a.HasAxe.ToString();
                    }
                ).SetCost(pickAxeSO.cost)
                .SetAct(a => StartCoroutine(ActionRoutines.GetToolCoroutine(a, ToolType.AXE))),
            new GOAPAction("Sleep").SetPreconditions
                (
                    (a) =>
                    {
                        return
                            a.BedBuilt == sleepSO.precondition_bedBuilt &&
                            a.Energy <= sleepSO.precondition_Energy &&
                            a.PatienceRate >= sleepSO.precondition_PatienceRate;
                    }
                ).SetEffects
                (
                    (a,b) =>
                    {
                        b.Energy = a.Energy + sleepSO.effect_Energy;
                        b.PatienceRate = a.PatienceRate + sleepSO.effect_Patience;
                        energyText.text = a.Energy.ToString();
                        patienceText.text = a.PatienceRate.ToString();
                        bedText.text = a.BedBuilt.ToString();
                    }
                ).SetCost(sleepSO.cost)
                .SetAct(a => StartCoroutine(ActionRoutines.SleepCoroutine(a))),
            new GOAPAction("PickHammer").SetPreconditions
                (
                (a) =>
                    {
                        return
                            a.HasHammer == pickHammerSO.precondition_HasHammer &&
                            a.PatienceRate >= pickHammerSO.precondition_PatienceRate;
                    }
                ).SetEffects
                (
                    (a,b) =>
                    {
                        b.HasHammer = pickHammerSO.effect_HasHammer;
                        b.PatienceRate = a.PatienceRate + pickHammerSO.effect_PatienceRate;
                        patienceText.text = a.PatienceRate.ToString();
                        hammerText.text = a.HasHammer.ToString();
                    }
                ).SetCost(pickHammerSO.cost)
                .SetAct(a => StartCoroutine(ActionRoutines.GetToolCoroutine(a, ToolType.HAMMER))),
            new GOAPAction("BuildBed").SetPreconditions
                (
                    (a) =>
                    {
                        return
                            a.BedBuilt == buildBedSO.precondition_BedBuilt &&
                            a.Energy >= buildBedSO.precondition_Energy &&
                            a.Wood >= buildBedSO.precondition_Wood &&
                            a.PatienceRate >= buildBedSO.precondition_PatienceRate;
                    }
                ).SetEffects
                (
                    (a,b) =>
                    {
                        b.Energy = a.Energy + buildBedSO.effect_Energy;
                        b.BedBuilt = buildBedSO.effect_BedBuilt;
                        b.PatienceRate = a.PatienceRate + buildBedSO.effect_PatienceRate;
                        energyText.text = a.Energy.ToString();
                        patienceText.text = a.PatienceRate.ToString();
                        woodText.text = a.Wood.ToString();
                        bedText.text = a.BedBuilt.ToString();
                    }
                ).SetCost(buildBedSO.cost)
                .SetAct(a => StartCoroutine(ActionRoutines.BuildBed(a))),
            new GOAPAction("ActivateCheats").SetPreconditions
                (
                    (a) =>
                    {
                        return
                            a.PatienceRate <= activateCheatsSO.precondition_PatienceRate;
                    }
                ).SetEffects
                (
                    (a,b) =>
                    {
                        b.Energy = activateCheatsSO.effect_Energy;
                        b.HouseOwned = activateCheatsSO.effect_HouseOwned;
                        energyText.text = a.Energy.ToString();
                        houseText.text = a.HouseOwned.ToString();
                    }
                ).SetCost(activateCheatsSO.cost)
                .SetAct(a => StartCoroutine(ActionRoutines.ActivateCheatsCoroutine(a))),
            new GOAPAction("KillEnemy").SetPreconditions
                (
                    (a) =>
                    {
                        return
                            a.PatienceRate <= killEnemySO.precondition_PatienceRate &&
                            a.HasAxe == killEnemySO.precondition_HasAxe &&
                            a.Energy <= killEnemySO.precondition_EnergyMax &&
                            a.Energy >= killEnemySO.precondition_EnergyMin;
                    }
                ).SetEffects
                (
                    (a,b) =>
                    {
                        b.Energy += killEnemySO.effect_Energy;
                        b.Keys.Add(killEnemySO.effect_HouseKeys);
                        b.PatienceRate += killEnemySO.effect_PatienceRate;
                        energyText.text = a.Energy.ToString();
                        patienceText.text = a.PatienceRate.ToString();
                    }
                ).SetCost(killEnemySO.cost)
                .SetAct(a => StartCoroutine(ActionRoutines.KillEnemyCoroutine(a))),
            new GOAPAction("OpenHouse1").SetPreconditions
                (
                    (a) =>
                    {
                        return
                            a.Keys.Contains(openHouseSO.precondition_houseKey);
                    }
                ).SetEffects
                (
                    (a,b) =>
                    {
                        b.HouseOwned = openHouseSO.effect_HouseOwned;
                        houseText.text = a.HouseOwned.ToString();
                    }
                ).SetCost(openHouseSO.cost)
                .SetAct(a => StartCoroutine(ActionRoutines.OpenHouseCoroutine(a))),
        };

        current = new GOAPState(energyAmount, initialWood, patienceAmount, hasAxe, hasHammer, false, hasHouse);

            satisfies = (a) => 
            {
                var satisfactionCondition = a.Energy >= energyNeededToFinish;

                if(house) satisfactionCondition = satisfactionCondition && a.HouseOwned; 
                if(hammer) satisfactionCondition = satisfactionCondition && a.HasHammer; 
                if(axe) satisfactionCondition = satisfactionCondition && a.HasAxe;
                if (wood) satisfactionCondition = satisfactionCondition && a.Wood > finishWithWoodAmount;

                return satisfactionCondition;
            };

        heuristics = (a) =>
        {
            float cost = 0;
            cost += Mathf.Max(a.Energy, 0) < energyNeededToFinish ? (energyNeededToFinish - Mathf.Max(a.Energy, 0)) * 0.3f : 0;
            cost += !a.HouseOwned ? 1f : 0f;
            return cost;
        };

        sequence = GOAP.RunGOAPOld(this, current, actions);
        if (sequence == null)
        {
            Debug.Log("No se pudo planear");
        }
        else sequence.First().Act(this);
    }
}