using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Firefly Interact SFX")]
    [field: SerializeField] public EventReference FireflyInteractSFX { get; private set; }

    [field: Header("Firefly Interact 2 SFX")]
    [field: SerializeField] public EventReference FireflyInteract2SFX { get; private set; }

    [field: Header("Running SFX")]
    [field: SerializeField] public EventReference runSFX { get; private set; }

    [field: Header("Running Grown SFX")]
    [field: SerializeField] public EventReference runGrownSFX { get; private set; }

    [field: Header("Jumping Big SFX")]
    [field: SerializeField] public EventReference JumpBigSFX { get; private set; }

    [field: Header("Jumping Small SFX")]
    [field: SerializeField] public EventReference JumpSmallSFX { get; private set; }

    [field: Header("Grow SFX")]
    [field: SerializeField] public EventReference GrowSFX { get; private set; }

    [field: Header("Shrink SFX")]
    [field: SerializeField] public EventReference ShrinkSFX { get; private set; }

    [field: Header("Glide SFX")]
    [field: SerializeField] public EventReference GlideSFX { get; private set; }

    [field: Header("Time trial SFX")]
    [field: SerializeField] public EventReference TimeTrial { get; private set; }

    [field: Header("Pressure Plate SFX")]
    [field: SerializeField] public EventReference PressurePlate { get; private set; }

    [field: Header("UI Click SFX")]
    [field: SerializeField] public EventReference UI_ClickSFX { get; private set; }

    [field: Header("Gravity Field Ambient")]
    [field: SerializeField] public EventReference GravityField { get; private set; }

    [field: Header("Level 5 Music")]
    [field: SerializeField] public EventReference Level5Music { get; private set; }

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