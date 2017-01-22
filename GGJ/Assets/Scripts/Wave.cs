using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour 
{
	[SerializeField]
	SpriteRenderer waveSprite;

	Vector2 centerPos;

	float currentRange = 0f;

	const float GROW_SPEED = 50f;
	const float MAX_RANGE = 100f;

	const float WORLD_MAX_RANGE = 2f;
	const float WORLD_COLLISION_BUFFER = 0.3f;

	private bool initialized = false;
    private Color startingColor;

	public void Init(Vector2 pos) 
	{
		centerPos = pos;
		currentRange = 0f;
		initialized = true;

		waveSprite.transform.localScale = new Vector3(0f, 0f, 1f);

		WavesManager.Instance.Waves.Add(this);
        startingColor = waveSprite.color;

		this.GetComponent<AudioSource>().Play();
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

		currentRange += Time.deltaTime * GROW_SPEED;

		waveSprite.transform.localScale = new Vector3(currentRange/MAX_RANGE, currentRange/MAX_RANGE, 1f);
		waveSprite.color = new Color(startingColor.r, startingColor.g, startingColor.b,
        startingColor.a * Mathf.Min(1f, (1f - (10f * (currentRange/MAX_RANGE) - 9f))));

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

	public bool IsCollidingWith(GameObject colliding)
	{
		float collidingRangeFromCenter = ((Vector2)colliding.transform.localPosition - centerPos).magnitude;

		Debug.DrawLine(centerPos, centerPos + new Vector2(WORLD_MAX_RANGE * (currentRange/MAX_RANGE) - WORLD_COLLISION_BUFFER, 0f));
		Debug.DrawLine(centerPos, centerPos + new Vector2(0f, WORLD_MAX_RANGE * (currentRange/MAX_RANGE) + WORLD_COLLISION_BUFFER));
		//Debug.Log("collidingRangeFromCenter " + collidingRangeFromCenter);
		return ((collidingRangeFromCenter > WORLD_MAX_RANGE * (currentRange/MAX_RANGE) - WORLD_COLLISION_BUFFER) &&
		        (collidingRangeFromCenter < WORLD_MAX_RANGE * (currentRange/MAX_RANGE) + WORLD_COLLISION_BUFFER));
	}
}
