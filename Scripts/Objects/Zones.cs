using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Identifies where the player is located for location based objectives. 
/// 
/// Created By: Kpable
/// Date Created: 09-18-17
/// 
/// </summary>
public class Zones : MonoBehaviour {

    public delegate void EnteredZone(string zoneName);
    public static event EnteredZone OnEnterZone;

    public delegate void ExitedZone(string zoneName);
    public static event ExitedZone OnExitZone;

    public string Name { get { return name; } }

	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (OnEnterZone != null) OnEnterZone(Name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (OnExitZone != null) OnExitZone(Name);
        }
    }
}
