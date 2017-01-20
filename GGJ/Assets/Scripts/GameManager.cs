using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance;

	void Awake () 
	{
		Instance = this;

		this.gameObject.GetComponent<ShipsManager>().Init();
		this.gameObject.GetComponent<WavesManager>().Init();
	}

	void Update () 
	{
	
	}
}
