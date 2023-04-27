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
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance
    {
        get => instance;
    }
    public GameSave GameSave
    {
        get => gameSave;
    }

    private static LevelManager instance;
    private GameSave gameSave;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log($"LevelManager>There should only be one total instance of LevelManager in all scenes.\nowning object name: {gameObject.name} |");
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        gameSave = GameSave.LoadGameSave();

        Scene activeScene = SceneManager.GetActiveScene();
        Debug.Log($"Welcome to {activeScene.name}!");
    }
    private void OnDestroy()
    {
        if (gameSave != null && gameSave.LevelsUnlocked.Count > 0)
        {
            GameSave.SaveGameSave(gameSave);
        }
    }
}