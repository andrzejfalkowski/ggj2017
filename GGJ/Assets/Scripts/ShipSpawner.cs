using UnityEngine;
using System.Collections;

public class ShipSpawner : MonoBehaviour 
{
	float timer = 0f;

	[SerializeField]
	float minDelay = 3f;
	[SerializeField]
	float maxDelay = 8f;
	
	void Start () 
	{
		SetNextDelay();
	}

	void Update () 
	{
		timer -= Time.deltaTime;

		if(timer < 0f)
		{
			ShipsManager.Instance.GenerateShip(this.transform.localPosition, this.transform.localEulerAngles);

			SetNextDelay();
		}

	}

	void SetNextDelay()
	{
		timer += UnityEngine.Random.Range(minDelay, maxDelay);
	}
}
