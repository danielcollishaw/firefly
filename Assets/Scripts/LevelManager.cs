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
using System.Text.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance
    {
        get => instance;
    }

    private static LevelManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("LevelManager>There should only be one total instance of LevelManager in all scenes.");
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        Scene activeScene = SceneManager.GetActiveScene();
        Debug.Log($"Welcome to {activeScene.name}!");
    }
    private bool Save()
    {
        return false;
    }
}