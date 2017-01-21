﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour {

    [SerializeField]
    private CanvasGroup texts;
    [SerializeField]
    private Transform background;
    [SerializeField]
    private Image flash;
    const float FADE_OUT_TIME = 0.5f;
    [SerializeField]
    MainMenuWhale whale1;
    [SerializeField]
    MainMenuWhale whale2;

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
            .SetEase(Ease.OutCirc);
        DOTween.To(() => background.localScale,
            (Vector3 scale) => background.localScale = scale, new Vector3(4f, 0f), FADE_OUT_TIME)
            .SetEase(Ease.OutCirc);
        DOTween.ToAlpha(() => flash.color, (Color c) => flash.color = c, 1f, FADE_OUT_TIME);
        DOTween.To(() => texts.transform.localScale,
            (Vector3 scale) => texts.transform.localScale = scale, new Vector3(4f, 0f), FADE_OUT_TIME)
            .SetEase(Ease.OutCirc);
    }
}