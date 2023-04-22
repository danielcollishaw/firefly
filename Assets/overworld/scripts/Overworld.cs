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
using UnityEngine.SceneManagement;
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

        for (int i = 0; i < foundOverworldLevels.Length; i++)
        {
            allOverworldLevels.Add(foundOverworldLevels[i]);
        }
    }
}
