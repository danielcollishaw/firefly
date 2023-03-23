using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Glide : Ability
{
    public float glideRate;

    public override void Activate(GameObject parent)
    {
        Animator animator = parent.GetComponent<Animator>();
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        animator.SetBool("IsGliding", true);
        movement.SetGlide(true);
        movement.SetGlideRate(glideRate);
    }

    public override void Deactivate(GameObject parent)
    {
        Animator animator = parent.GetComponent<Animator>();
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        animator.SetBool("IsGliding", false);
        movement.SetGlide(false);
    }
}
