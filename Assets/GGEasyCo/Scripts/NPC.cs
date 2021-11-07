using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
	public Transform hatSlot;
	public SpriteRenderer headBorder;

	public static int hatless;

	public List<GameObject> hats = new List<GameObject>();

    private NavMeshAgent refAgent;

	private bool isMovingToTarget;
	private Vector3 currentTarget;

	public bool hasHat;

	float timeNearTarget;

	private void Start()
	{
		headBorder.color = Color.red;

		refAgent = GetComponent<NavMeshAgent>();

		if (hasHat)
		{
			PutOnRandomHat();
		}
		else
		{
			hatless++;
			Debug.Log(hatless);
		}
	}

	private void LateUpdate()
	{
		if (!isMovingToTarget)
		{
			FindNewTarget();
		}

		if (Vector3.Distance(transform.position, currentTarget) < 3)
		{
			timeNearTarget += Time.deltaTime;

			if (timeNearTarget >= 1f)
			{
				isMovingToTarget = false;
			}
		}
	}

	private void FindNewTarget()
	{
		currentTarget = PathPoint.GetRandomPoint();
		refAgent.SetDestination(currentTarget);
		isMovingToTarget = true;
		timeNearTarget = 0f;
	}

	public void PutOnRandomHat()
	{
		int index = Random.Range(0, hats.Count);

		GameObject hat = GameObject.Instantiate<GameObject>(hats[index], hatSlot);

		hasHat = true;
		headBorder.color = Color.white;
	}

	public void PutOnHat(GameObject Hat)
	{
		GameObject hat = GameObject.Instantiate<GameObject>(Hat, hatSlot);
		hatless--;
		hasHat = true;
		headBorder.color = Color.white;
	}
}
