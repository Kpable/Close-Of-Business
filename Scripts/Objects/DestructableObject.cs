using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Base class for all objects in game that should break. 
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
[RequireComponent(typeof(Health))]
public class DestructableObject : MonoBehaviour, IChildEventListener {

    public delegate void BreakEvent(DestructableObject obj);
    public static event BreakEvent OnBreakObject;

    public delegate void HitEvent(DestructableObject obj);
    public static event HitEvent OnHitObject;

    [Tooltip("The broken object to be used when this solid one breaks")]
    public GameObject debris;
    [HideInInspector]
    public Health health;       // health component of this game object

    // This was added because occasionally an object would hit several of this object's colliders
    // at the same time triggering the breaking process several times at once
    private bool alreadyBroken = false;     // Whether this object is already broken
    
    protected virtual void Awake () {
        // Get the health component attached to this object
        health = gameObject.GetComponent<Health>();
        health.zeroHealthAlert += BreakObject;
	}

    internal void Damage(int dmg)
    {
        if (health != null) health.Damage(dmg);
    }

    protected virtual void Update () {}

    protected void OnCollisionEnter(Collision collision)
    {
        // When something hits this object deal with it
        HandleCollision(collision);
    }

    protected virtual void HandleCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (OnHitObject != null)
            {
                OnHitObject(this);
            }
        }

        // Grab the rigidBody of the collision
        Rigidbody collisionRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        // Grab the Health of the collision
        //Health collisionHealth = collision.gameObject.GetComponent<Health>();
        DestructableObject collisionDestructable = collision.gameObject.GetComponent<DestructableObject>();

        // If the collision has a rigidBody
        if (collisionRigidbody != null)
        {

            // if this object has health
            if(health!=null)
                // Damage this object based of the magnitude of the relative velocity
                Damage(Mathf.RoundToInt(collision.relativeVelocity.magnitude));

            // if collision object has health
            //if (collisionHealth!=null)
            //    // Damage collision object based of the magnitude of the relative velocity
            //    collisionHealth.Damage(Mathf.RoundToInt(collision.relativeVelocity.magnitude));

            if (collisionDestructable != null)
                // Damage collision object based of the magnitude of the relative velocity
                collisionDestructable.Damage(Mathf.RoundToInt(collision.relativeVelocity.magnitude));

            // If the collision has run out of health 
            //if (collisionHealth!=null && collisionHealth.currentHealth <= 0)
            //    // Break that object
            //    collision.gameObject.GetComponent<DestructableObject>().BreakObject();
            
            // If this object has run out of health
            //if (health!= null && health.currentHealth <= 0)
            //    // Break this object
            //    BreakObject();
        }
    }

    public virtual void BreakObject()
    {
        // Make sure we only break once
        if (!alreadyBroken)
        {
            if(OnBreakObject != null)
            {
                //Debug.Log("firing event");
                OnBreakObject(this);
            }

            // If not then set to true so we dont break again. 
            alreadyBroken = true;
            // If we have a broken object, Instatiate it at this objects position and rotation
            if (debris)
            {
                Instantiate(debris, transform.position, transform.rotation);

                // Kill this object
                Destroy(gameObject);
            }
        }
    }

    private void BreakObject(Vector3 velocity)
    {
        // If we have a broken object..
        if (debris)
        {
            //Instatiate it at this objects position and rotation
            GameObject debrisObject = Instantiate(debris, transform.position, transform.rotation) as GameObject;
            // Add the force that came with it
            debrisObject.GetComponent<Rigidbody>().AddForce(velocity);
        }

        // Kill this object
        Destroy(gameObject);
    }

    public void OnChildCollisionEnter(Collision collision)
    {
        // When something hits A CHILD of this object deal with it
        HandleCollision(collision);
    }

    public void OnChildCollisionExit(Collision collision) { }

    public void OnChildTriggerEnter(Collider collision) { }

    public void OnChildTriggerExit(Collider collision) { }
}
