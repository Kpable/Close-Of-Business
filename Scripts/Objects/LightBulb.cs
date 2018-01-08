using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Light bulb that goes off or flickers when broken. 
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class LightBulb : DestructableObject {

    bool canFlicker = false;
    Light lightSource;

    protected override void Awake()
    {
        base.Awake();
        lightSource = transform.GetComponentInChildren<Light>();
        Debug.Log("light found " + lightSource == null);
    }
	
    IEnumerator FlickerBulb()
    {
        if (canFlicker)
        {
            canFlicker = false;
            //GetComponent<AudioSource>().PlayOneShot(clip);
            lightSource.enabled = true;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
            lightSource.enabled = false;
            yield return new WaitForSeconds(Random.Range(0.1f, 5f));
            canFlicker = true;

        }
    }

    public override void BreakObject()
    {
        //base.BreakObject();
        // Give it a 30% chance this bulb will flicker
        canFlicker = Random.value > 0.7f;
        // If it doesnt flicker, turn if off
        if (!canFlicker) lightSource.enabled = false;
        
    }

    protected override void Update () {
        if (canFlicker) StartCoroutine("FlickerBulb");
	}
}
