
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [Header("Audio Menu Settings")]
    [SerializeField] private AudioMenuSettings audioMenuSettings;

    [Header("Menu Settings")]
    [SerializeField] private GameObject menuSettings;
    [Header("Play Game Button")]
    [SerializeField] private GameObject PlayGame;
    private bool InSettings = false;

    private void Update()
    {
        
        if (InSettings &&  Input.GetButtonDown("Back"))
        {
            Settings();
            menuSettings.SetActive(true);
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(PlayGame, new BaseEventData(eventSystem));
            InSettings = false;
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
        InSettings = true;
        menuSettings.SetActive(false);
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
