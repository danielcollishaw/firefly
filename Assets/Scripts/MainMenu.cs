using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // New game loads the index of the first level in Build settings
    public void NewGame()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.UI_Click, this.transform.position);
        SceneManager.LoadScene(1);
    }

    // This will need a fix!!!
    // Save latest scene user achieved and load that scene
    public void ContinueGame()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.UI_Click, this.transform.position);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // FIXME:
    // We need to brainstorm ideas for this!!
    public void Settings()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.UI_Click, this.transform.position);
    }

    // Exits game
    public void ExitGame()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.UI_Click, this.transform.position);
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
        #endif
    }
}
