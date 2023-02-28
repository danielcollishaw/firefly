
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public readonly UnityEvent EventJump = new();

    [SerializeField]
    private CapsuleCollider playerCollision;
    [SerializeField]
    private CapsuleCollider playerCollisionFix;

    public float BaseSpeed { get; set; } = 10f;
    public float BaseHeight
    {
        get => baseHeight;
    }

    private float baseHeight;

    public Rigidbody player;
    public Animator animator;

    public float speedDecrement = 10f;
    public float jumpSpeed = 20f;
    public float moveLimiter = 0.7f;
    public float gravityScale = 3f;
    
    private float originalSpeed;
    private float x, z;
    private bool jumped;

    void Start() 
    {
        originalSpeed = BaseSpeed;
        baseHeight = playerCollision.height;
        
        
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

        NormalizeSpeed();
    }

    // FixedUpdate is called on physic updates
    private void FixedUpdate()
    {
        // Makes gravity stronger
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
            SetVelocity(new Vector3(x * BaseSpeed, GetVelocity().y, z * BaseSpeed));
            SetLookRotation(new Vector3(-x, 0f, -z));
        }
        else
        {
            animator.SetBool("IsRunning", false);
            SetSpeed(originalSpeed);
        }

        if (jumped)
        {
            SetVelocity(GetVelocity() + new Vector3(0, jumpSpeed, 0));
            jumped = false;
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }
    }

    public void SetSpeed(float s)
    {
        BaseSpeed = s;
    }
    public void MultHeight(float height)
    {
        playerCollision.height *= height;
        playerCollisionFix.height *= height;
    }

    public Vector3 GetVelocity()
    {
        return player.velocity;
    }

    public void SetVelocity(Vector3 velocity)
    {
        player.velocity = velocity;
    }

    private void SetLookRotation(Vector3 direction)
    {
        player.rotation = Quaternion.LookRotation(direction);
    }

    private void NormalizeSpeed()
    {
        if (BaseSpeed > originalSpeed)
        {
            BaseSpeed -= speedDecrement * Time.deltaTime;
        }
        else if (BaseSpeed < originalSpeed)
        {
            BaseSpeed = originalSpeed;
        }
    }

    private void OnJump()
    {
        jumped = true;
    }

    // Checks if player is grounded by shooting a sphere to check for collisions
    private bool OnGround()
    {
        Vector3 offset = new Vector3(0, .7f, 0)  / (playerCollision.height / baseHeight);
        RaycastHit hit;
        float radius = .5f;
        float dist = .75f;

        //return Physics.Raycast(transform.position + offset, Vector3.down, dist);
        
        return Physics.SphereCast(transform.position + offset, radius, Vector3.down, out hit, dist);
    }

    // Visualize raycast when gizmos show is selected
    void OnDrawGizmos()
    {
        
        Vector3 offset = new Vector3(0, .7f, 0)  / (playerCollision.height / baseHeight);
        float radius = .5f;
        float dist = .75f;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset + Vector3.down * dist, radius);
    }
}
