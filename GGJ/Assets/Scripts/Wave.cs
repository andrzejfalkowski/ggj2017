using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour 
{
	Vector2 centerPos;

	float currentRange = 0f;

	const float GROW_SPEED = 1f;
	const float MAX_RANGE = 100f;

	private bool initialized = false;

	public void Init(Vector2 pos) 
	{
		centerPos = pos;
		currentRange = 0f;
		initialized = true;

		WavesManager.Instance.Waves.Add(this);
	}

	void Update () 
	{
		if(!initialized)
			return;

		currentRange += Time.deltaTime;
		if (currentRange > MAX_RANGE) 
		{
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnDestroy()
	{
		if(WavesManager.Instance != null)
		{
			if(WavesManager.Instance.Waves.Contains(this))
			{
				WavesManager.Instance.Waves.Remove(this);
			}
		}
	}

}
