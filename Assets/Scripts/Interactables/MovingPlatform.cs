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

    GameObject MovingPlatformObject;

    // Start is called before the first frame update
    void Start()
    {
        MovingPlatformObject = gameObject.transform.GetChild(0).gameObject;
        GameObject DestinationMarker = MovingPlatformObject.transform.GetChild(0).gameObject;

        DestinationMarker.SetActive(false);

        start = MovingPlatformObject.transform.position;
        end = DestinationMarker.transform.position;
        direction = end - start;
        direction.Normalize();

        threshold = buffer * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        // gameObject.transform.position += direction * speed * Time.deltaTime;
        MovingPlatformObject.transform.Translate(direction * speed * Time.deltaTime);

        if (platformWithinEndPoints())
        {
            direction *= -1;
            // gameObject.transform.position += direction * speed * Time.deltaTime;
            MovingPlatformObject.transform.Translate(direction * speed * Time.deltaTime);
        }
            
    }

    private bool platformWithinEndPoints()
    {
        return Vector3.Distance(MovingPlatformObject.transform.position, start) < threshold || Vector3.Distance(MovingPlatformObject.transform.position, end) < threshold;
    }
}
