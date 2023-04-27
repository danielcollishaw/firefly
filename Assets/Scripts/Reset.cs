using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public GameObject player;
    private AbilityHolder ability;
    private Vector3 spawn;

    private bool canReset = true;

    private void Start()
    {
        setSpawn(player.transform.position);
        ability = player.GetComponent<AbilityHolder>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.DeathSFX, other.transform.position);

            if (other.TryGetComponent<TimeTrial>(out var timetrial))
            {
                timetrial.ResetTimer();
            }

            if (canReset)
            {
                canReset = false;
                ResetLevel();
                StartCoroutine(ResetDelay());
            }
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
        LevelManager.Instance.GameSave.FallCount++;
        int count = LevelManager.Instance.GameSave.FallCount;
        Extend.FindFallCountCanvas(player).UpdateFallCount(count);
    }
    private IEnumerator ResetDelay()
    {
        yield return new WaitForSeconds(1.0f);
        canReset = true;
    }
}
