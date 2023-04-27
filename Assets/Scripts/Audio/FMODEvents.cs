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

    [field: Header("Roll SFX")]
    [field: SerializeField] public EventReference RollSFX { get; private set; }

    [field: Header("Time trial SFX")]
    [field: SerializeField] public EventReference TimeTrial { get; private set; }

    [field: Header("Pressure Plate SFX")]
    [field: SerializeField] public EventReference PressurePlate { get; private set; }

    [field: Header("UI Click SFX")]
    [field: SerializeField] public EventReference UI_ClickSFX { get; private set; }

    [field: Header("Gravity Field SFX")]
    [field: SerializeField] public EventReference GravityField { get; private set; }

    [field: Header("Death SFX")]
    [field: SerializeField] public EventReference DeathSFX { get; private set; }

    // Music for levels
    [field: Header("Menu Music")]
    [field: SerializeField] public EventReference MenuMusic { get; private set; }

    [field: Header("OverWorld Music")]
    [field: SerializeField] public EventReference OverWorldMusic { get; private set; }

    [field: Header("Level 1 Music")]
    [field: SerializeField] public EventReference Level1Music { get; private set; }

    [field: Header("Level 2 Music")]
    [field: SerializeField] public EventReference Level2Music { get; private set; }

    [field: Header("Level 3 Music")]
    [field: SerializeField] public EventReference Level3Music { get; private set; }

    [field: Header("Level 4 Music")]
    [field: SerializeField] public EventReference Level4Music { get; private set; }

    [field: Header("Level 5 Music")]
    [field: SerializeField] public EventReference Level5Music { get; private set; }

    [field: Header("Level 6 Music")]
    [field: SerializeField] public EventReference Level6Music { get; private set; }

    [field: Header("Level 7Music")]
    [field: SerializeField] public EventReference Level7Music { get; private set; }

    [field: Header("Level 8Music")]
    [field: SerializeField] public EventReference Level8Music { get; private set; }

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