using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class WavesManager : MonoBehaviour 
{
	public static WavesManager Instance;

	[SerializeField]
	Transform wavesParent;
	
	[SerializeField]
	GameObject specialEffect;
	
	public GameObject WavePrefab;
	public GameObject SpecialPrefab;

	public List<Wave> Waves;

	public void Init () 
	{
		Instance = this;

		Waves = new List<Wave>();
	}

	public void GenerateWave(Vector3 pos) 
	{
		GameObject go = GameObject.Instantiate(WavePrefab);
		go.transform.SetParent(wavesParent);
		go.transform.localPosition = pos;

		go.GetComponent<Wave>().Init(pos);
	}

	public void GenerateSpecial(Vector3 pos) 
	{
		GameObject go = GameObject.Instantiate(SpecialPrefab);
		go.transform.SetParent(wavesParent);
		go.transform.localPosition = pos;
		
		go.GetComponent<Wave>().Init(pos);

		DOTween.Kill("special_effet", true);
		specialEffect.GetComponent<RectTransform>().DOMoveY(6f, 1.2f).From().SetEase(Ease.InSine).SetId("special_effet");

		DOTween.Kill("punch_camera", true);
		DOTween.Punch(()=> new Vector3(Camera.main.orthographicSize, 0f, 0f), (v)=> Camera.main.orthographicSize = v.x, new Vector3(-2f, 0f, 0f), 0.7f, 10, 0.5f).SetId("punch_camera");
	}
}
