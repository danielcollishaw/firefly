using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioMenuSettings : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject menu;

    [Header("Player")]
    [SerializeField] private PlayerMovement playerMovement;

    private bool InMenu = false;
    private int level;

    private void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        menu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (level > 0 && Input.GetButtonDown("MenuToggle"))
        {
            ToggleAudioMenu();
            InMenu = true;
        }

        if(InMenu && Input.GetButtonDown("Back"))
        {
            ToggleAudioMenu();
        }
    }

    private void ToggleAudioMenu()
    {
        if(menu.activeInHierarchy)
        {
            menu.SetActive(false);
            playerMovement.MovementEnabled();
        }
        else
        {
            menu.SetActive(true);
            // highlight first selected slider
            playerMovement.MovementDisabled();
        }
    }

    public void MainMenuSettings()
    {
        if (menu.activeInHierarchy)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
            // highlight first selected slider
        }
    }
}
