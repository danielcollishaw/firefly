using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteTrigger : MonoBehaviour
{
    [SerializeField]
    private BoxCollider collision;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentSceneIndex >= sceneCount)
            {
                Debug.Log("Not enough scenes");
                return;
            }

            bool currentSceneValid = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex).IsValid();
            bool nextSceneValid = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).IsValid();
            
            if (currentSceneValid)
            {
                if (nextSceneValid)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    Debug.Log("Next scene should be added to the build index.");
                }
            }
            else
            {
                Debug.Log("Current scene should be added to the build index.");
            }
        }
    }
}
