#region Copyright
/*---------------------------------------------------------------*/
/*        File: StretchMechanic.cs                               */
/*        Firefly                                                */
/*        AI For Game Programming - CAP 4053                     */
/*        Copyright (c) 2023 Serenity Studios                    */
/*        All rights reserved.                                   */
/*        Made with love, by Justin Sasso.                       */
/*---------------------------------------------------------------*/
#endregion

using System;
using UnityEngine;
using UnityEngine.Events;

public class StretchMechanic : MonoBehaviour
{
    [Tooltip("Assign the stretch action to an input mapping.")]
    [SerializeField]
    private string stretchAction;

    [Tooltip("How much the player can stretch as the capsule's height.")]
    [SerializeField]
    private float maxHeight = 8.0f;

    [Tooltip("How fast the player increases in height and decreases in height.")]
    [SerializeField]
    private float stretchSpeed = 1.5f;

    [Tooltip("The player collision that stretches.")]
    [SerializeField]
    private CapsuleCollider mainCollision;
    [SerializeField]
    private CapsuleCollider collisionFix;

    [Tooltip("The visual instance of the player that will stretch.")]
    [SerializeField]
    private MeshFilter meshInstance;

    [Tooltip("Needed to connect jumping event signal.")]
    [SerializeField]
    private PlayerMovement playerMovement;

    public readonly UnityEvent EventStretchBegin = new();
    public readonly UnityEvent EventStretchEnd = new();
    public readonly UnityEvent EventShrinkBegin = new();
    public readonly UnityEvent EventShrinkEnd = new();

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
        originalHeight = mainCollision.height;

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

        bool stretchInput = Input.GetKey("p");
        if (stretchInput) IncreaseHeight();
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
        playerMovement.EventJump?.Invoke();
        Debug.Log("Shrink began.");
    }
    private void OnShrinkEnd()
    {
        stretchingAndShrinking = false;
        Debug.Log("Shrink ended.");
    }
    private void IncreaseHeight()
    {
        if (mainCollision.height <= maxHeight)
        //if (!Mathf.Approximately(Collision.height, MaxHeight))
        {
            stretchEndGate = true;

            if (stretchBeginGate)
            {
                stretchBeginGate = false;
                EventStretchBegin.Invoke();
            }

            // I need to check if this is necessary since Update()
            // might already be running at a fixed rate.
            float calc = stretchSpeed * Time.fixedDeltaTime;

            mainCollision.height += calc;
            collisionFix.height += calc;

            stretchOffset = (float)Extend.MapRangeClamped(mainCollision.height, originalHeight, maxHeight);

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
        if (mainCollision.height >= originalHeight)
        //if (!Mathf.Approximately(Collision.height, originalHeight))
        {
            shrinkEndGate = true;

            if (shrinkBeginGate)
            {
                shrinkBeginGate = false;
                EventShrinkBegin.Invoke();
            }

            float calc = stretchSpeed * Time.fixedDeltaTime;
            mainCollision.height -= calc;
            collisionFix.height -= calc;

            stretchOffset = (float)Extend.MapRangeClamped(mainCollision.height, originalHeight, maxHeight);

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
