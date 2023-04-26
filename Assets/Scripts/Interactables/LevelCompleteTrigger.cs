#region Copyright
/*---------------------------------------------------------------*/
/*        File: LevelCompleteTrigger.cs                          */
/*        Firefly                                                */
/*        AI For Game Programming - CAP 4053                     */
/*        Copyright (c) 2023 Serenity Studios                    */
/*        All rights reserved.                                   */
/*        Made with love, by Justin Sasso.                       */
/*---------------------------------------------------------------*/
#endregion

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteTrigger : MonoBehaviour
{
    [SerializeField]
    private string nextLevel = "";
    [SerializeField]
    private BoxCollider collision;
    [SerializeField]
    private GameObject chooseLevelCompPrefab;
    private GameObject chooseLevelCompObject;
    [SerializeField]
    private GameObject baseCamera;
    private Camera devinCamera;

    private GameObject mesh;

    private bool canLoad = false;
    private bool hasLoaded = false;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.gameObject.name == "Mesh")
            {
                mesh = child.gameObject;
            }
        }


        if (baseCamera.TryGetComponent<Camera>(out var devinCamera))
        {
            this.devinCamera = devinCamera;
        }
        else
        {
            Debug.Log("OverworldLevel>Couldn't get devinCamera.");
        }
    }

    private void Update()
    {
        if (canLoad)
        {
            bool pressed = Input.GetButton("Confirm");

            if (pressed)
            {
                GoBackToOverworld();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canLoad = true;
            //ToggleChooseLevelComp(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canLoad = false;
            //ToggleChooseLevelComp(false);
        }
    }
    private void GoBackToOverworld()
    {
        if (!hasLoaded)
        {
            hasLoaded = true;
            
            try
            {
                LevelManager.Instance.GameSave.LevelsUnlocked[nextLevel] = true;
            }
            catch (Exception ex)
            {
                Debug.Log($"LevelCompleteTrigger>Name: {nextLevel} doesn't exist. Error: {ex}");
                return;
            }

            SceneManager.LoadScene("overworld/scenes/overworld", LoadSceneMode.Single);
        }
    }
    private void ToggleChooseLevelComp(bool activated)
    {
        if (activated)
        {
            if (chooseLevelCompObject == null)
            {
                CreateChooseLevelCompObject();
            }
        }
        else
        {
            if (chooseLevelCompObject != null)
            {
                Destroy(chooseLevelCompObject);
                chooseLevelCompObject = null;
            }
        }

        canLoad = activated;
    }
    private void CreateChooseLevelCompObject()
    {
        Vector3 worldPosition = transform.position;
        Vector3 spawnLocation = worldPosition + new Vector3(0, 10, 0);
        chooseLevelCompObject = Instantiate(chooseLevelCompPrefab, spawnLocation, Quaternion.identity);

        if (chooseLevelCompObject.TryGetComponent<ChooseLevelComp>(out var chooseLevelComp))
        {
            chooseLevelComp.Init(gameObject);
            chooseLevelComp.SetLevelText("Level Complete!\nPress A to Exit.");
            chooseLevelComp.SetCanvasCamera(devinCamera);
        }
        else
        {
            Debug.Log("Couldn't find ChooseLevelComp.");
        }
    }
}