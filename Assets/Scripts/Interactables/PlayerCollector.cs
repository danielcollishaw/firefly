using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerCollector : MonoBehaviour
{
    private int collectableCount; 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    public float textTimer = 5;
    private bool timerStarted = false; 
    public GameObject finalJar;
    public GameObject firefly;
    public int numberOfCollectableFireflies;

    // Start is called before the first frame update
    void Start()
    {
        collectableCount = 0;

        SetCountText();
        winTextObject.SetActive(false);
        finalJar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(collectableCount >= numberOfCollectableFireflies) 
        {

            timerStarted = true; 
            
            if (textTimer < 1)
            {
              winTextObject.SetActive(false);
              timerStarted = false;
              Debug.Log("Timer reached 0");

            }
            else if (textTimer > 0)
            {
                textTimer -= Time.deltaTime;
                  Debug.Log("Timer has this many seconds " + textTimer);
            }


        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // will be called when the player first touches a trigger collider (i.e the collectables)
        if(other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            collectableCount++;
            Debug.Log(collectableCount);
            //called everytime it touches collider 

            SetCountText();
        }
        // using TAGS to correctly disable the collectables and not other objects 
    }

    private void SetCountText() 
    {
        countText.text = "Fireflies Collected: \t\t" + collectableCount.ToString() + "/" + numberOfCollectableFireflies.ToString();
        if(collectableCount >= numberOfCollectableFireflies) 
        {
            winTextObject.SetActive(true);
            firefly.SetActive(true);
            finalJar.SetActive(true);
            countText.text = "";
        }
    }
}
