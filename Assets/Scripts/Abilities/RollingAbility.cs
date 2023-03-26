#region Copyright
/*---------------------------------------------------------------*/
/*        File: RollingAbility.cs                                */
/*        Firefly                                                */
/*        AI For Game Programming - CAP 4053                     */
/*        Copyright (c) 2023 Serenity Studios                    */
/*        All rights reserved.                                   */
/*        Made with love, by Justin Sasso.                       */
/*---------------------------------------------------------------*/
#endregion

using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class RollingAbility : Ability
{
    [SerializeField]
    private float heightMultiplier = 0.4f;
    [SerializeField]
    private float speedMultiplier = 4.0f;

    private float activeSpeed;
    private float activeHeight;

    public override void Activate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        Animator animator = parent.GetComponent<Animator>();
        animator.SetBool("IsRolling", true);
       
        activeSpeed = movement.BaseSpeed;
        activeHeight = movement.BaseHeight;
        movement.SetSpeed(activeSpeed * speedMultiplier);
        movement.MultHeight(heightMultiplier);

        //Debug.Log($"Height: {movement.BaseHeight}, Speed: {movement.BaseSpeed}"); // DEBUG
    }
    public override void Deactivate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        Animator animator = parent.GetComponent<Animator>();
        animator.SetBool("IsRolling", false);
        
        activeSpeed = movement.BaseSpeed;
        activeHeight = movement.BaseHeight;
        movement.SetSpeed(activeSpeed / speedMultiplier);
        movement.MultHeight(1 / heightMultiplier);
        //Debug.Log($"Height: {movement.BaseHeight}, Speed: {movement.BaseSpeed}"); // DEBUG
    }
}
