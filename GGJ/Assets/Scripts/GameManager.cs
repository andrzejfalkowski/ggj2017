using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance;

	void Awake () 
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;

		Instance = this;

		this.gameObject.GetComponent<ShipsManager>().Init();
		this.gameObject.GetComponent<WavesManager>().Init();
	}

	void Update() 
	{
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
