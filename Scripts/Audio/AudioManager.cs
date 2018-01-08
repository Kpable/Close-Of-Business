using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Manage the audio in the game. 
/// Holds and changes volumes for all sound effects.
/// 
/// Created By: Kpable
/// Date Created: 10-16-17
/// 
/// </summary>
public class AudioManager : MonoBehaviour {
    public static AudioManager instance;

    public delegate void AmbientVolumeChange(float volume);
    public event AmbientVolumeChange OnAmbientVolumeChange;

    public delegate void SoundEffectVolumeChange(float volume);
    public event SoundEffectVolumeChange OnSoundEffectVolumeChange;

    public SoundEffectContainer soundEffectContainer;

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        // Make a Singleton
        // If the static instance isnt null
        if (instance != null)
        {
            // We already have one Sound Manager Object In the Game, Destroy all others
            Destroy(gameObject);
        }
        else
        {
            // This is the first instance of Sound Manager, Store it and prevent it from dying. 
            instance = this;
            DontDestroyOnLoad(gameObject);
            // Only Initialize once
        }

        if (soundEffectContainer == null)
            soundEffectContainer = GetComponent<SoundEffectContainer>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
