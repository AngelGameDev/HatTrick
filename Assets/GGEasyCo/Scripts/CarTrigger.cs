using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrigger : MonoBehaviour
{
	private Car car;

	private int triggerCount;

	private void Awake()
	{
		car = GetComponentInParent<Car>();
	}

	private void LateUpdate()
	{
		if (car.InDock)
		{
			triggerCount = 81;
		}

		triggerCount++;

		car.CanMove = triggerCount > 80;
	}

	private void OnTriggerStay(Collider other)
	{
		if (!car.InDock)
			triggerCount = 0;
	}
}
