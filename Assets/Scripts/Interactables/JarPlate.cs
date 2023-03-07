using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarPlate : MonoBehaviour
{
    public Collider trigger;
    public GameObject gate;
    public GameObject sceneGate;

    void Start()
    {
        sceneGate.SetActive(false);
    }

    void OnTriggerEnter()
    {
        gameObject.SetActive(false);
        sceneGate.SetActive(true);
        gate.GetComponent<GateControl>().OpenGate();
    }
}
