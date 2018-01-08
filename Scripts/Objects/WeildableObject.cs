using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Objects the player can pick up and use
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class WeildableObject : DestructableObject {

    public bool throwable = true;
    public Action action;
    public float force = 20;

	// Use this for initialization
	void Start () {
		
	}

    public override void BreakObject()
    {
        base.BreakObject();
    }

    // Update is called once per frame
    protected override void Update () {
        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
        {
            LeftClick();
        }

        if(!Input.GetMouseButton(0) && Input.GetMouseButtonDown(1))
        {
            RightClick();
        }
	}

    protected virtual void LeftClick()
    {
        if(action != null ) action.Act();
    }

    protected virtual void RightClick()
    {
        //if (throwable)
        //{
        //    gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.VelocityChange);
        //}
    }

}
