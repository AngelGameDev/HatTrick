using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
	public Transform CamTransform;
	public float MinAngle;
	public float MaxAngle;

	public float MoveThrust;
	public float VerticalThrust;
	public float TurnSpeed;
	public float TiltSpeed;


	private Rigidbody refBody;

	private Vector2 leftStickInput;
	private Vector2 rightStickInput;
	private float verticalInput;

	private void Start()
	{
		refBody = GetComponent<Rigidbody>();
		refBody.useGravity = false;
	}

	private void Update()
	{

		// Rotate ----------------------------------------------------------------------------
		rightStickInput.x = 
			Input.GetAxis("Drone Rightstick Horizontal") - 
			Input.GetAxis("Drone Vertical Movement Up Trigger") + 
			Input.GetAxis("Drone Vertical Movement Down Trigger");

		transform.Rotate(Vector3.up, TurnSpeed * Time.deltaTime * rightStickInput.x);

		// Camera tilt ----------------------------------------------------------------------------
		rightStickInput.y = Input.GetAxis("Drone Rightstick Vertical");

		float newX = CamTransform.localEulerAngles.x + (TurnSpeed * rightStickInput.y * Time.deltaTime);
		if (newX > 180.0f && newX < 360.0f + MinAngle)
		{
			newX = 360.0f + MinAngle;
		}
		else if (newX < 180.0f && newX > MaxAngle)
		{
			newX = MaxAngle;
		}

		CamTransform.localEulerAngles = new Vector3
		(
			newX, 
			CamTransform.localEulerAngles.y,
			CamTransform.localEulerAngles.z
		);
	}

	private void FixedUpdate()
	{
		// Vertical thrust ------------------------------------------------------------------------

		// Create vertical input from triggers.
		verticalInput = 
			Input.GetAxis("Drone Vertical Movement Up Trigger") - 
			Input.GetAxis("Drone Vertical Movement Down Trigger");

		// Add in dpad.
		verticalInput = Mathf.Clamp
		(
			verticalInput + Input.GetAxis("Drone Vertical Movement Dpad"), -1f, 1f
		);

		// Move vertically by adjusting gravity.
		refBody.AddForce(Vector3.up * (VerticalThrust * verticalInput), ForceMode.Acceleration);

		// Tilt move ------------------------------------------------------------------------------
		leftStickInput.x = Input.GetAxis("Drone Leftstick Horizontal");
		leftStickInput.y = Input.GetAxis("Drone Leftstick Vertical");

		refBody.AddForce(transform.forward * (MoveThrust * leftStickInput.y), ForceMode.Acceleration);
		refBody.AddForce(transform.right * (MoveThrust * leftStickInput.x), ForceMode.Acceleration);
	}
}
