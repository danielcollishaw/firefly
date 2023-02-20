using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody player;
    public float acceleration;
    public float normalAcceleration; 
    public Vector3 movementInput; 
    public float speed = 5f;
    public float jumpSpeed = 20f;
    public float moveLimiter = 0.7f;
    public float gravityScale = 3f;
    
    private float x, z;
    private bool jumped;

    void Start() {
        player = GetComponent<Rigidbody>();
        acceleration = normalAcceleration;
    }

    // Update is called once per frame
    void Update()
    {
        // Takes current input direction vector values [-1, 1];

        
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        movementInput = new Vector3(x, player.velocity.y, z) * speed; 
      //  player.AddForce(movementInput * speed);
        // Creates velocity in direction of value equal to keypress (WASD). rb.velocity.y deals with falling + jumping by setting velocity to y. 

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
}
