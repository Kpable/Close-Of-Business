using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Toggle a GameObject in the scene. 
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class ToggleObjectAction : Action {

    /// Target GameObject to enable/disable
    public GameObject target;

    public override void Act()
    {
        //base.Act();
        Debug.Log("Toggling Object");
        ToggleObject();
    }

    void ToggleObject()
    {
        if (target.activeInHierarchy)
            target.SetActive(false);
        else
            target.SetActive(true);
    }

}
