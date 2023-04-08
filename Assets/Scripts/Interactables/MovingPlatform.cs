using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 5f;

    private Vector3 direction;
    private Vector3 start;
    private Vector3 end;
    private bool moveToEnd;
    private float threshold; 
    private float buffer = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject destinationMarker = gameObject.transform.GetChild(0).gameObject;
        destinationMarker.SetActive(false);

        start = gameObject.transform.position;
        end = destinationMarker.transform.position;
        direction = end - start;
        direction.Normalize();

        threshold = buffer * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += direction * speed * Time.deltaTime; 
        
        if (platformWithinEndPoints())
        {
            direction *= -1;
            gameObject.transform.position += direction * speed * Time.deltaTime; 
        }
            
    }

    private bool platformWithinEndPoints()
    {
        return Vector3.Distance(gameObject.transform.position, start) < threshold || Vector3.Distance(gameObject.transform.position, end) < threshold;
    }
}
