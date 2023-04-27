#region Copyright
/*---------------------------------------------------------------*/
/*        File: MenuSettings.cs                                  */
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
using UnityEngine.SceneManagement;

public class MenuSettings : MonoBehaviour
{
    [SerializeField]
    private GameObject exitLevelObject;
    [SerializeField]
    private GameObject exitGameObject;
   
    private void Start()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name == "Menu" || activeScene.buildIndex == 0)
        {
            exitLevelObject.SetActive(false);
            exitGameObject.SetActive(false);
        }
    }
    public void ExitLevel()
    {
        SceneManager.LoadScene("overworld/scenes/overworld", LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }
}
