using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public enum ObjectiveType
{
    Kick, 
    Steal,
    Plant,
    Break, 
    GoTo,
    Exit,
    //Destroy
    None
}

/// <summary>
/// 
/// Objective
/// 
/// Created By: Kpable
/// Date Created: 08-20-17
/// 
/// </summary>
[System.Serializable]
public class Objective {

    [HideInInspector]
    public bool complete;
    public ObjectiveType type;
    public string targetObject;
    public string targetZone;

    public override string ToString()
    {
        string objective = "";
        if(type != ObjectiveType.GoTo && type != ObjectiveType.Exit)
            objective += type.ToString() + " a " + targetObject + "\n";
        else
            objective += type.ToString() + " the " + targetZone + "\n";

        return objective;
    }
}

