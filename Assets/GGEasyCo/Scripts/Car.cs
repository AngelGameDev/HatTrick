using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public BezierSolution.BezierSpline spline;

	public List<GameObject> cars;

	public float speed;

	private float progress = 0f;

	private GameObject activeCar;

	[HideInInspector]
	public bool CanMove;

	private void Start()
	{
		RandomizeLook();
	}

	private void Update()
	{
		if (!CanMove)
		{
			Debug.Log("Cant");
			return;
		}

		progress += speed * Time.deltaTime;

		if (progress > 1)
		{
			progress = 0f;
		}

		// Move.
		transform.position = spline.GetPoint(spline.evenlySpacedPoints.GetNormalizedTAtPercentage(progress));
		transform.LookAt(transform.position + spline.GetTangent(spline.evenlySpacedPoints.GetNormalizedTAtPercentage(progress)));
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
