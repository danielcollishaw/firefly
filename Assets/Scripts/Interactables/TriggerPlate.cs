using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlate : MonoBehaviour
{
    public Collider trigger;
    public GameObject gate;

    void OnTriggerEnter()
    {
        gate.GetComponent<GateControl>().OpenGate();
    }
}
