using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneScoreManager : MonoBehaviour
{
    public Text scoreText;
	private bool hasPopulated = false;

	private void Update()
	{
		scoreText.text = "Hats distributed: " + (20 - NPC.hatless);

		if (!hasPopulated && NPC.hatless > 0)
		{
			hasPopulated = true;
		}

		if (hasPopulated)
		{
			if (NPC.hatless <= 0)
			{
				// WIN
				UnityEngine.SceneManagement.SceneManager.LoadScene("Map_v1", UnityEngine.SceneManagement.LoadSceneMode.Single);
			}
		}
	}
}
