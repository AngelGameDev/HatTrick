using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerController : MonoBehaviour
{
	public Transform LeftHandTransform;
	public Transform RightHandTransform;

	public AudioClip hatThrowClip;

	public AudioSource sourceLeft;
	public AudioSource sourceRight;

	public float ThrowForce;

	public List<GameObject> hats = new List<GameObject>(); 

	private float throwTimer;

	private void Update()
	{
		throwTimer -= Time.deltaTime;
		if (throwTimer < 0f) throwTimer = 0f;

		var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
		UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

		foreach (UnityEngine.XR.InputDevice device in leftHandDevices)
		{ 
			bool triggerValue;

			if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
			{
				ThrowLeft();
			}
		}

		var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
		UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

		foreach (UnityEngine.XR.InputDevice device in rightHandDevices)
		{
			bool triggerValue;

			if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
			{
				ThrowRight();
			}
		}
	}

	public void ThrowLeft()
	{
		if (throwTimer > 0f)
		{
			return;
		}

		sourceLeft.Stop();
		sourceLeft.clip = hatThrowClip;
		sourceLeft.Play();

		int index = Random.Range(0, hats.Count);

		GameObject newHat = GameObject.Instantiate<GameObject>(hats[index], LeftHandTransform);
		Rigidbody hatBody = newHat.GetComponent<Rigidbody>();

		newHat.transform.rotation = Quaternion.identity;

		newHat.transform.parent = null;

		hatBody.AddForce(LeftHandTransform.forward * ThrowForce, ForceMode.Impulse);

		throwTimer = 0.45f;
	}

	public void ThrowRight()
	{
		if (throwTimer > 0f)
		{
			return;
		}

		sourceRight.Stop();
		sourceRight.clip = hatThrowClip;
		sourceRight.Play();

		int index = Random.Range(0, hats.Count);

		GameObject newHat = GameObject.Instantiate<GameObject>(hats[index], RightHandTransform);
		Rigidbody hatBody = newHat.GetComponent<Rigidbody>();

		newHat.transform.rotation = Quaternion.identity;

		newHat.transform.parent = null;

		hatBody.AddForce(RightHandTransform.forward * ThrowForce, ForceMode.Impulse);
		hatBody.AddTorque(Vector3.up * Random.Range(0.5f, 15f), ForceMode.Impulse);
		hatBody.AddTorque(Vector3.right * Random.Range(0.5f, 5f), ForceMode.Impulse);
		hatBody.AddTorque(Vector3.up * Random.Range(0.5f, 5f), ForceMode.Impulse);

		throwTimer = 0.45f;
	}
}
