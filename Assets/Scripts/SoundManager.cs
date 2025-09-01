using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton Sound Manager responsible for playing sound effects and background music.
/// Supports one background music source and a list of named sound effects.
/// </summary>
public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// Represents a single sound entry in the sound list.
    /// Each sound has a name and an AudioClip reference.
    /// </summary>
    [System.Serializable]
    public class SoundGroup
    {
        public AudioClip audioClip;  // The audio file to play
        public string soundName;     // The unique name used to find this sound
    }

    [Header("Background Music")]
    public AudioSource bgmSound;     // Audio source for looping background music

    [Header("Sound Effects")]
    public List<SoundGroup> sound_List = new List<SoundGroup>(); // List of available sound effects

    // Singleton instance
    public static SoundManager instance;

    /// <summary>
    /// Unity Start method.
    /// Initializes singleton instance and starts background music coroutine.
    /// </summary>
    public void Start()
    {
        instance = this;
        StartCoroutine(StartBGM());
    }

    /// <summary>
    /// Plays a sound effect by its name.
    /// </summary>
    /// <param name="_soundName">The name of the sound effect to play.</param>
    public void PlayingSound(string _soundName)
    {
        int index = FindSound(_soundName);
        if (index < sound_List.Count)
        {
            AudioClip clip = sound_List[index].audioClip;
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + _soundName);
        }
    }

    /// <summary>
    /// Finds the index of a sound in the sound list by its name.
    /// </summary>
    /// <param name="_soundName">The name of the sound to search for.</param>
    /// <returns>The index of the sound in the list, or list.Count if not found.</returns>
    private int FindSound(string _soundName)
    {
        int i = 0;
        while (i < sound_List.Count)
        {
            if (sound_List[i].soundName == _soundName)
                return i;

            i++;
        }
        return i; // returns sound_List.Count if not found
    }

    /// <summary>
    /// Restarts the background music playback.
    /// </summary>
    void ManageBGM()
    {
        StartCoroutine(StartBGM());
    }

    /// <summary>
    /// Coroutine that starts background music playback after a short delay.
    /// Useful for waiting until the scene is fully loaded.
    /// </summary>
    IEnumerator StartBGM()
    {
        yield return new WaitForSeconds(0.5f);
        bgmSound.Play();
    }
}
