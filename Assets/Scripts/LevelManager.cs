#region Copyright
/*---------------------------------------------------------------*/
/*        File: LevelManager.cs                                  */
/*        Firefly                                                */
/*        AI For Game Programming - CAP 4053                     */
/*        Copyright (c) 2023 Serenity Studios                    */
/*        All rights reserved.                                   */
/*        Made with love, by Justin Sasso.                       */
/*---------------------------------------------------------------*/
#endregion

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // To access LevelManager, go through the singleton var and connect 
    // appropriate signals for level wide events.
    public static LevelManager Single { get; private set; }

    public readonly LoadLevelEvent EventLoadLevel = new();
    public readonly UnityEvent EventLevelComplete = new();

    void Start()
    {
        // Checks if LevelManager has been instanced before, and if it has, then that means
        // an error has occurred because this should only be instanced once so this handles
        // that.
        if (Single != null)
        {
            Debug.Log("There shouldn't be more than one instance of LevelManager!");
            return;
        }

        EventLoadLevel.AddListener(OnLoadLevel);
        EventLevelComplete.AddListener(OnLevelComplete);

        Single = this;

        //EventLoadLevel.Invoke("Shader Proto");

        Scene activeScene = SceneManager.GetActiveScene();
        Debug.Log($"Welcome to {activeScene.name}! There are {SceneManager.sceneCount} loaded scenes.");
    }
    private void OnLoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
    private void OnLevelComplete()
    {
        Debug.Log("Level completed, yay!");
    }
}
[Serializable]
public class LoadLevelEvent : UnityEvent<string> { }
