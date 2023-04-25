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
    public List<bool> UnlockedLevels
    {
        get => unlockedLevels;
    }

    private static LevelManager instance;
    private GameSave gameSave;
    private List<bool> unlockedLevels;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("LevelManager>There should only be one total instance of LevelManager in all scenes.");
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        gameSave = GameSave.LoadGameSave();
        unlockedLevels = gameSave.LevelsUnlocked.ToList();

        Scene activeScene = SceneManager.GetActiveScene();
        Debug.Log($"Welcome to {activeScene.name}!");
    }
    private void OnDestroy()
    {
        gameSave.LevelsUnlocked = unlockedLevels.ToArray();
        GameSave.SaveGameSave(gameSave);
    }
}