using UnityEngine;
using System.Collections;

public class ShipSpawner : MonoBehaviour 
{
	float timer = 0f;

	[SerializeField]
	float minDelay = 3f;
	[SerializeField]
	float maxDelay = 8f;

	bool initialized = false;

	public void Init () 
	{
		initialized = true;

		SetFirstDelay();
	}

	void Update ()
    {
        if (GameManager.Instance.paused)
        {
            return;
        }
        if (!GameManager.Instance.Started || GameManager.Instance.GameOver)
			return;

		if(!initialized)
			return;

		timer -= Time.deltaTime;

		if(timer < 0f)
		{
			ShipsManager.Instance.GenerateShip(this.transform.localPosition, this.transform.localEulerAngles);

			SetNextDelay();
		}

	}

	void SetFirstDelay()
	{
		timer += UnityEngine.Random.Range(0f, 2f);
	}

	void SetNextDelay()
	{
		timer += UnityEngine.Random.Range(minDelay, maxDelay);
	}
}
