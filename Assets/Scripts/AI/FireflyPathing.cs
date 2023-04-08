using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyPathing : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public float randomDist = 10;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity == Vector3.zero)
        {
            GoToRandomDestination();
        }
    }

    private void GoToRandomDestination()
    {
        // Causing an error to appear in the console every frame, so there's a bug here.
        //agent.destination = RandomNavSphere(transform.position, randomDist, -1);   
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
