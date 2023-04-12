using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    // List for audio events
    private List<EventInstance> eventInstances;

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one audio manager in scene.");
        }
        instance = this;

        eventInstances = new List<EventInstance>();
    }
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        // Plays SFX that is meant to be played one time
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);

        // Add audio event to list to be deleted after scene
        eventInstances.Add(eventInstance);

        return eventInstance;
    }

    private void CleanUp()
    {
        // Stop and release any created instances
        foreach(EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }
    
    private void OnDestroy()
    {
        CleanUp();
    }
}
