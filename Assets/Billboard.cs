using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	private void LateUpdate()
	{
		transform.LookAt(DroneController.DroneCam);
	}
}
