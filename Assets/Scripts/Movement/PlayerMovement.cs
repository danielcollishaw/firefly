using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public readonly UnityEvent EventJump = new UnityEvent();

    public Rigidbody player;
    public Animator animator;

    private int collectableCount; 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public int numberOfCollectableFireflies;
    public float speed = 5f;
    public float speedDecrement = 10f;
    public float jumpSpeed = 20f;
    public float moveLimiter = 0.7f;
    public float gravityScale = 3f;
    
    private float originalSpeed;
    private float x, z;
    private bool jumped;

    void Start() 
    {
        originalSpeed = speed;
        
        player.GetComponent<Rigidbody>();
        EventJump.AddListener(OnJump);

        collectableCount = 0;

        SetCountText();
        winTextObject.SetActive(false);
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

    void SetCountText() 
    {
        countText.text = "Count: " + collectableCount.ToString();
        if(collectableCount >= numberOfCollectableFireflies) 
        {
            winTextObject.SetActive(true);
        }
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
            SetVelocity(new Vector3(x * speed, GetVelocity().y, z * speed));
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

    private void OnTriggerEnter(Collider other)
    {
        // will be called when the player first touches a trigger collider (i.e the collectables)
        if(other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            collectableCount++;
            Debug.Log(collectableCount);
            //called everytime it touches collider 

            SetCountText();
        }
        // using TAGS to correctly disable the collectables and not other objects 
    }

    public void SetSpeed(float s)
    {
        speed = s;
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
        if (speed > originalSpeed)
        {
            speed -= speedDecrement * Time.deltaTime;
        }
        else if (speed < originalSpeed)
        {
            speed = originalSpeed;
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
