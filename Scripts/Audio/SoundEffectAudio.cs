using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// 
/// SoundEffectAudio.
/// 
/// Created By: Kpable
/// Date Created: 10-16-17
/// 
/// </summary>
public class SoundEffectAudio : MonoBehaviour {

    public string soundEffectName;
    private AudioClip clip;
    private AudioSource audioSource;
    private SoundEffectContainer container;

    // Use this for initialization
    void Start () {

        audioSource = GetComponent<AudioSource>();
        container = AudioManager.instance.soundEffectContainer;

        Assert.IsNotNull(audioSource, "Need to add an AudioSource to " + name);
        Assert.IsNotNull(container, "Check the audiomanager ");

        AudioManager.instance.OnSoundEffectVolumeChange += OnVolumeChange;
    }

    public void OnVolumeChange(float value)
    {
        audioSource.volume = value;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(container.GetSoundEffect(soundEffectName));
    }
}
