using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Computer Tower, when broken, causes blue screen of death on linked monitors.
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class Computer : DestructableObject {

    [Tooltip("The monitors connected to this computer")]
    public GameObject[] connectedMonitors;

    /// <summary>
    /// Called by the router to alter the image on screen for all the monitors 
    /// connected to this computer
    /// </summary>
    public void LinkedRouterDestroyed()
    {
        // For each monitor..
        foreach (GameObject monitor in connectedMonitors)
        {
            // .. display sad face because no internet
            monitor.GetComponent<Monitor>().DisplaySadFace();
        }
    }

    /// <summary>
    /// Called when the computer tower breaks
    /// </summary>
    public override void BreakObject()
    {
        // For each monitor ..
        foreach (GameObject monitor in connectedMonitors)
        {
            // .. display blue screen of death because no computer
           if(monitor) monitor.GetComponent<Monitor>().DisplayBlueScreenOfDeath();
        }
  
        // Then break this computer
        base.BreakObject();
    }
}
