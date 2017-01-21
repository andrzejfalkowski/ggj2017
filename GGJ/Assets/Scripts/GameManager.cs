using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance;

	public Island IslandObject;

	[SerializeField]
	Text ScoreLabel;
	public int Score = 0;

	[SerializeField]
	Text LivesLabel;
	public int Lives = 5;

	public bool Started = false;

	void Awake () 
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;

		Instance = this;

		//Init();

		this.gameObject.GetComponent<DialogueManager>().Init();
	}

	public void Init()
	{
		Score = 0;
		Lives = 5;

		this.gameObject.GetComponent<ShipsManager>().Init();
		this.gameObject.GetComponent<WavesManager>().Init();

		Started = true;
	}

	public void IncreaseScore()
	{
		Score++;
		ScoreLabel.text = "Score: " + Score.ToString();
	}

	public void DecreaseLives()
	{
		Lives--;
		LivesLabel.text = "Credits: " + Score.ToString();
	}
	
	void Update() 
	{
		if(!Started)
			return;

		// check for collisions
		for(int i = 0; i < ShipsManager.Instance.Ships.Count; i++)
		{
			ShipsManager.Instance.Ships[i].HittingWaves = 0;
			for(int j = 0; j < WavesManager.Instance.Waves.Count; j++)
			{
				if(WavesManager.Instance.Waves[j].IsCollidingWith(ShipsManager.Instance.Ships[i].gameObject))
				{
					//Debug.Log("Hit!");
					ShipsManager.Instance.Ships[i].HittingWaves++;
				}
			}
			if(ShipsManager.Instance.Ships[i].HittingWaves > 1)
				ShipsManager.Instance.Ships[i].Kill();
		}
	}
}
