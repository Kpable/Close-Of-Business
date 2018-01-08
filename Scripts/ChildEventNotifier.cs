using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Interface to inform on child events
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class ChildEventNotifier : MonoBehaviour {

    IChildEventListener parentListener;

    void Start()
    {
        parentListener = FindParentListenter(transform);
        if (parentListener == null) Debug.LogError(name + ": cant find parent listener");
    }

    IChildEventListener FindParentListenter(Transform t)
    {
        // If the passed in transform has a listener, return that listener
        if (t.GetComponent<IChildEventListener>() != null)
        {
            return t.GetComponent<IChildEventListener>();
        }

        // If the passed in transform has no parent, return nothing
        if (t.parent == null)
        {
            return null;
        }

        //Otherwise recursively climb up the tree
        return FindParentListenter(t.parent);
    }

    private void OnTriggerEnter(Collider other)
    {
        parentListener.OnChildTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        parentListener.OnChildTriggerExit(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        parentListener.OnChildCollisionEnter(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        parentListener.OnChildCollisionExit(collision);
    }
}
