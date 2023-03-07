using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Glide : Ability
{
    public float glideRate;

    public override void Activate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        movement.SetGlide(true);
        movement.SetGlideRate(glideRate);
    }

    public override void Deactivate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        movement.SetGlide(false);
    }
}
