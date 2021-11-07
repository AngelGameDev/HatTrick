using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public BezierSolution.BezierSpline spline;
	public Rigidbody refBody;

	public List<GameObject> cars;

	public float initialPause;

	private float secondaryPause;

	public float speed;

	private float progress = 0f;

	private GameObject activeCar;

	[HideInInspector]
	public bool CanMove;
	[HideInInspector]
	public bool InDock;

	private bool isPaused = true;

	private void Start()
	{
		RandomizeLook();

		InDock = true;
		refBody.isKinematic = true;
		Invoke("EndPause", initialPause);
	}

	private void EndPause()
	{
		isPaused = false;
		refBody.isKinematic = false;
	}

	private void Update()
	{
		if (isPaused)
		{
			return;
		}

		if (progress < 0.05f)
		{
			CanMove = true;
			InDock = true;
		}

		InDock = false;
			
		if (!CanMove)
		{
			return;
		}

		progress += speed * Time.deltaTime;

		if (progress > 1)
		{
			progress = 0f;

			RandomizeLook();
		}

		// Move.
		transform.position = spline.GetPoint(spline.evenlySpacedPoints.GetNormalizedTAtPercentage(progress));
		transform.LookAt(transform.position - spline.GetTangent(spline.evenlySpacedPoints.GetNormalizedTAtPercentage(progress)));
	}

	public void RandomizeLook()
	{
		int index = Random.Range(0, cars.Count);

		if (activeCar)
		{
			Destroy(activeCar);
		}

		activeCar = GameObject.Instantiate<GameObject>(cars[index], this.transform);
	}
}
