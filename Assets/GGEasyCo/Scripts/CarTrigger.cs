using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrigger : MonoBehaviour
{
	private Car car;

	private bool isTriggered;

	private int triggerCount;

	private void Awake()
	{
		car = GetComponentInParent<Car>();
	}

	private void LateUpdate()
	{
		triggerCount++;

		car.CanMove = triggerCount > 10;
	}

	private void OnTriggerStay(Collider other)
	{
		triggerCount = 0;
		isTriggered = true;
	}
}
