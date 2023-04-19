using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyPathing : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public float offset = 10;
    public float speed = 5;
    public GameObject player;

    private Vector3 velocity = Vector3.zero;
    private Vector3 returnPoint = Vector3.zero;
    private bool targetPlayer = false;
    private bool returning = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        AssignDestination();

        // On start, play idle sound 
        GetComponent<FMODUnity.StudioEventEmitter>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPlayer)
        {
            SeekPlayer();
            return;
        }

        if (returning)
        {
            SeekReturn();
            return;
        }

        if (agent.velocity == Vector3.zero)
            AssignDestination();
    }

    public void EnableTargetting()
    {
        targetPlayer = true;
        velocity = Vector3.zero;
        agent.enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }

    public void Return(Vector3 pos)
    {
        targetPlayer = false;
        returning = true;
        returnPoint = pos;
    }

    private void SeekPlayer()
    {
        Vector3 height = new Vector3(0, 2.5f, 0);
        if ((transform.position - (player.transform.position + height)).magnitude <= 3)
        {
            return;
        }
 
        UpdateVelocity(player.transform.position + height);
        SetLookRotation(velocity);
        transform.position += velocity * Time.deltaTime;
    }

    private void SeekReturn()
    {
        if ((transform.position - returnPoint).magnitude <= 1)
        {
            returning = false;
            agent.enabled = true;
            gameObject.GetComponent<SphereCollider>().enabled = true;

            // Play idle sound, firefly no longer active
            GetComponent<FMODUnity.StudioEventEmitter>().Play();
            return;
        }

        UpdateVelocity(returnPoint);
        SetLookRotation(velocity);
        transform.position += velocity * Time.deltaTime;
    }

    private void SetLookRotation(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void UpdateVelocity(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        Vector3 steer = desired.normalized * speed - velocity;
        velocity = velocity + steer * 0.005f;
    }

    private void AssignDestination()
    {
        // Causing an error to appear in the console every frame, so there's a bug here.
        Vector3 target = RandomNavSphere(transform.position, offset, -1);

        if (target != null)
            agent.destination = target;
    }

    private Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        
        randomDirection += origin;
        
        UnityEngine.AI.NavMeshHit navHit;        
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
