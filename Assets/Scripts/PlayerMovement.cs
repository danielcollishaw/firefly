using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public readonly UnityEvent EventJump = new UnityEvent();

    public Rigidbody player;
    public float speed = 5f;
    public float jumpSpeed = 20f;
    public float moveLimiter = 0.7f;
    public float gravityScale = 3f;
    
    private float x, z;
    private bool jumped;

    void Start()
    {
        EventJump.AddListener(OnJump);
    }

    // Update is called once per frame
    void Update()
    {
        // Takes current input direction vector values [-1, 1];
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        if (!jumped)
            jumped = Input.GetButtonDown("Jump");
    }

    // FixedUpdate is called on physic updates
    private void FixedUpdate()
    {
        player.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
         // Limit diagonal movement by avoiding compounding of speed when the a keyboard input.
        if (x != 0 && z != 0)
        {
            x *= moveLimiter;
            z *= moveLimiter;
        }

        // Applies velocity change the reason it is encapsulated is because we do not want to set velocity instantly to zero
        // we want friction to handle this for smoother feeling (See rigid body drag)
        if (x != 0 || z != 0)
        {
            player.velocity = new Vector3(x * speed, player.velocity.y, z * speed);
        }

        if (jumped)
        {
            player.velocity = player.velocity + new Vector3(0, jumpSpeed, 0);
            jumped = false;
        }
    }
    private void OnJump()
    {
        jumped = true;
    }
}
