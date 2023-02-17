using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public readonly UnityEvent EventJump = new UnityEvent();

    public Rigidbody player;
    public Animator animator;
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

        if (Input.GetButtonDown("Jump") && OnGround())
            jumped = true;
            
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
            animator.SetBool("IsRunning", true);
            player.velocity = new Vector3(x * speed, player.velocity.y, z * speed);
            player.rotation = Quaternion.LookRotation(new Vector3(-x, 0f, -z));
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (jumped)
        {
            player.velocity = player.velocity + new Vector3(0, jumpSpeed, 0);
            jumped = false;
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }
    }

    private void OnJump()
    {
        jumped = true;
    }

    // Checks if player is grounded by shooting a sphere to check for collisions
    private bool OnGround()
    {
        Vector3 offset = new Vector3(0, .7f, 0);
        RaycastHit hit;
        float radius = .5f;
        float dist = .75f;

        //return Physics.Raycast(transform.position + offset, Vector3.down, dist);
        
        return Physics.SphereCast(transform.position + offset, radius, Vector3.down, out hit, dist);
    }

    // Visualize raycast when gizmos show is selected
    void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(0, .7f, 0);
        float radius = .5f;
        float dist = .75f;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset + Vector3.down * dist, radius);
    }
}
