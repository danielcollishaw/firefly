using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Firefly Interact SFX")]
    [field: SerializeField] public EventReference Firefly_Interact { get; private set; }

    [field: Header("Grow SFX")]
    [field: SerializeField] public EventReference Grow { get; private set; }

    [field: Header("Shrink SFX")]
    [field: SerializeField] public EventReference Shrink { get; private set; }

    [field: Header("UI Click SFX")]
    [field: SerializeField] public EventReference UI_Click { get; private set; }


    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one audio manager in scene.");
        }
        instance = this;
    }
    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }
}
