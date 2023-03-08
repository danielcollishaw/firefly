using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    public float speed = 50f;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        // Takes current input direction vector values [-1, 1];
        float x = Input.GetAxisRaw("CameraHorizontal");

        offset = Quaternion.AngleAxis(x * speed * Time.deltaTime, Vector3.up) * offset;
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform);
        //transform.Translate(Vector3.right * x * speed * Time.deltaTime);
    }
}
