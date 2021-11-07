using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour
{
	public Transform hatPivot;
	public List<GameObject> hats = new List<GameObject>();

	bool destroying;

	int index;

	private void Start()
	{
		index = Random.Range(0, hats.Count);

		GameObject.Instantiate<GameObject>(hats[index], hatPivot);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (destroying) return;

 		if (collision.collider.gameObject.CompareTag("NPC"))
		{
			NPC target = collision.collider.gameObject.GetComponent<NPC>();
			
			if (target.hasHat)
			{
				target.PutOnHat(hats[index]);
				Destroy(this);
			}
		}	
		else
		{
			destroying = true;
			Invoke("DestroySoon", 5f);
		}
	}

	private void DestroySoon()
	{
		Destroy(this);
	}
}
