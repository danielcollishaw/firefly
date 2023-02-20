#region Copyright
/*---------------------------------------------------------------*/
/*        File: StretchMechanic.cs                               */
/*        Firefly                                                */
/*        AI For Game Programming - CAP 4053                     */
/*        Copyright � 2023 Serenity Studios                      */
/*        All rights reserved.                                   */
/*        Made with love, by Justin Sasso.                       */
/*---------------------------------------------------------------*/
#endregion

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class StretchMechanic : MonoBehaviour
{
    [Tooltip("Assign the stretch action to an input mapping.")]
    public InputAction stretchAction;

    [Tooltip("How much the player can stretch as the capsule's height.")]
    public float MaxHeight = 8.0f;

    [Tooltip("How fast the player increases in height and decreases in height.")]
    public float StretchSpeed = 1.5f;

    [Tooltip("The player collision that stretches.")]
    public CapsuleCollider Collision;

    [Tooltip("The visual instance of the player that will stretch.")]
    public MeshFilter MeshInstance;

    [Tooltip("Needed to connect jumping event signal.")]
    public PlayerMovement PlayerMovement;

    public readonly UnityEvent EventStretchBegin = new UnityEvent();
    public readonly UnityEvent EventStretchEnd = new UnityEvent();
    public readonly UnityEvent EventShrinkBegin = new UnityEvent();
    public readonly UnityEvent EventShrinkEnd = new UnityEvent();

    public float StretchOffset
    {
        get => stretchOffset;
    }
    public bool StretchingAndShrinking
    {
        get => stretchingAndShrinking;
    }

    private float stretchOffset = 0.0f;
    private bool stretchingAndShrinking = false;

    private bool stretchBeginGate = true;
    private bool stretchEndGate = true;
    private bool shrinkBeginGate = false;
    private bool shrinkEndGate = false;

    private float originalHeight;

    void Start()
    {
        originalHeight = Collision.height;
        stretchAction.Enable();

        EventStretchBegin.AddListener(OnStretchBegin);
        EventStretchEnd.AddListener(OnStretchEnd);
        EventShrinkBegin.AddListener(OnShrinkBegin);
        EventShrinkEnd.AddListener(OnShrinkEnd);

        Debug.Log("Stretch mechanic equipped!");
    }
    void Update()
    {
        // When the stretching value is 0, the button is not being pressed.
        // When it's 1, the button is being pressed.
        float stretchInput = Input.GetAxisRaw("Power");
        if (stretchInput == 1) IncreaseHeight();
        else DecreaseHeight();

        if (stretchingAndShrinking)
        {
            //Debug.Log("Stretch offset: " + stretchOffset);
            // Can resize mesh here when something suitable is equipped.
            //Mesh mesh = MeshInstance.mesh;
            //mesh.RecalculateBounds
        }
    }
    private void OnStretchBegin()
    {
        stretchingAndShrinking = true;
        Debug.Log("Stretch began.");
    }
    private void OnStretchEnd()
    {
        Debug.Log("Stretch ended.");
    }
    private void OnShrinkBegin()
    {
        PlayerMovement.EventJump?.Invoke();
        Debug.Log("Shrink began.");
    }
    private void OnShrinkEnd()
    {
        stretchingAndShrinking = false;
        Debug.Log("Shrink ended.");
    }
    private void IncreaseHeight()
    {
        if (!Mathf.Approximately(Collision.height, MaxHeight))
        {
            stretchEndGate = true;

            if (stretchBeginGate)
            {
                stretchBeginGate = false;
                EventStretchBegin.Invoke();
            }

            // I need to check if this is necessary since Update()
            // might already be running at a fixed rate.
            float calc = StretchSpeed * Time.fixedDeltaTime;
            Collision.height += calc;

            stretchOffset = (float)Extend.MapRangeClamped(Collision.height, originalHeight, MaxHeight);

            //Debug.Log($"Height is increasing: {Collision.height} |");
        }
        else
        {
            if (stretchEndGate)
            {
                stretchEndGate = false;
                EventStretchEnd.Invoke();
            }

            stretchBeginGate = true;
        }
    }
    private void DecreaseHeight()
    {
        if (!Mathf.Approximately(Collision.height, originalHeight))
        {
            shrinkEndGate = true;

            if (shrinkBeginGate)
            {
                shrinkBeginGate = false;
                EventShrinkBegin.Invoke();
            }

            float calc = StretchSpeed * Time.fixedDeltaTime;
            Collision.height -= calc;

            stretchOffset = (float)Extend.MapRangeClamped(Collision.height, originalHeight, MaxHeight);

            //Debug.Log($"Height is decreasing: {Collision.height} |");
        }
        else
        {
            if (shrinkEndGate)
            {
                shrinkEndGate = false;
                EventShrinkEnd.Invoke();
            }

            shrinkBeginGate = true;
        }
    } 
}
