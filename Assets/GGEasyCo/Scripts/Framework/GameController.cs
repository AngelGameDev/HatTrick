using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public AudioSource audioSource;
	public AudioClip musicStart;
	public AudioClip musicLoop;

	private void Awake()
	{
		PathPoint.ClearPoints();
		NPC.hatless = 0;
	}

	private void Start()
	{
		audioSource.clip = musicStart;
		audioSource.Play();
	}

	private void StartMusicLoop()
	{
		audioSource.Stop();
		audioSource.clip = musicLoop;
		audioSource.loop = true;
		audioSource.Play();
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
