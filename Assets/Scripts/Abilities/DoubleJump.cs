#region Copyright
/*---------------------------------------------------------------*/
/*        File: DoubleJump.cs                                    */
/*        Firefly                                                */
/*        AI For Game Programming - CAP 4053                     */
/*        Copyright (c) 2023 Serenity Studios                    */
/*        All rights reserved.                                   */
/*        Made with love, by Justin Sasso.                       */
/*---------------------------------------------------------------*/
#endregion

using System;
using UnityEngine;


[CreateAssetMenu]
public class DoubleJump : Ability
{
    public override void Activate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        movement.ToggleDoubleJump(true);
    }
    public override void Deactivate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        movement.ToggleDoubleJump(false);
    }
}
