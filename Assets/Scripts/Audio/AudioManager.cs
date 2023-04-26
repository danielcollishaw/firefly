using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    // Volume Settings
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;

    [Range(0, 1)]
    public float musicVolume = 1;

    [Range(0, 1)]
    public float sfxVolume = 1;

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;

    // List for audio events
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;

    private EventInstance musicEventInstance;

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one audio manager in scene.");
        }
        instance = this;

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();

        // References to FMOD buses
        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }
    private void Start()
    {
        // Minus 1 for offset
        int level = SceneManager.GetActiveScene().buildIndex;

        switch (level)
        {
            case 0:
                InitializeMusic(FMODEvents.instance.MenuMusic);
                break;
            case 1:
                InitializeMusic(FMODEvents.instance.OverWorldMusic);
                break;
            case 2:
                InitializeMusic(FMODEvents.instance.TutorialMusic);
                break;
            case 3:
                InitializeMusic(FMODEvents.instance.Level1Music);
                break;
            case 4:
                InitializeMusic(FMODEvents.instance.Level2Music);
                break;
            case 5:
                InitializeMusic(FMODEvents.instance.Level3Music);
                break;
            case 6:
                InitializeMusic(FMODEvents.instance.Level4Music);
                break;
            case 7:
                InitializeMusic(FMODEvents.instance.Level5Music);
                break;
            case 8:
                InitializeMusic(FMODEvents.instance.Level6Music);
                break;
            case 9:
                // Special TimeTrial case
                // Start music once ability firefly is interacted with
                // InitializeMusic(FMODEvents.instance.Level7Music);
                break;
            case 10:
                InitializeMusic(FMODEvents.instance.Level8Music);
                break;
            default:
                Debug.LogError("Found mystery scene: " + level);
                break;
        }
    }
    private void Update()
    {
        // Set Volume equal to user input if default values change
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
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
    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }
    public void TimeTrialMusicStart()
    {
        InitializeMusic(FMODEvents.instance.Level7Music);
    }
    public void TimeTrialMusicStop()
    {
        musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    private void CleanUp()
    {
        // Stop and release any created instances
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

        // stop all of the event emitters, because if we don't they may hand around in other scenes 
        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}