using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour {

    [SerializeField]
    private CanvasGroup texts;
    [SerializeField]
    private Transform background;
    [SerializeField]
    private SpriteRenderer flash;
    const float FADE_OUT_TIME = 0.5f;
    [SerializeField]
    MainMenuWhale whale1;
    [SerializeField]
    MainMenuWhale whale2;

	[SerializeField]
	private Text loadingText;
	
	[SerializeField]
	List<Text> highScores;

	void Awake()
	{
		highScores[0].text = PlayerPrefs.GetString("highscore_1_name", "BSS") 
			+ "........." + PlayerPrefs.GetInt("highscore_1_score", 6).ToString("000");
		highScores[1].text = PlayerPrefs.GetString("highscore_2_name", "FCK") 
			+ "........." + PlayerPrefs.GetInt("highscore_2_score", 3).ToString("000");
		highScores[2].text = PlayerPrefs.GetString("highscore_3_name", "GGJ") 
			+ "........." + PlayerPrefs.GetInt("highscore_3_score", 1).ToString("000");
	}

    void Update () {
	    if (Input.anyKeyDown)
        {
            StartGame();
        }
	}

    public void StartGame()
    {
        SwitchOffBackgroundAndText();
        whale1.SwimAway(true);
        whale2.SwimAway(false);

        DOVirtual.DelayedCall(2.2f, () => Application.LoadLevel("Gameplay"));
    }

    // TV-like effect for switching off the menu.
    private void SwitchOffBackgroundAndText()
    {
        DOTween.To(() => texts.alpha, (float alpha) => texts.alpha = alpha, 0f, FADE_OUT_TIME)
			.SetEase(Ease.OutCirc).OnComplete(()=>{ loadingText.gameObject.SetActive(true); });
        DOTween.To(() => background.localScale,
            (Vector3 scale) => background.localScale = scale, new Vector3(4f, 0f), FADE_OUT_TIME)
            .SetEase(Ease.OutCirc);
        DOTween.ToAlpha(() => flash.color, (Color c) => flash.color = c, 1f, FADE_OUT_TIME);
        DOTween.To(() => texts.transform.localScale,
            (Vector3 scale) => texts.transform.localScale = scale, new Vector3(4f, 0f), FADE_OUT_TIME)
            .SetEase(Ease.OutCirc);
	}
}
