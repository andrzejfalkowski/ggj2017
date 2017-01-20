using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour 
{
	[SerializeField]
	SpriteRenderer waveSprite;

	Vector2 centerPos;

	float currentRange = 0f;

	const float GROW_SPEED = 30f;
	const float MAX_RANGE = 100f;

	private bool initialized = false;

	public void Init(Vector2 pos) 
	{
		centerPos = pos;
		currentRange = 0f;
		initialized = true;

		waveSprite.transform.localScale = new Vector3(0f, 0f, 1f);

		WavesManager.Instance.Waves.Add(this);
	}

	void Update () 
	{
		if(!initialized)
			return;

		currentRange += Time.deltaTime * GROW_SPEED;

		waveSprite.transform.localScale = new Vector3(currentRange/MAX_RANGE, currentRange/MAX_RANGE, 1f);

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
