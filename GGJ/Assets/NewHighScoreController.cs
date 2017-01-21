using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewHighScoreController : MonoBehaviour 
{
	[SerializeField]
	InputField inputField;

	int newHighScorePos = -1;

	void Awake()
	{
		if(GameManager.Instance.Score > PlayerPrefs.GetInt("highscore_1_score", 6))
		{
			newHighScorePos = 1;
		}
		else if(GameManager.Instance.Score > PlayerPrefs.GetInt("highscore_2_score", 3))
		{
			newHighScorePos = 2;
		}
		else if(GameManager.Instance.Score > PlayerPrefs.GetInt("highscore_3_score", 1))
		{
			newHighScorePos = 3;
		}
	}

	public void InputChanged(string text)
	{
		if(text.Length > 3)
			inputField.text = inputField.text.Substring(0, 3);
	}

	public void InputProvided(string text)
	{
		if(text.Length > 3)
			text = text.Substring(0, 3);

		while(text.Length < 3)
			text += "_";

		if(newHighScorePos == 3)
		{
			PlayerPrefs.SetInt("highscore_3_score", GameManager.Instance.Score);
			PlayerPrefs.SetString("highscore_3_name", text);
		}
		else if(newHighScorePos == 2)
		{
			PlayerPrefs.SetInt("highscore_3_score", PlayerPrefs.GetInt("highscore_2_score", 1));
			PlayerPrefs.SetString("highscore_3_name", PlayerPrefs.GetString("highscore_2_name", "FCK"));

			PlayerPrefs.SetInt("highscore_2_score", GameManager.Instance.Score);
			PlayerPrefs.SetString("highscore_2_name", text);
		}
		else if(newHighScorePos == 1)
		{
			PlayerPrefs.SetInt("highscore_3_score", PlayerPrefs.GetInt("highscore_2_score", 1));
			PlayerPrefs.SetString("highscore_3_name", PlayerPrefs.GetString("highscore_2_name", "FCK"));

			PlayerPrefs.SetInt("highscore_2_score", PlayerPrefs.GetInt("highscore_1_score", 6));
			PlayerPrefs.SetString("highscore_2_name", PlayerPrefs.GetString("highscore_1_name", "BSS"));

			PlayerPrefs.SetInt("highscore_1_score", GameManager.Instance.Score);
			PlayerPrefs.SetString("highscore_1_name", text);
		}

		Application.LoadLevel("MainMenu");
	}
}
