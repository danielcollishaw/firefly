
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using FMOD.Studio;


public class PlayerMovement : MonoBehaviour
{
    public readonly UnityEvent EventJump = new();

    [SerializeField]
    private CapsuleCollider playerCollision;
    [SerializeField]
    private CapsuleCollider playerCollisionFix;

    [SerializeField]
    private AbilityHolder abilityHolder;
    [SerializeField]
    private StretchMechanic stretchMechanic;

    public float BaseSpeed { get; set; } = 15f;
    public float BaseHeight
    {
        get => baseHeight;
    }

    private float baseHeight;

    public Rigidbody player;
    public Animator animator;
    public GameObject playerCamera;

    public float speedDecrement = 10f;
    public float jumpSpeed = 20f;
    public float moveLimiter = 0.7f;
    public float gravityScale = 3f;
    public float friction = 0.9f;
    
    private float originalSpeed;
    private float glideRate = 0;
    private float x, z;

    private bool jumped = false;
    private bool canJump = true;

    private bool doubleJumped = false;
    private bool canDoubleJump = false;
    private bool doubleJumpDelay = true;
    private bool doubleJumpIsOnGround = true;

    private bool glide = false;

    // Audio
    private EventInstance playerFootsteps;
    private EventInstance glideWind;

    void Start() 
    {
        originalSpeed = BaseSpeed;
        baseHeight = playerCollision.height;
       
        EventJump.AddListener(OnJump);

        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.runSFX);
        glideWind = AudioManager.instance.CreateEventInstance(FMODEvents.instance.GlideSFX);
    }

    // Update is called once per frame
    void Update()
    {
        // Takes current input direction vector values [-1, 1];
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            UpdateJumpSound();

            if (OnGround())
            {
                jumped = true; 
            }
            else
            {
                doubleJumped = true;
            }
        }

        if (doubleJumpDelay)
        {
            if (OnGround())
            {
                doubleJumpIsOnGround = true;
            }
        }
            
        if (OnGround()) 
        {
            SetGlide(false);
            animator.SetBool("IsGliding", false);
        }
            

        NormalizeSpeed();
    }

    // FixedUpdate is called on physic updates
    private void FixedUpdate()
    {
        // Check if player is moving to start or stop footstep SFX
        UpdateRunningSound();
        UpdateGlideSound();

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
            Vector3 move = new Vector3(x * BaseSpeed, 0, z * BaseSpeed);
            move = playerCamera.transform.TransformDirection(move);
            move = Vector3.ProjectOnPlane(move, Vector3.up);
            move = move + new Vector3(0, GetVelocity().y, 0);
            SetVelocity(move);
            SetLookRotation(new Vector3(-move.x, 0f, -move.z));

            animator.SetBool("IsRunning", true);
        }
        else
        {
            SetVelocity(new Vector3(GetVelocity().x * friction, GetVelocity().y, GetVelocity().z * friction));
            animator.SetBool("IsRunning", false);
            SetSpeed(originalSpeed);
        }

        if (jumped && canJump)
        {
            float multiplier = 1.0f;

            if (stretchMechanic.ReadyToJump)
            {
                stretchMechanic.ReadyToJump = false;
                abilityHolder.State = AbilityHolder.AbilityState.AbilityChange;
                multiplier = 1.5f;
            }

            SetVelocity(GetVelocity() + new Vector3(0, jumpSpeed * multiplier, 0));
            jumped = false;
            canJump = false;
            animator.SetBool("IsJumping", true);
            StartCoroutine(JumpDelay());
        }
        //else if (canDoubleJump && doubleJumped && doubleJumpDelay)
        else if (canDoubleJump && doubleJumped && doubleJumpDelay && doubleJumpIsOnGround)
        {
            SetVelocity(GetVelocity() + new Vector3(0, jumpSpeed * 1.15f, 0));
            doubleJumped = false;
            doubleJumpDelay = false;
            doubleJumpIsOnGround = false;
            animator.SetBool("IsJumping", true);
            StartCoroutine(StartDoubleJumpDelay());
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }

        if (glide)
        {
            if (!(player.velocity.y > 0))
                player.AddForce(Physics.gravity * gravityScale * -glideRate);    
        }
    }
    private IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(1.0f); // Change this delay to suit your needs
        canJump = true;
    }
    private IEnumerator StartDoubleJumpDelay()
    {
        yield return new WaitForSeconds(0.5f);
        doubleJumpDelay = true;
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

    public void SetGlide(bool state)
    {
        glide = state;
    }

    public bool getGlide()
    {
        return glide;
    }

    public void SetGlideRate(float rate)
    {
        glideRate = rate;
    }
    public void ToggleDoubleJump(bool activated)
    {
        canDoubleJump = activated;
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

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Platform"))
            gameObject.transform.parent = col.transform;
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag.Equals("Platform"))
            gameObject.transform.parent = null;
    }

    // Checks if player is grounded by shooting a sphere to check for collisions
    public bool OnGround()
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

    private void UpdateRunningSound()
    {
        // Start footsteps audio event if the player is moving and on the ground
        if ((x != 0 || z != 0) && OnGround())
        {
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);

            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }
        // Stop event if otherwise
        else
        {
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void UpdateGlideSound()
    {
        // Start wind audio event if the player is gliding
        if (getGlide())
        {
            PLAYBACK_STATE playbackState;
            glideWind.getPlaybackState(out playbackState);

            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                glideWind.start();
            }
        }
        // Stop event if otherwise
        else
        {
            glideWind.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
    private void UpdateJumpSound()
    {
        // Start wind audio event if the player is gliding
        if (OnGround() && !jumped && !stretchMechanic.IsGrown)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.JumpSmallSFX, this.transform.position);
        }
        else if (OnGround() && !jumped && stretchMechanic.ReadyToJump)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.JumpBigSFX, this.transform.position);
        }
    }
}
