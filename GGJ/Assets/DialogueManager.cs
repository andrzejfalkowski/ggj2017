using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class DialogueManager : MonoBehaviour 
{
	public static GameManager Instance;

	[SerializeField]
	List<GameObject> hideableObjects;

	[SerializeField]
	GameObject whale1Box;
	[SerializeField]
	Text whale1Message;

	[SerializeField]
	GameObject whale2Box;
	[SerializeField]
	Text whale2Message;

	int currentMessage = 0;

	bool storyFinished = false;

	public void Init()
	{
		storyFinished = false;

		currentMessage = 0;

		whale1Box.SetActive(false);
		whale2Box.SetActive(false);

		whale1Message.gameObject.SetActive(false);
		whale2Message.gameObject.SetActive(false);

		ShowGameplayObjects(false);

		DOVirtual.DelayedCall(0.1f, ()=>{ ShowNextMessage(); });
	}

	public void ShowGameplayObjects(bool show = true)
	{
		for(int i = 0; i < hideableObjects.Count; i++)
		{
			hideableObjects[i].SetActive(show);
		}
	}

	public void ShowNextMessage()
	{
		currentMessage++;
		ShowMessage(currentMessage);
	}

	public void ShowMessage(int message)
	{
		//Debug.Log("ShowMessage " + message);

		currentMessage = message;

		whale1Box.SetActive(false);
		whale2Box.SetActive(false);

		whale1Message.gameObject.SetActive(false);
		whale2Message.gameObject.SetActive(false);

		DOTween.Kill("dialogue", true);

		whale1Message.text = "";
		whale2Message.text = "";

		switch(currentMessage)
		{
		case 1:
			whale1Message.DOText("Cousin Dick said whalers have their port on this island.", "Cousin Dick said whalers have their port on this island.".Length * 0.03f).SetDelay(0.3f).SetId("dialogue");
			break;
		case 2:
			whale1Message.DOText("On of these psychos chased him for days and then stabbed him with giant harpoon.", "Some psycho chased him for days and then stabbed him with giant harpoon.".Length * 0.03f).SetDelay(0.3f).SetId("dialogue");
			break;
		case 3:
			whale2Message.DOText("Bro, that's not cool. Humans are mental.", "Bro, that's not cool. Humans are mental.".Length * 0.03f).SetDelay(0.3f).SetId("dialogue");
			break;
		case 4:
			whale1Message.DOText("Amen to that. We need to show them this ocean is not big enough for both our species.", "Amen to that. We need to show them this ocean is not big enough for both our species.".Length * 0.03f).SetDelay(0.3f).SetId("dialogue");
			break;
		case 5:
			whale2Message.DOText("But bro! Their boats are pretty sturdy. I can barely push them.", "But bro! Their boats are pretty sturdy. I can barely push them.".Length * 0.03f).SetDelay(0.3f).SetId("dialogue");
			break;
		case 6:
			whale1Message.DOText("If we synchronize our waves, we should be able to blow them out of the water.", "If we synchronize our waves, we should be able to blow them out of the water.".Length * 0.03f).SetDelay(0.3f).SetId("dialogue");
			break;
		case 7:
			whale2Message.DOText("Now yer talkin\', bro. Lemme just get my boombox and *whale* get this party started.", "Now yer talkin\', bro. Lemme just get my boombox and *whale* get this party started.".Length * 0.03f).SetDelay(0.3f).SetId("dialogue");
			break;
		}

		switch(currentMessage)
		{
			case 1:
			case 2:
			case 4:
			case 6:
				whale2Box.SetActive(false);
				whale2Message.gameObject.SetActive(false);

				whale1Box.SetActive(true);
				whale1Message.gameObject.SetActive(true);
				whale1Box.GetComponent<RectTransform>().DOMoveX(-12f, 0.3f).From().SetId("dialogue");
				break;
			case 3:
			case 5:
			case 7:
				whale1Box.SetActive(false);
				whale1Message.gameObject.SetActive(false);

				whale2Box.SetActive(true);
				whale2Message.gameObject.SetActive(true);
				whale2Box.GetComponent<RectTransform>().DOMoveX(12f, 0.3f).From().SetId("dialogue");
				break;
		}
		if(currentMessage == 8)
		{
			storyFinished = true;

			GameManager.Instance.Init();
			ShowGameplayObjects(true);

			whale1Box.SetActive(true);
			whale2Box.SetActive(true);
		}
	}

	void Update()
	{
		if(storyFinished)
			return;

		if(Input.anyKeyDown)
		{
			ShowNextMessage();
		}
	}
}
