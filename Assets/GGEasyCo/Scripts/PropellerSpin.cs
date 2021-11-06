using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerSpin : MonoBehaviour
{
    public float SpinSpeed;

	private void Update()
	{
		transform.Rotate(transform.parent.up, SpinSpeed * Time.deltaTime);
	}
}
