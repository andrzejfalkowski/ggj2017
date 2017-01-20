using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WavesManager : MonoBehaviour 
{
	public static WavesManager Instance;

	[SerializeField]
	Transform wavesParent;

	public GameObject WavePrefab;

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
}
