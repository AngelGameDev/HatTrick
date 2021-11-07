using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatTrigger : MonoBehaviour
{
	public GameObject hatProto;

	private void OnTriggerEnter(Collider other)
	{
		NPC target = other.gameObject.GetComponent<NPC>();

		if (!target.hasHat)
		{ 
			target.PutOnHat(hatProto);
			Destroy(transform.parent.gameObject);
			Destroy(this);
			
		}
	}
}
