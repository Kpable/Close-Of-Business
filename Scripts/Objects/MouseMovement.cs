using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// MouseMovement to reflect on a desk
/// 
/// Created By: Kpable
/// Date Created: 10-05-17
/// 
/// </summary>
public class MouseMovement : MonoBehaviour {

    public Vector3 size;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Mouse Position: " + Input.mousePosition);
        Debug.Log("STW Mouse Position: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Debug.Log("STV Mouse Position: " + Camera.main.ScreenToViewportPoint(Input.mousePosition));

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
        
    }

}
