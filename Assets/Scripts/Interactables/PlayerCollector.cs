using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerCollector : MonoBehaviour
{
    private int collectableCount;     
    private bool timerStarted = false;
    private bool levelCompleted = false;
    private float timeLeft;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI fireflycountText;
    public GameObject winTextObject;
    public GameObject finalJar;
    public GameObject firefly;

    public int numberOfCollectableFireflies;

    public float totalTimeTrial = 20.0f;
    public float textTimer = 5;


    // Start is called before the first frame update
    void Start()
    {
        collectableCount = 0;

        //SetCountText();
        winTextObject.SetActive(false);
        finalJar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(collectableCount >= numberOfCollectableFireflies) 
        {
            levelCompleted = true;

            if (textTimer <= 0)
            {
              winTextObject.SetActive(false);
            }
            else if (textTimer > 0)
            {
                textTimer -= Time.deltaTime;
            }
        }
        
        // Began countdown if level mechanic is triggered
        if(timerStarted)
        {
            timeLeft -= Time.deltaTime;
            countText.text = "Time Left: " + Mathf.Round(timeLeft).ToString();

            // Remove timer if level is completed
            if(levelCompleted)
            {
                countText.text = "";
            }
            // FIXME!!!: Restart level
            else if (timeLeft <= 0)
            {
                // Countdown has finished
                // No need to keep this line once scene restarts
                countText.text = "Time's up!";
            }
        }
             
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if green firefly has been activate and begin time trial 
        // Add roll mechanic as well
        if(other.gameObject.CompareTag("GreenFF"))
        {
            other.gameObject.SetActive(false);
            timeLeft = totalTimeTrial;
            timerStarted = true;
            SetCountText();
        }
        // will be called when the player first touches a trigger collider (i.e the collectables)
        else if(other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            collectableCount++;
            //called everytime it touches collider 
            SetCountText();
        }

        // using TAGS to correctly disable the collectables and not other objects 
    }

    private void SetCountText() 
    {
        fireflycountText.text = "Fireflies Collected: \t\t" + collectableCount.ToString() + "/" + numberOfCollectableFireflies.ToString();

        if(collectableCount >= numberOfCollectableFireflies) 
        {
            winTextObject.SetActive(true);
            firefly.SetActive(true);
            finalJar.SetActive(true);
            fireflycountText.text = "";
        }
    }
}
