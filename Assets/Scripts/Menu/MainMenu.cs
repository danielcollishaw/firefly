using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Audio Menu Settings")]
    [SerializeField] private AudioMenuSettings audioMenuSettings;

    private bool InSettings = false;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            StartGame();
        }
        else if (Input.GetButtonDown("MenuToggle") && !InSettings)
        {
            Settings();
        }
        else if(Input.GetButtonDown("Exit"))
        {
            ExitGame();
        }
    }

    // Start game 
    public void StartGame()
    {
        SceneManager.LoadScene("overworld/scenes/overworld", LoadSceneMode.Single);
    }

    // FIXME:
    // We need to brainstorm ideas for this!!
    public void Settings()
    {
        audioMenuSettings.MainMenuSettings();
    }

    // Exits game
    public void ExitGame()
    {
        #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
