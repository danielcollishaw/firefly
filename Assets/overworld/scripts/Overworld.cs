#region Copyright
/*---------------------------------------------------------------*/
/*        File: Overworld.cs                                     */
/*        Firefly                                                */
/*        AI For Game Programming - CAP 4053                     */
/*        Copyright (c) 2023 Serenity Studios                    */
/*        All rights reserved.                                   */
/*        Made with love, by Justin Sasso.                       */
/*---------------------------------------------------------------*/
#endregion

using System;
using UnityEngine;
using System.Collections.Generic;

public class Overworld : MonoBehaviour
{
    private readonly List<OverworldLevel> allOverworldLevels = new();

    private void Start()
    {
        DetectLevels();
    }
    private void DetectLevels()
    {
        OverworldLevel[] foundOverworldLevels = FindObjectsOfType<OverworldLevel>();
        Dictionary<string, bool> unlockedLevels = LevelManager.Instance.GameSave.LevelsUnlocked;

        for (int i = 0; i < foundOverworldLevels.Length; i++)
        {
            OverworldLevel level = foundOverworldLevels[i];

            if (unlockedLevels.Count <= i && level.LevelName == "Tutorial Level")
            {
                unlockedLevels.Add(level.LevelName, true);
            }
            else if (unlockedLevels.Count <= i)
            {
                unlockedLevels.Add(level.LevelName, false);
            }

            level.Init(unlockedLevels[level.LevelName]);
            allOverworldLevels.Add(level);
        }
    }
}