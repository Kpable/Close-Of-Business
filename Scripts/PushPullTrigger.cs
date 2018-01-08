using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// Trigger to push objects away or pull them closer for as long as they are within the trigger. 
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class PushPullTrigger : MonoBehaviour {

    public enum PushPull { Push, Pull }

    [Tooltip("Sets the force to apply while the object is within the trigger.")]
    public float moveForce = 20;        //The lift force to apply to the object that enters the trigger
    [Tooltip("Sets the direction of which the above force is applied.")]
    public PushPull direction = PushPull.Push;  //The direction in which to apply that force. 
    public Transform relativeToTransform;
    List<Rigidbody> targets;    

    void Awake()
    {
        targets = new List<Rigidbody>();                    //Strangle prevention comment!
        if (relativeToTransform == null) relativeToTransform = transform;
    }

    void FixedUpdate()
    {
        if( targets.Count > 0 )
        {
            //Continuously add liftForce to the rigid bodies in the trigger
            switch (direction)
            {
                case PushPull.Push:
                    targets.ForEach(body => { body.AddForce(relativeToTransform.forward * moveForce); }); // Get handsy with that body
                    break;
                case PushPull.Pull:
                    targets.ForEach(body => { body.AddForce(relativeToTransform.forward * -1 * moveForce); }); // Get handsy with that body
                    break;
                default:
                    break;
            }

        }
    }

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(name + " was triggered by: " + col.gameObject.name);
        Rigidbody body = col.GetComponent<Rigidbody>();             //Grab the Rigidbody
        if ( body != null)                                          //Check if its present           
            targets.Add(body);                                      //Add to list of bodies to get handsy with                   
    }

    void OnTriggerExit(Collider col)
    {
        //Debug.Log(name + " trigger exited by: " + col.gameObject.name);
        Rigidbody body = col.GetComponent<Rigidbody>();        //Grab the Rigidbody
        if ( body != null )                                         //Check if its present
        {
            if ( targets.Contains(body) )                       //Check if it's currently in the list of bodies
            {
                bool success = targets.Remove(body);            //Remove them if they are
                if (!success) Debug.LogWarning("Failed to remove body: " + body.gameObject.name);
            }
        }
    }
}
