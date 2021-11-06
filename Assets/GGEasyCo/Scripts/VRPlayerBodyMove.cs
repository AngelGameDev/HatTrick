using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerBodyMove : MonoBehaviour
{
    public Transform HeadTransform;
	public float DistanceDown;

	private void LateUpdate()
	{
		transform.position = new Vector3
		(
			HeadTransform.position.x,
			HeadTransform.position.y - DistanceDown,
			HeadTransform.position.z
		);
	}
}
