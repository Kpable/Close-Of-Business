using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 
/// Contain all the sound effects for the game. 
/// Including multiple for the same action (ie. 5 different jump sound effects).
/// 
/// Created By: Kpable
/// Date Created: 10-16-17
/// 
/// </summary>
public class SoundEffectContainer : MonoBehaviour {

    public List<SoundEffect> soundEffects = new List<SoundEffect>();

    public AudioClip GetSoundEffect(string soundEffectName)
    {
        // Get all the Sound Effects with the name provided
        List<SoundEffect> foundClips = soundEffects.Where(clip => clip.name == soundEffectName).ToList<SoundEffect>();
        //Debug.Log(name + ": Clips count-" + clips.Count);
        // If  one or more was found
        if (foundClips.Count > 0)
        {
            // If only one sound effect was found return the first in the list
            if (foundClips.Count == 1) return foundClips[0].clips[Random.Range(0, foundClips[0].clips.Length)];
            // If more than one sound effect was found, return one at random
            else return foundClips[Random.Range(0, foundClips.Count)].clips[Random.Range(0, foundClips[0].clips.Length)];
        }
        // If no sound effect was found return null
        Debug.LogWarning(name + ": No sound effect named '" + soundEffectName + "' found");

        return null;
    }
}

[System.Serializable]
public class SoundEffect
{
    public string name;
    public AudioClip[] clips;
}
