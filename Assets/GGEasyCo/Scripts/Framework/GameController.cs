using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	private void Awake()
	{
		PathPoint.ClearPoints();
		NPC.hatless = 0;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log("Quitting by keyboard input.");

			Application.Quit();
		}

		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			Debug.Log("Restarting by keyboard input.");

			UnityEngine.SceneManagement.SceneManager.LoadScene
			(
				"Map_v1",
				UnityEngine.SceneManagement.LoadSceneMode.Single
			);
		}
	}
}
