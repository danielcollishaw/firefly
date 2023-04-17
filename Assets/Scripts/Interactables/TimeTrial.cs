using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using FMOD.Studio;

public class TimeTrial : MonoBehaviour
{
    public GameObject devin;

    public float totalTimeTrial = 20.0f;
    public TextMeshProUGUI countdownTimer;

    private bool timerStarted = false;
    private float timeLeft;

    private Collider abilityFireFly;

    [SerializeField]
    private Reset reset;

    [SerializeField]
    private PlayerCollector playerCollector;

    // Audio
    private EventInstance TimeTrialSFX;

    // Start is called before the first frame update
    void Start()
    {
        // Reference to playercollect script
        playerCollector = devin.GetComponent<PlayerCollector>();

        // TimeTrial audio event
        TimeTrialSFX = AudioManager.instance.CreateEventInstance(FMODEvents.instance.TimeTrial);
    }

    // Update is called once per frame
    void Update()
    {
        // Stop time trial audio
        UpdateTimeTrialSound();

        bool levelCompleted = playerCollector.levelCompleted;
        //Debug.Log("level complete is " + levelCompleted);

        // Began countdown if level mechanic is triggered
        if (timerStarted)
        {
            timeLeft -= Time.deltaTime;
            countdownTimer.text = "Time Left: " + Mathf.Round(timeLeft).ToString();

            // Remove timer if level is completed
            if (levelCompleted)
            {
                countdownTimer.text = "";
                timerStarted = false;
            }
            // FIXME!!!: Restart level
            else if (timeLeft <= 0)
            {
                timerStarted = false;
                // Countdown has finished
                // No need to keep this line once scene restarts
                countdownTimer.text = "Time's up!";
                reset.ResetLevel();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if green firefly has been activate and begin time trial 
        // Add roll mechanic as well
        if (other.gameObject.CompareTag("Roll"))
        {
            abilityFireFly = other;
            abilityFireFly.gameObject.SetActive(false);
            timeLeft = totalTimeTrial;
            timerStarted = true;
        }
    }
    public void ResetTimer()
    {
        abilityFireFly.gameObject.SetActive(true);
        timerStarted = false;
        timeLeft = totalTimeTrial;
        countdownTimer.text = "";
        playerCollector.ResetCount();
    }

    private void UpdateTimeTrialSound()
    {
        // Start footsteps audio event if the player is moving and on the ground
        if (timerStarted)
        {
            PLAYBACK_STATE playbackState;
            TimeTrialSFX.getPlaybackState(out playbackState);

            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                TimeTrialSFX.start();
            }
        }
        // Stop event if otherwise
        else
        {
            TimeTrialSFX.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}

