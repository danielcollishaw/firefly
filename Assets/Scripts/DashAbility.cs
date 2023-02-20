using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashAbility : Ability
{
    public float dashVelocity;
    public float speed = 5f;

    public override void Activate(GameObject parent)
    {
       PlayerMovement movement = parent.GetComponent<PlayerMovement>();
       Rigidbody player = parent.GetComponent<Rigidbody>();
      
       player.velocity = movement.movementInput.normalized * dashVelocity;


    }
}
