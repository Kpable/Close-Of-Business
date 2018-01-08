using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Object that gets shot out of camera turrets to hurt the player
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class Projectile : MonoBehaviour {

    public int damageOnImpact = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Health collisionHealth = collision.gameObject.GetComponent<Health>();
        if (collisionHealth != null)
        {
            collisionHealth.Damage(Mathf.RoundToInt(damageOnImpact * collision.relativeVelocity.magnitude));
            if (collisionHealth.currentHealth <= 0)
                Debug.Log("Object Destroyed");
                        
        }

        Destroy(gameObject);
    }
}
