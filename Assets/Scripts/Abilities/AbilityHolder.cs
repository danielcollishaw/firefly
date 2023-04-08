
using System;
using UnityEngine;
using System.Collections.Generic;

public class AbilityHolder : MonoBehaviour
{
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

    private GameObject firefly = null;

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
            ability = roll;
            // col.gameObject.transform.parent = gameObject.transform;
            wasFirefly = true;
        }
        else if (col.gameObject.tag.Equals("Glide"))
        {
            ability = glide;
            // col.gameObject.transform.parent = gameObject.transform;
            wasFirefly = true;
        }
        else if (col.gameObject.tag.Equals("Grow"))
        {
            ability = grow;
            wasFirefly = true;
        }
        else if (col.gameObject.tag.Equals("DoubleJump"))
        {
            ability = doubleJump;
            wasFirefly = true;
        }

        if (!wasFirefly) return;

        // Freeing past firefly
        removeFirefly();
        
        // Storing new one
        col.gameObject.SetActive(false);
        firefly = col.gameObject;
    }

    private void removeFirefly()
    {
        if (firefly != null)
        {
            firefly.SetActive(true);
        }

        firefly = null;
    }

    public void removePower()
    {
        removeFirefly();
        ability = none;
    }
}
