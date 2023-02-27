
using System;
using UnityEngine;
using System.Collections.Generic;

public class AbilityHolder : MonoBehaviour
{
    private const string POWER_BUTTON = "ActivatePower";

    [SerializeField]
    private Ability ability;

    private float cooldownTime; 
    private float activeTime; 

    // Which state ability is currently in.
    enum AbilityState 
    {
        Activate, 
        Deactivate,
        Active,
        Cooldown,
    }

    private AbilityState state = AbilityState.Activate; 

    // Update is called once per frame
    void Update()
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
            case AbilityState.Active:
                // If the ability has no cooldown, set activeTime to -1.
                // That means the ability stops only when the player 
                // lifts the specified button.
                if (activeTime == -1)
                {
                    if (Input.GetButtonUp(POWER_BUTTON))
                    {
                        state = AbilityState.Deactivate;
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
}
