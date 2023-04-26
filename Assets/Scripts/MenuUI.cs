using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    // Start is called before the first frame update

     public GameObject uiMenuObject;
    void Start()
    {
      uiMenuObject.SetActive(false);  
    }

     void OnTriggerEnter (Collider player)
    {
        if(player.gameObject.CompareTag("Player"))
        {
            uiMenuObject.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

     IEnumerator WaitForSec() 
    {
        yield return new WaitForSeconds(6);
         Destroy(uiMenuObject);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
