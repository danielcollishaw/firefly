#region Copyright
/*---------------------------------------------------------------*/
/*        File: StretchMechanic.cs                               */
/*        Firefly                                                */
/*        AI For Game Programming - CAP 4053                     */
/*        Copyright © 2023 Serenity Studios                      */
/*        All rights reserved.                                   */
/*        Made with love, by Justin Sasso.                       */
/*---------------------------------------------------------------*/
#endregion

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class StretchMechanic : MonoBehaviour
{
    [Tooltip("Assign the stretch action to an input mapping.")]
    public InputAction stretchAction;

    [Tooltip("How much the player can stretch as the capsule's height.")]
    public float MaxHeight = 8.0f;

    [Tooltip("How fast the player increases in height and decreases in height.")]
    public float StretchSpeed = 1.0f;

    [Tooltip("The player collision that stretches.")]
    public CapsuleCollider Collision;

    [Tooltip("The visual instance of the player that will stretch.")]
    public MeshFilter MeshInstance;

    private float originalHeight;

    void Start()
    {
        originalHeight = Collision.height;
        stretchAction.Enable();

        Debug.Log("Stretch mechanic equipped!");
    }
    void Update()
    {
        // When the stretching value is 0, the button is not being pressed.
        // When it's 1, the button is being pressed.
        float stretching = stretchAction.ReadValue<float>();
        if (stretching == 1) IncreaseHeight();
        else DecreaseHeight();
    }
    private void IncreaseHeight()
    {
        if (!Mathf.Approximately(Collision.height, MaxHeight))
        {
            // I need to check if this is necessary since Update()
            // might already be running at a fixed rate.
            float calc = StretchSpeed * Time.fixedDeltaTime;
            Collision.height += calc;

            Bounds bounds = MeshInstance.mesh.bounds;
            Vector3 extents = bounds.extents;
            extents = new Vector3()
            {
                x = extents.x,
                y = extents.y + calc,
                z = extents.z
            };
            bounds.extents = extents;
            MeshInstance.mesh.bounds = bounds;

            Debug.Log($"Height is increasing: {Collision.height} |");
        }
    }
    private void DecreaseHeight()
    {
        if (!Mathf.Approximately(Collision.height, originalHeight))
        {
            float calc = StretchSpeed * Time.fixedDeltaTime;
            Collision.height -= calc;

            Bounds bounds = MeshInstance.mesh.bounds;
            Vector3 extents = bounds.extents;
            extents = new Vector3()
            {
                x = extents.x,
                y = extents.y - calc,
                z = extents.z
            };
            bounds.extents = extents;
            MeshInstance.mesh.bounds = bounds;

            Debug.Log($"Height is decreasing: {Collision.height} |");
        }
    } 
}
