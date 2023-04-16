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
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StretchMechanic : MonoBehaviour
{
    [Tooltip("How much the player can stretch as the capsule's height.")]
    [SerializeField]
    private float maxHeight = 4.0f;

    [Tooltip("How small the player can stretch back to as the mesh's min height.")]
    [SerializeField]
    private float minHeight = 1.0f;

    [Tooltip("How fast the player increases in height and decreases in height.")]
    [SerializeField]
    private float stretchSpeed = 1.0f;

    [Tooltip("The player collision that stretches.")]
    [SerializeField]
    private CapsuleCollider mainCollision;
    [SerializeField]
    private CapsuleCollider collisionFix;

    [Tooltip("The visual instance of the player that will stretch.")]
    [SerializeField]
    Transform meshInstance;

    [Tooltip("Needed to connect jumping event signal.")]
    [SerializeField]
    private PlayerMovement playerMovement;

    public readonly UnityEvent EventStretchBegin = new();
    public readonly UnityEvent EventStretchEnd = new();
    public readonly UnityEvent EventShrinkBegin = new();
    public readonly UnityEvent EventShrinkEnd = new();

    public bool ReadyToJump
    {
        get => readyToJump;
        set => readyToJump = value;
    }
    public bool IsGrown
    {
        get => isGrown;
    }

    private bool isGrown = false;

    private bool readyToJump = false;

    private bool stretchBeginGate = true;
    private bool stretchEndGate = true;
    private bool shrinkBeginGate = false;
    private bool shrinkEndGate = false;
    private bool stretchInput;

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
        if (stretchInput) IncreaseHeight();
        else DecreaseHeight();
    }
    private void OnStretchBegin()
    {
        Debug.Log("Stretch began.");
    }
    private void OnStretchEnd()
    {
        readyToJump = true;
        Debug.Log("Stretch ended.");
    }
    private void OnShrinkBegin()
    {
        //playerMovement.EventJump?.Invoke();
        Debug.Log("Shrink began.");

        isGrown = false;
    }
    private void OnShrinkEnd()
    {
        //StartCoroutine(GrowJumpDelay());
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

            // Scales mesh in only the Y direction using the same growing collider logic
            Vector3 newScale = meshInstance.localScale;

            // Max height 
            if (newScale.y < maxHeight)
            {
                newScale.x += calc;
                newScale.y += calc;
                newScale.z += calc;
                meshInstance.localScale = newScale;
            }

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

            // Retrieve current transform and scale down to 1 
            Vector3 newScale = meshInstance.localScale;

            // Stop shrinking once back at scale factor of 1 
            if (newScale.y > minHeight)
            {
                newScale.x -= calc;
                newScale.y -= calc;
                newScale.z -= calc;
                meshInstance.localScale = newScale;
            }
            // Floating point numbers will happen as newScale approaches 1 (minHeight)
            // Set newScale to 1 once newScale < 1
            else
            {
                newScale.x = minHeight;
                newScale.y = minHeight;
                newScale.z = minHeight;
                meshInstance.localScale = newScale;
            }

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
    private IEnumerator GrowJumpDelay()
    {
        yield return new WaitForSeconds(1.0f);
        readyToJump = true;
    }
    public void SetInput(bool state)
    {
        stretchInput = state;
    }
}
