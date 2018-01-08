using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Computer monitor that shows different images 
/// when the computer and/or router break
/// 
/// Created By: Kpable
/// Date Created: 06-08-17
/// 
/// </summary>
public class Monitor : DestructableObject {

    [Tooltip("The image for the Blue Screen of Death")]
    public Sprite blueScreenOfDeath;
    [Tooltip("The image for the Sad No Internet Face")]
    public Sprite sadFace;

    Image screen;           // The screen of the monitor
	// Use this for initialization
	void Start () {
        // Get the image we'll be changing
        screen = GetComponentInChildren<Image>();
        if (screen == null) Debug.LogError("Cannot find the screen image");
	}

    public void DisplaySadFace()
    {
        // Set the sad face
        screen.sprite = sadFace;
    }
    
    public void DisplayBlueScreenOfDeath()
    {
        // Set the blue screen of death
        screen.sprite = blueScreenOfDeath;
    }
}
