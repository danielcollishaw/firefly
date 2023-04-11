using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GravityField : MonoBehaviour
{
    public float force = 75f;

	protected Collider m_collider;

    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<Collider>();
		m_collider.isTrigger = true;
    }


    //  if(other.gameObject.CompareTag("Collectable"))

    void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (other.TryGetComponent<PlayerMovement>(out var player))
			{
				if (player.OnGround())
				{
					player.SetVelocity(Vector3.zero);
					//player.SetVelocity() = Vector3.zero;
				}

				// Vector3 currentVelocity = player.GetVelocity();
				player.SetVelocity(transform.up * force * Time.deltaTime);

				//player.velocity += transform.up * force * Time.deltaTime;
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
