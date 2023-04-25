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
        if(other.gameObject.CompareTag("Player"))
        {
            if (other.TryGetComponent<TimeTrial>(out var timetrial))
            {
            timetrial.ResetTimer();
            }
            ResetLevel();

        }
    }

    public void setSpawn(Vector3 pos)
    {
        spawn = pos;
    }

    public void ResetLevel()
    {
        player.transform.position = spawn;
        ability.RemovePower();
    }
}
