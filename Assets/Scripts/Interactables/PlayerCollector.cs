using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerCollector : MonoBehaviour
{
    private int collectableCount;     
   
    //public TextMeshProUGUI countText;
    public TextMeshProUGUI fireflycountText;
    public GameObject winTextObject;
    public GameObject finalJar;
    public GameObject firefly;

    public int numberOfCollectableFireflies;
    public bool levelCompleted = false;
    public float totalTimeTrial = 20.0f;
    public float textTimer = 5;


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
    }

    private void OnTriggerEnter(Collider other)
    {
        
        // will be called when the player first touches a trigger collider (i.e the collectables)
        if(other.gameObject.CompareTag("Collectable"))
        {
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

        if(collectableCount >= numberOfCollectableFireflies) 
        {
            winTextObject.SetActive(true);
            firefly.SetActive(true);
            finalJar.SetActive(true);
            fireflycountText.text = "";
        }
    }
}
