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
	public GameObject SpecialBassPrefab;
	public GameObject SpecialSmokePrefab;

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

	public void GenerateSpecialBass(Vector3 pos) 
	{
		GameObject go = GameObject.Instantiate(SpecialBassPrefab);
		go.transform.SetParent(wavesParent);
		go.transform.localPosition = pos;
		
		go.GetComponent<Wave>().Init(pos);

		DOTween.Kill("special_effet", true);
		specialEffect.GetComponent<RectTransform>().DOMoveY(6f, 1.2f).From().SetEase(Ease.InSine).SetId("special_effet");

		DOTween.Kill("punch_camera", true);
		DOTween.Punch(()=> new Vector3(Camera.main.orthographicSize, 0f, 0f), (v)=> Camera.main.orthographicSize = v.x, new Vector3(-2f, 0f, 0f), 0.7f, 10, 0.5f).SetId("punch_camera");
	}

	public void GenerateSpecialSmoke(Vector3 pos) 
	{
		GameObject go = GameObject.Instantiate(SpecialSmokePrefab);
		go.transform.SetParent(wavesParent);
		go.transform.localPosition = pos;
		
		go.GetComponent<Wave>().Init(pos);
		
//		DOTween.Kill("special_effet", true);
//		specialEffect.GetComponent<RectTransform>().DOMoveY(6f, 1.2f).From().SetEase(Ease.InSine).SetId("special_effet");
//		
//		DOTween.Kill("punch_camera", true);
//		DOTween.Punch(()=> new Vector3(Camera.main.orthographicSize, 0f, 0f), (v)=> Camera.main.orthographicSize = v.x, new Vector3(-2f, 0f, 0f), 0.7f, 10, 0.5f).SetId("punch_camera");
	}
}
