using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
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
				"Main",
				UnityEngine.SceneManagement.LoadSceneMode.Single
			);
		}
	}
}
