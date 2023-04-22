using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // New game loads the index of the first level in Build settings
    public void NewGame()
    {
        SceneManager.LoadScene("overworld/scenes/overworld", LoadSceneMode.Single);
    }

    // This will need a fix!!!
    // Save latest scene user achieved and load that scene
    public void ContinueGame()
    {
        SceneManager.LoadScene("overworld/scenes/overworld", LoadSceneMode.Single);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // FIXME:
    // We need to brainstorm ideas for this!!
    public void Settings()
    {
        
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
