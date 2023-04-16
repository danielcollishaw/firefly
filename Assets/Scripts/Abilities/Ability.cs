using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public float CooldownTime
    {
        get => cooldownTime;
    }
    public float ActiveTime
    {
        get => activeTime;
    }
   
    [SerializeField]
    private string abilityName;
    [SerializeField]
    private float cooldownTime;
    [SerializeField]
    private float activeTime;

    public virtual void Activate(GameObject parent) {  }
    public virtual void Deactivate(GameObject parent) {  }
    public virtual void AbilityChange(GameObject parent, int id) {  }
}
