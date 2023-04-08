using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollect : MonoBehaviour
{   
    public GameObject[] fireflies;
    
    void OnTriggerEnter()
    {
        foreach (GameObject fly in fireflies)
        {
            fly.SetActive(true);
        }
    }
}
