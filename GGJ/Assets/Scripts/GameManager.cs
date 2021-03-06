﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

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

	[SerializeField]
	Text Hint;

	[SerializeField]
	GameObject gameOverScreen;
    [SerializeField]
    GameObject newHighScoreScreen;
    [SerializeField]
    private HintScreenController hintScreen;
    private DialogueManager dialogueManager;

    public bool paused = false;
    public bool Started = false;
	public bool GameOver = false;

	void Awake () 
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;

		Instance = this;

        //Init();
        dialogueManager = this.gameObject.GetComponent<DialogueManager>();
        dialogueManager.Init();
	}

	public void Init()
	{
		Score = 0;
		Lives = 5;

		this.gameObject.GetComponent<ShipsManager>().Init();
		this.gameObject.GetComponent<WavesManager>().Init();

		Started = true;

        //ShowHint("Destroy whaler ships before they reach safe port!");
        TogglePauseGame(true);
	}

	public void IncreaseScore()
	{
		Score++;
		ScoreLabel.text = "Score: " + Score.ToString();
	}

	public void ShowHint(string hint)
	{
		Hint.text = hint;

		DOTween.Kill("hint", true);

		Hint.color = new Color(Hint.color.r, Hint.color.g, Hint.color.b, 0f);
		Hint.DOFade(1f, 0.5f).OnComplete
		(
			()=>
			{
				Hint.DOFade(0f, 2f).OnComplete
				(
					()=>
					{
					
					}
				).SetDelay(1f).SetId("hint");
			}
			).SetId("hint");
	}

	public void DecreaseLives()
	{
		Lives--;
		LivesLabel.text = "Credits: " + Lives.ToString();

		if(Lives == 0)
		{
			//game over
			GameOver = true;

			if(Score > PlayerPrefs.GetInt("highscore_1_score", 6) ||
			   Score > PlayerPrefs.GetInt("highscore_2_score", 3) ||
				Score > PlayerPrefs.GetInt("highscore_3_score", 1))
			{
				newHighScoreScreen.gameObject.SetActive(true);
			}
			else
			{
				gameOverScreen.gameObject.SetActive(true);
			}

		}
	}

    public void TogglePauseGame(bool startingGame = false)
    {
        paused = !paused;
        hintScreen.Show(paused, startingGame);
        if (!Started)
        {
            //Init();
            Started = true;
        }
    }
	
	void Update() 
	{
        if (!Started || GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            {
                dialogueManager.SkipAll();
                //TogglePauseGame(true);
            }
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            TogglePauseGame();
            return;
        }
		// check for collisions
		for(int i = 0; i < ShipsManager.Instance.Ships.Count; i++)
		{
			ShipsManager.Instance.Ships[i].HittingWaves = 0;
			ShipsManager.Instance.Ships[i].SlowedDown = false;
			for(int j = 0; j < WavesManager.Instance.Waves.Count; j++)
			{
				if(WavesManager.Instance.Waves[j].IsCollidingWith(ShipsManager.Instance.Ships[i].gameObject))
				{
					//Debug.Log("Hit!");
					if(!WavesManager.Instance.Waves[j].IsSmoke)
					{
						ShipsManager.Instance.Ships[i].Hit(WavesManager.Instance.Waves[j]);
						ShipsManager.Instance.Ships[i].HittingWaves++;
					}
					else
					{
						ShipsManager.Instance.Ships[i].SlowedDown = true;
					}
				}
			}
			if(ShipsManager.Instance.Ships[i].HittingWaves > 1)
				ShipsManager.Instance.Ships[i].Kill();
		}
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
