using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WavesManager : MonoBehaviour 
{
	public static WavesManager Instance;

	public GameObject WavePrefab;

	public List<Wave> Waves;

	public void Init () 
	{
		Instance = this;

		Waves = new List<Wave>();
	}

	public void GenerateWave(Vector2 pos) 
	{
		GameObject go = GameObject.Instantiate(WavePrefab);

		go.GetComponent<Wave>().Init(pos);
	}
}
