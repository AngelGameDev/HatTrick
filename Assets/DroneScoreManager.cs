using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneScoreManager : MonoBehaviour
{
    public Text scoreText;
	private bool hasPopulated = false;

	private static Texture2D _staticRectTexture;
	private static GUIStyle _staticRectStyle;

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

		// Draw rects.
		for (int i=0; i<NPC.activeRefs.Count; i++)
		{
			// Set x,y pos.
			NPC.activeRefs[i].personalRect.rectTransform.anchoredPosition = new Vector3
			(
				NPC.activeRefs[i].topLeft.x - 960,
				NPC.activeRefs[i].topLeft.y - 540,
				0f
			);

			// Set width/height.
			NPC.activeRefs[i].personalRect.rectTransform.sizeDelta = new Vector2
			(
				Mathf.Abs(NPC.activeRefs[i].topLeft.x - NPC.activeRefs[i].bottomRight.x) / 2,
				Mathf.Abs(NPC.activeRefs[i].topLeft.y - NPC.activeRefs[i].bottomRight.y) / 2
			);
		}
	}
}
