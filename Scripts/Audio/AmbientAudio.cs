using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// 
/// Ambient Audio. 
/// 
/// Created By: Kpable
/// Date Created: 10-16-17
/// 
/// </summary>
public class AmbientAudio : MonoBehaviour {

    private AudioClip clip;
    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();

        Assert.IsNotNull(audioSource, "Need to add an AudioSource to " + name);

        AudioManager.instance.OnAmbientVolumeChange += OnVolumeChange;
    }

    public void OnVolumeChange(float value)
    {
        audioSource.volume = value;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
