using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public enum MissionType
{
    Sequential_Unrestricted, // Objectives follow an order but if a later objective is completed all previous are skipped/marked completed
    Sequential_Restricted, // Objectives follow an order and can not be completed out of order
    Free_For_All // Objectives are completed whenever
}

/// <summary>
/// 
/// Mission
/// 
/// Created By: Kpable
/// Date Created: 08-20-17
/// 
/// </summary>
[System.Serializable]
public class Mission {

    public string missionDescription;
    public bool complete = false;
    public MissionType type = MissionType.Free_For_All;

    public Objective[] objectives;

    public delegate void ObjectiveComplete(int i);
    public event ObjectiveComplete OnObjectiveComplete;

    public delegate void MissionComplete();
    public event MissionComplete OnMissionComplete;

    public override string ToString()
    {
        string mission = "";
        for (int i = 0; i < objectives.Length; i++)
        {
            mission += objectives[i].type.ToString() + " a " + objectives[i].targetObject + "\n";
        }
        return mission;
    }

    public void CheckMissionObjectives(string target, List<string> zones, ObjectiveType trigger)
    {
        switch (type)
        {
            case MissionType.Sequential_Unrestricted:
                // Go through in reverse order checking completion
                for (int i = objectives.Length - 1; i >= 0; i--)
                {
                    CheckForObjectiveCompletion(objectives[i], target, zones, trigger);
                    // if this objective has been met, complete every previous objective
                    if (objectives[i].complete)
                    {
                        for (int j = i; j >= 0; j--)
                        {
                            objectives[j].complete = true;
                        }
                    }
                }

                break;
            case MissionType.Sequential_Restricted:

                Objective o = FirstIncompleteObjective();
                if (o != null) CheckForObjectiveCompletion(o, target, zones, trigger);

                break;
            case MissionType.Free_For_All:

                for (int i = 0; i < objectives.Length; i++)
                {
                    CheckForObjectiveCompletion(objectives[i], target, zones, trigger);
                }

                break;
            default:
                break;
        }
        

        CheckForMissionComplete();
    }

    private void CheckForObjectiveCompletion(Objective objective, string target, List<string> zones, ObjectiveType trigger)
    {
        switch (objective.type)
        {
            case ObjectiveType.Kick:
                if (trigger == ObjectiveType.Kick &&
                    objective.targetObject == target &&
                    !GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().IsWalking)
                {
                    objective.complete = true;
                }
                break;
            case ObjectiveType.Steal:
                if(objective.targetObject == target && !zones.Contains(objective.targetZone))
                {
                    objective.complete = true;
                }
                break;
            case ObjectiveType.Plant:
                if (objective.targetObject == target && zones.Contains(objective.targetZone))
                {
                    objective.complete = true;
                }
                break;
            case ObjectiveType.Break:
                if (objective.targetObject == target && trigger == ObjectiveType.Break )
                {
                    objective.complete = true;
                }
                break;
            case ObjectiveType.GoTo:
                if (zones.Contains(objective.targetZone) && trigger == ObjectiveType.GoTo)
                {
                    objective.complete = true;
                }
                break;
            case ObjectiveType.Exit:
                if (!zones.Contains(objective.targetZone) && trigger == ObjectiveType.Exit)
                {
                    objective.complete = true;
                }
                break;
            default:
                break;
        }

        if(objective.complete && OnObjectiveComplete != null) OnObjectiveComplete(ObjectiveIndex(objective));


    }

    private int ObjectiveIndex(Objective objective)
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i].Equals(objective)) return i;
        }
        return -1;
    }

    //public int ObjectiveIndexOfIncompleteObjectives(Objective objective)
    //{
    //    List<Objective> incomplete = new List<Objective>();

    //    for (int i = 0; i < objectives.Length; i++)
    //    {
    //        if (!objectives[i].complete) incomplete.Add(objectives[i]);
    //    }
    //    for (int i = 0; i < objectives.Length; i++)
    //    {
    //        return incomplete.IndexOf(objective);
    //    }        
    //    return -1;
    //}

    private Objective FirstIncompleteObjective()
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            if (!objectives[i].complete) return objectives[i];
        }
        return null;
    }

    //public void ObjectHit(string target)
    //{
    //    switch (type)
    //    {
    //        case MissionType.Sequential_Unrestricted:
    //            break;
    //        case MissionType.Sequential_Restricted:
    //            break;
    //        case MissionType.Free_For_All:
    //            for (int i = 0; i < objectives.Length; i++)
    //            {
    //                // Check for running
    //                if (objectives[i].type == ObjectiveType.Kick &&
    //                    objectives[i].target == target &&
    //                    !GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().IsWalking)
    //                {
    //                    objectives[i].complete = true;
    //                }
    //            }
    //            break;
    //        default:
    //            break;
    //    }

    //    CheckForMissionComplete();

    //}
        
    void CheckForMissionComplete()
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            if (!objectives[i].complete)
            {
                Debug.Log("Found an incomplete objective, return early");
                return;
            }
        }

        Debug.Log("Mission is complete");
        complete = true;
        if (OnMissionComplete != null) OnMissionComplete();
    }

}
