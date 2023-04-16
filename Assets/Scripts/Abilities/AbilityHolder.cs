
using System;
using UnityEngine;
using System.Collections.Generic;

public class AbilityHolder : MonoBehaviour
{
    public AbilityState State
    {
        get => state;
        set => state = value;
    }

    private const string POWER_BUTTON = "ActivatePower";

    [SerializeField]
    private Ability roll;
    [SerializeField]
    private Ability glide;
    [SerializeField]
    private Ability grow;
    [SerializeField]
    private Ability doubleJump;
    [SerializeField]
    private Ability none;
    [SerializeField]
    private Ability ability;

    [SerializeField]
    private StretchMechanic stretchMechanic;

    private GameObject firefly = null;
    private Vector3 returnPoint = Vector3.zero;

    private float cooldownTime; 
    private float activeTime; 

    // Which state ability is currently in.
    public enum AbilityState 
    {
        Activate, 
        Deactivate,
        AbilityChange,
        Active,
        Cooldown,
    }

    private AbilityState state = AbilityState.Activate;

    private void Start()
    {
        
    }
    // Update is called once per frame
    private void Update()
    {
        switch (state) 
        {
            case AbilityState.Activate:
                if (Input.GetButtonDown(POWER_BUTTON)) 
                {
                    ability.Activate(gameObject);
                    state = AbilityState.Active;
                    activeTime = ability.ActiveTime;
                }
                break;
            case AbilityState.Deactivate:
                ability.Deactivate(gameObject);
                state = AbilityState.Activate;
                break;
            case AbilityState.AbilityChange:
                ability.AbilityChange(gameObject, 0);
                state = AbilityState.Activate;
                break;
            case AbilityState.Active:
                // If the ability has no cooldown, set activeTime to -1.
                // That means the ability stops only when the player 
                // lifts the specified button.
                if (activeTime == -1)
                {
                    if (Input.GetButtonUp(POWER_BUTTON))
                    {
                        state = AbilityState.Cooldown;
                        cooldownTime = ability.CooldownTime;
                        ability.Deactivate(gameObject);
                    }
                }
                else if (activeTime > 0) 
                {
                    activeTime -= Time.deltaTime;
                }   
                else 
                {
                    state = AbilityState.Cooldown;
                    cooldownTime = ability.CooldownTime;
                    ability.Deactivate(gameObject);
                }
                break;
            case AbilityState.Cooldown:
                if (cooldownTime > 0) 
                {
                    cooldownTime -= Time.deltaTime;
                }
                else 
                {
                    state = AbilityState.Activate;
                }
                break;
        } 
    }
    private void OnTriggerEnter(Collider col)
    {
        bool wasFirefly = false;

        if (col.gameObject.tag.Equals("Roll"))
        {
            ability.Deactivate(gameObject);
            ability = roll;
            // col.gameObject.transform.parent = gameObject.transform;
            wasFirefly = true;
        }
        else if (col.gameObject.tag.Equals("Glide"))
        {
            ability.Deactivate(gameObject);
            ability = glide;
            // col.gameObject.transform.parent = gameObject.transform;
            wasFirefly = true;
        }
        else if (col.gameObject.tag.Equals("Grow"))
        {
            ability.Deactivate(gameObject);
            ability = grow;
            wasFirefly = true;
        }
        else if (col.gameObject.tag.Equals("DoubleJump"))
        {
            ability.Deactivate(gameObject);
            ability = doubleJump;
            wasFirefly = true;
        }

        if (!wasFirefly) return;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.FireflyInteract2SFX, this.transform.position);
        // Freeing past firefly
        RemoveFirefly();
        
        // Storing new one
        firefly = col.gameObject;
        returnPoint = firefly.transform.position;
        firefly.GetComponent<FireflyPathing>().EnableTargetting();
    }

    private void RemoveFirefly()
    {
        if (firefly != null)
        {
            firefly.GetComponent<FireflyPathing>().Return(returnPoint);
        }

        firefly = null;
    }

    public void RemovePower()
    {
        ability.Deactivate(gameObject);
        RemoveFirefly();
        ability = none;
    }
}
