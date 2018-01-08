using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Office Router Object
/// When broken, linked computers lose internet 
/// (shows on monitors linked to the linked computers)
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class Router : DestructableObject {

    public GameObject[] linkedComputers;

    public override void BreakObject()
    {
        foreach (GameObject computer in linkedComputers)
        {
            if (computer == null) continue;
            computer.GetComponent<Computer>().LinkedRouterDestroyed();
        }

        base.BreakObject();
    }
}
