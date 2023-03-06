using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TimeTrial : MonoBehaviour
{
    public PlayerCollector playerCollector;
    public GameObject devin;

    public float totalTimeTrial = 20.0f;
    public TextMeshProUGUI countdownTimer;


    private bool timerStarted = false;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        // Reference to playercollect script
        playerCollector = devin.GetComponent<PlayerCollector>();
    }

    // Update is called once per frame
    void Update()
    {
       
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
            }
            // FIXME!!!: Restart level
            else if (timeLeft <= 0)
            {
                // Countdown has finished
                // No need to keep this line once scene restarts
                countdownTimer.text = "Time's up!";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if green firefly has been activate and begin time trial 
        // Add roll mechanic as well
        if (other.gameObject.CompareTag("GreenFF"))
        {
            other.gameObject.SetActive(false);
            timeLeft = totalTimeTrial;
            timerStarted = true;
        }
    }
}

