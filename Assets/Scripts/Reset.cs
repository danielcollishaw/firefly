using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public GameObject player;
    private AbilityHolder ability;
    private Vector3 spawn;

    private void Start()
    {
        setSpawn(player.transform.position);
        ability = player.GetComponent<AbilityHolder>();
    }
    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = spawn;
        ability.removePower();
    }

    public void setSpawn(Vector3 pos)
    {
        spawn = pos;
    }
}
