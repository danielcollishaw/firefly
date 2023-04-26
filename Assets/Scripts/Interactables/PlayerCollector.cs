using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerCollector : MonoBehaviour
{
    private int collectableCount;
    private readonly List<Collider> collectableList = new();

    public TextMeshProUGUI fireflycountText;
    public GameObject winTextObject;

    public int numberOfCollectableFireflies;

    private bool levelCompleted = false;
    public float WintextTimer = 5;

    // Start is called before the first frame update
    void Start()
    {
        collectableCount = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if all collectable fireflies are collected
        if (collectableCount >= numberOfCollectableFireflies)
        {
            levelCompleted = true;

            // Timer for wintext displayed
            if (WintextTimer <= 0)
            {
                winTextObject.SetActive(false);
            }
            else if (WintextTimer > 0)
            {
                WintextTimer -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // will be called when the player first touches a trigger collider (i.e the collectables)
        if (other.gameObject.CompareTag("Collectable"))
        {
            collectableList.Add(other);
            // Audio sfx 
            AudioManager.instance.PlayOneShot(FMODEvents.instance.FireflyInteractSFX, this.transform.position);

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

        if (collectableCount >= numberOfCollectableFireflies)
        {
            winTextObject.SetActive(true);
        }
    }

    // Reset everything if Devin dies
    public void ResetCount()
    {
        for (int i = 0; i < collectableList.Count; i++)
        {
            collectableList[i].gameObject.SetActive(true);
            collectableList[i].gameObject.GetComponent<FMODUnity.StudioEventEmitter>().Play();
        }

        levelCompleted = false;
        winTextObject.SetActive(false);
        collectableCount = 0;
        fireflycountText.text = "Fireflies Collected: \t\t" + collectableCount.ToString() + "/" + numberOfCollectableFireflies.ToString();
    }
    public bool GetLevelCompleted()
    {
        return levelCompleted;
    }
}