using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// The Player's ability to interact with interactable objects
/// 
/// Created By: Kpable
/// Date Created: 08-02-17
/// 
/// </summary>
public class Interaction : MonoBehaviour {

    public List<Interactable> nearbyInteractableObjects = new List<Interactable>();
    private Ray ray;
    private RaycastHit hit;
    private Camera fpCam;

    // Use this for initialization
    void Start () {
        fpCam = gameObject.GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (nearbyInteractableObjects.Count > 0)
        {
            ray = fpCam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
            if (Physics.Raycast(ray, out hit, 10))
            {
                for (int i = 0; i < nearbyInteractableObjects.Count; i++)
                {
                    if (nearbyInteractableObjects[i].Equals(hit.collider.gameObject.GetComponent<Interactable>()))
                    {
                        nearbyInteractableObjects[i].highlighted = true;                     
                    }
                    else
                    {
                        nearbyInteractableObjects[i].highlighted = false;
                    }
                }
                
            }
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(ray);
    }
}
