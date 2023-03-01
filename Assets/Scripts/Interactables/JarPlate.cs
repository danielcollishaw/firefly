using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarPlate : MonoBehaviour
{
    public Collider trigger;
    public GameObject gate;

    void OnTriggerEnter()
    {
        gameObject.SetActive(false);
        gate.GetComponent<GateControl>().OpenGate();
    }
}
