using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Base class for Actions the player can take in the world.
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class Action : MonoBehaviour {

    public virtual void Act()
    {
        Debug.Log("Action!");
    }
}
