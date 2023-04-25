#region Copyright
/*---------------------------------------------------------------*/
/*        File: OverworldLevel.cs                                */
/*        Firefly                                                */
/*        AI For Game Programming - CAP 4053                     */
/*        Copyright (c) 2023 Serenity Studios                    */
/*        All rights reserved.                                   */
/*        Made with love, by Justin Sasso.                       */
/*---------------------------------------------------------------*/
#endregion

using System;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldLevel : MonoBehaviour
{
    public Camera DevinCamera
    {
        get => devinCamera;
    }

    private const string BASE_LEVEL = "Scenes/";

    [SerializeField]
    private string levelName = "";
    [SerializeField]
    private float rotateSpeed = 25.0f;
    [SerializeField]
    private GameObject chooseLevelCompPrefab;
    [SerializeField]
    private GameObject baseCamera;
    private Camera devinCamera;

    private GameObject mesh;

    private GameObject chooseLevelCompObject;
    private ChooseLevelComp chooseLevelComp;

    
    private float activeRotation = 0.0f;

    private bool canLoad = true;
    private bool canChooseLevel = false;
    private bool levelLoading = false;

    private bool unlocked;
    
    private void Start()
    {
        if (levelName == "")
        {
            Debug.Log(name + ": level not set.");
            canLoad = false;
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.gameObject.name == "Mesh")
                {
                    mesh = child.gameObject;
                }
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
    public void Init(bool unlocked)
    {
        this.unlocked = unlocked;

        Debug.Log(levelName + "- unlocked: " + unlocked); // DEBUG
    }
    private void Update()
    {
        if (canLoad)
        {
            RotateLevel();

            if (canChooseLevel)
            {
                bool pressed = Input.GetButton("Confirm");

                if (pressed)
                {
                    if (unlocked)
                    {
                        LoadLevel();
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ToggleChooseLevelComp(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ToggleChooseLevelComp(false);
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

        canChooseLevel = activated;
    }
    private void CreateChooseLevelCompObject()
    {
        Vector3 worldPosition = transform.position;
        Vector3 spawnLocation = worldPosition + new Vector3(0, 10, 0);
        chooseLevelCompObject = Instantiate(chooseLevelCompPrefab, spawnLocation, Quaternion.identity);

        if (chooseLevelCompObject.TryGetComponent<ChooseLevelComp>(out var chooseLevelComp))
        {
            this.chooseLevelComp = chooseLevelComp;
            chooseLevelComp.Init(gameObject);

            if (unlocked)
            {
                chooseLevelComp.SetLevelText(levelName);
            }
            else
            {
                chooseLevelComp.SetLevelText("LOCKED\n" + levelName);
            }
            
            chooseLevelComp.SetCanvasCamera(devinCamera);
        }
        else
        {
            Debug.Log("Couldn't find ChooseLevelComp.");
        }
    }
    private void LoadLevel()
    {
        if (!levelLoading)
        {
            levelLoading = true;
            SceneManager.LoadScene(BASE_LEVEL + levelName, LoadSceneMode.Single);
        }
    }
    private void RotateLevel()
    {
        activeRotation += rotateSpeed * Time.deltaTime;
        mesh.transform.rotation = Quaternion.Euler(0, activeRotation, 0);
    }
}
