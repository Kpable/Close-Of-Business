using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Objects that explodes damaging/breaking everything around it. 
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class ExplodableObject : DestructableObject {

    [Tooltip("The radius of the explosion")]
    public float explosionRadius = 10f;
    [Tooltip("The force of the explosion")]
    public float explosionForce = 15f;

    public override void BreakObject()
    {
        // Get a list of all the colliders in the explosion radius
        Collider[] impacted =  Physics.OverlapSphere(transform.position, explosionRadius);
        // For each collider.. 
        foreach (Collider item in impacted)
        {
            // If the its not this object,
            if (item.gameObject.GetComponent<DestructableObject>() && item.gameObject.name != gameObject.name)
            {
                // Destroy that object
                item.gameObject.GetComponent<DestructableObject>().BreakObject();
            }
        }

        // Get the newly spawned debris in the explosion radius
        impacted = Physics.OverlapSphere(transform.position, explosionRadius);
        // For each piece of debris..
        foreach (Collider item in impacted)
        {
            // If that object has a rigid body (it should)
            if(item.GetComponent<Rigidbody>())
                // Add EXPLOSIVE force just for fun!! No not really, but because thats what this script does
                item.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        // Proceed to break this object
        base.BreakObject();
    }

    private void OnDrawGizmos()
    {
        // Draw the radius for view in the editor
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
