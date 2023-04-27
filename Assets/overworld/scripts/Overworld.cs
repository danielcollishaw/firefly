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
using System.Collections;
using UnityEditor.VersionControl;

public class Overworld : MonoBehaviour
{
    [SerializeField]
    private GameObject fallCountCanvasObject;
    private FallCountCanvas fallCountCanvas;

    private readonly List<OverworldLevel> allOverworldLevels = new();

    private void Start()
    {
        DetectLevels();

        if (fallCountCanvasObject.TryGetComponent<FallCountCanvas>(out var fallCountCanvas))
        {
            this.fallCountCanvas = fallCountCanvas;
            fallCountCanvas.Toggle(true);
            StartCoroutine(LateStart());
        }
        else
        {
            Debug.Log("Overworld>couldn't find FallCountCanvas.");
        }

        
    }
    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        fallCountCanvas.UpdateFallCount(LevelManager.Instance.GameSave.FallCount);
    }
    private void DetectLevels()
    {
        OverworldLevel[] foundOverworldLevels = FindObjectsOfType<OverworldLevel>();
        Dictionary<string, bool> unlockedLevels = LevelManager.Instance.GameSave.LevelsUnlocked;

        for (int i = 0; i < foundOverworldLevels.Length; i++)
        {
            OverworldLevel level = foundOverworldLevels[i];

            if (unlockedLevels.Count <= i && level.LevelName == "Level_1")
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