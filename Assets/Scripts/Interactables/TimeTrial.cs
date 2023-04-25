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

    [SerializeField]
    private Reset reset;

    [SerializeField]
    private PlayerCollector playerCollector;

    // Start is called before the first frame update
    void Start()
    {
        // Reference to playercollect script
        playerCollector = devin.GetComponent<PlayerCollector>();
    }

    // Update is called once per frame
    void Update()
    {
        // Stop time trial audio
        //dateTimeTrialSound();

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
            // Restart level
            else if (timeLeft <= 0)
            {
                timerStarted = false;
                // Countdown has finished
                // No need to keep this line once scene restarts
                countdownTimer.text = "Time's up!";
                reset.ResetLevel();
                ResetTimer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if green firefly has been activate and begin time trial 
        // Add roll mechanic as well
        if (other.gameObject.CompareTag("Roll"))
        {
            timeLeft = totalTimeTrial;
            timerStarted = true;

            // TimeTrial Music begins
            AudioManager.instance.TimeTrialMusicStart();
        }
    }
    public void ResetTimer()
    {
        timerStarted = false;
        timeLeft = totalTimeTrial;
        countdownTimer.text = "";
        playerCollector.ResetCount();

        // TimeTrial Music ends
        AudioManager.instance.TimeTrialMusicStop();
    }
}

