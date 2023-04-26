using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenuSettings : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject firstSlider;
    [Header("Player")]
    [SerializeField] private PlayerMovement playerMovement;

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
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(firstSlider, new BaseEventData(eventSystem));
        }
    }
}
