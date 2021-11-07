using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    private static List<PathPoint> points = new List<PathPoint>();

	private void Start()
	{
		points.Add(this);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, transform.localScale.x);
	}

	public Vector3 GetPosition()
	{
		return transform.position;
	}

	public static void ClearPoints()
	{
		points = new List<PathPoint>();
	}

	public static Vector3 GetRandomPoint()
	{
		int index = Random.Range(0, points.Count);

		return points[index].GetPosition();
	}
}
