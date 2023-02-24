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
    public GameObject firefly;
    public int numberOfCollectableFireflies;

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
        countText.text = "Count: " + collectableCount.ToString();
        if(collectableCount >= numberOfCollectableFireflies) 
        {
            winTextObject.SetActive(true);
            firefly.SetActive(true);
        }
    }
}
