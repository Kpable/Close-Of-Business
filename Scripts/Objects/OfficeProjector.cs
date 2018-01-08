using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Office Projector
/// When broken, the image goes away. 
/// 
/// Created By: Kpable
/// Date Created: 06-12-17
/// 
/// </summary>
public class OfficeProjector : DestructableObject {

    Image screen;
    Vector3 initialposition;
    // Use this for initialization
    void Start()
    {
        screen = GetComponentInChildren<Image>();
        if (screen == null) Debug.LogError("Cannot find the screen image");
        initialposition = transform.position;
    }

    protected override void HandleCollision(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
        {
            screen.gameObject.SetActive(false);
            Debug.Log(name + "magnitude " + collision.relativeVelocity.magnitude);

        }
        base.HandleCollision(collision);
    }

    protected override void Update()
    {
        if(transform.position != initialposition && screen.gameObject.activeInHierarchy)
        {
            screen.gameObject.SetActive(false);
            Debug.Log(name + " inital postition " + initialposition + " current position " + transform.position);
        }

        base.Update();

    }

    //public void DisplaySadFace()
    //{
    //    screen.sprite = sadFace;
    //}

    //public void DisplayBlueScreenOfDeath()
    //{
    //    screen.sprite = blueScreenOfDeath;
    //}
}

