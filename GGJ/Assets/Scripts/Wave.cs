using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Wave : MonoBehaviour 
{
	public bool IsSmoke = false;

	[SerializeField]
	SpriteRenderer waveSprite;

	public Vector2 centerPos;

	float currentRange = 0f;

	[SerializeField]
	float growSpeed = 50f;
	const float MAX_RANGE = 100f;

	const float SMOKE_MAX_SIZE_REMAIN = 3f;

	[SerializeField]
	float worldMaxRange = 2f;
	[SerializeField]
	float worlCollisionBuffer = 0.3f;
	[SerializeField]
	float pushForce = 1f;

	private bool initialized = false;
    private Color startingColor;

	private bool timeToDie = false;

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

		if(!IsSmoke)
			currentRange += Time.deltaTime * growSpeed;
		else
			currentRange = Mathf.Min(currentRange + Time.deltaTime * growSpeed, MAX_RANGE);

		waveSprite.transform.localScale = new Vector3(currentRange/MAX_RANGE, currentRange/MAX_RANGE, 1f);

		if(!IsSmoke)
		{
			waveSprite.color = new Color(startingColor.r, startingColor.g, startingColor.b,
	        startingColor.a * Mathf.Min(1f, (1f - (10f * (currentRange/MAX_RANGE) - 9f))));
		}

		if (currentRange >= MAX_RANGE && !timeToDie) 
		{
			timeToDie = true;

			if(!IsSmoke)
				GameObject.Destroy(this.gameObject);
			else
			{
				GameObject.Destroy(this.gameObject, SMOKE_MAX_SIZE_REMAIN);
				waveSprite.DOFade(0f, SMOKE_MAX_SIZE_REMAIN - 0.1f);
			}
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

		Debug.DrawLine(centerPos, centerPos + new Vector2(worldMaxRange * (currentRange/MAX_RANGE) - worlCollisionBuffer, 0f));
		Debug.DrawLine(centerPos, centerPos + new Vector2(0f, worldMaxRange * (currentRange/MAX_RANGE) + worlCollisionBuffer));

		if(IsSmoke)
		{
			return (collidingRangeFromCenter < worldMaxRange * (currentRange/MAX_RANGE));
		}

		//Debug.Log("collidingRangeFromCenter " + collidingRangeFromCenter);
		return ((collidingRangeFromCenter > worldMaxRange * (currentRange/MAX_RANGE) - worlCollisionBuffer) &&
		        (collidingRangeFromCenter < worldMaxRange * (currentRange/MAX_RANGE) + worlCollisionBuffer));
	}

	public float GetPowerLevel()
	{
		return (1f - (currentRange/MAX_RANGE)) * pushForce;
	}
}
