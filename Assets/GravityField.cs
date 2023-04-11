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

    // protected virtual void OnTriggerStay(Collider other)
	// 	{
	// 		if (other.CompareTag(GameTags.Player))
	// 		{
	// 			if (other.TryGetComponent<Player>(out var player))
	// 			{
	// 				if (player.isGrounded)
	// 				{
	// 					player.verticalVelocity = Vector3.zero;
	// 				}

	// 				player.velocity += transform.up * force * Time.deltaTime;
	// 			}
	// 		}
	// 	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
