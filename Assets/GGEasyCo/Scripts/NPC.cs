using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private NavMeshAgent refAgent;

	private bool isMovingToTarget;
	private Vector3 currentTarget;

	float timeNearTarget;

	private void Awake()
	{
		refAgent = GetComponent<NavMeshAgent>();
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
}
