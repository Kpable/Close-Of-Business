using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Interface to listen on child events
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public interface IChildEventListener : IChildCollisionEventListener, IChildTriggerEventListener { }

public interface IChildCollisionEventListener : IChildCollisionEnter, IChildCollisionExit { }

public interface IChildTriggerEventListener : IChildTriggerEnter, IChildTriggerExit { }

public interface IChildCollisionEnter
{
    void OnChildCollisionEnter(Collision collision);
}

public interface IChildCollisionExit
{
    void OnChildCollisionExit(Collision collision);
}

public interface IChildTriggerEnter
{
    void OnChildTriggerEnter(Collider collision);
}

public interface IChildTriggerExit
{
    void OnChildTriggerExit(Collider collision);
}
