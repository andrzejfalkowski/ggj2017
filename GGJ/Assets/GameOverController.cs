using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameOverController : MonoBehaviour 
{
	[SerializeField]
	List<Text> highScores;

	void Awake()
	{
		RefreshHighScore();
	}

	void RefreshHighScore()
	{
		highScores[0].text = PlayerPrefs.GetString("highscore_1_name", "BSS") 
			+ "....." + PlayerPrefs.GetInt("highscore_1_score", 6).ToString("000");
		highScores[1].text = PlayerPrefs.GetString("highscore_2_name", "FCK") 
			+ "....." + PlayerPrefs.GetInt("highscore_2_score", 3).ToString("000");
		highScores[2].text = PlayerPrefs.GetString("highscore_3_name", "GGJ") 
			+ "....." + PlayerPrefs.GetInt("highscore_3_score", 1).ToString("000");
	}

	void Update () 
	{
		if (Input.anyKeyDown)
		{
			Application.LoadLevel("MainMenu");
		}
	}
}
