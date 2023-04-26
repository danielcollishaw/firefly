using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private BoxCollider stepTrigger;

    private Vector3 direction;
    private Vector3 start;
    private Vector3 end;
    
    private float threshold;
    public float speed = 5f;
    private float buffer = 3;

    private GameObject parent;
    private GameObject platformMesh;
    private GameObject destinationMarker;

    private void Start()
    {
        parent = transform.parent.gameObject;
        platformMesh = gameObject;
        destinationMarker = platformMesh.transform.GetChild(0).gameObject;

        destinationMarker.SetActive(false);

        start = parent.transform.position;
        end = destinationMarker.transform.position;
        direction = end - start;
        direction.Normalize();
        threshold = buffer * speed * Time.deltaTime;
        parent.transform.position = start + ((end - start) / 2);
    }
    private void Update()
    {
        parent.transform.position += direction * speed * Time.deltaTime;

        if (PlatformWithinEndPoints())
        {
            direction *= -1;
            parent.transform.position += direction * speed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.SetParent(parent.transform);
    }
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.transform.parent = null;
    }
    private bool PlatformWithinEndPoints()
    {
        return Vector3.Distance(parent.transform.position, start) < threshold || Vector3.Distance(parent.transform.position, end) < threshold;
    }
}
