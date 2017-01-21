using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Ship : MonoBehaviour 
{
	float speed = 3f;

	const float MAX_SPEED = 0.03f;

	const float ROTATION_SPEED = 0.005f;
	const float ISLAND_ROTATION_SPEED = 1f;

	const float MAX_ANGLE = 45f;
	float targetAngle = 0f;
	float changeAngle = 0f;
	bool turningLeft = true;

	bool sinking = false;
	bool fading = false;

	bool goingForIsland = false;

	[HideInInspector]
	public int HittingWaves = 0;

	public void Init(Vector3 pos) 
	{
		changeAngle = UnityEngine.Random.Range(-MAX_ANGLE, MAX_ANGLE);
		targetAngle = this.transform.localEulerAngles.z + changeAngle;

		turningLeft = (Mathf.Sign(targetAngle) == 1);

		ShipsManager.Instance.Ships.Add(this);

		DOVirtual.DelayedCall(UnityEngine.Random.Range(10f, 15f), 
		()=> 
		{ 
			if(this.gameObject != null)
				goingForIsland = true;
		});
	}

	public void Update()
	{
		if(sinking)
			return;

		if(!goingForIsland)
		{
			Vector3 rot = this.transform.localEulerAngles;
			if(Mathf.Abs(targetAngle - rot.z) > 1f)
			{	
				rot.z = rot.z + changeAngle * ROTATION_SPEED;
				this.transform.localEulerAngles = rot;
			}
		}
		else
		{
			Vector3 vectorToTarget = GameManager.Instance.IslandObject.transform.position - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90f;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * ISLAND_ROTATION_SPEED);
		}

		Vector3 translation = new Vector3(0f, MAX_SPEED, 0f);
		translation = Quaternion.Euler(this.transform.localEulerAngles) * translation;
		
		Vector3 pos = this.transform.localPosition + translation;
		this.transform.localPosition = pos;
	}

	public void Kill()
	{
		if(sinking)
			return;

		sinking = true;

		GameManager.Instance.IncreaseScore();

		this.GetComponent<AudioSource>().Play();

		this.transform.DOScale(new Vector3(0.4f, 0f, 1f), 5f);
		this.transform.DORotate(new Vector3(0f, 0f, 720f), 5f, RotateMode.FastBeyond360);
		this.GetComponentInChildren<SpriteRenderer>().DOFade(0f, 5f)
			.OnComplete(
			()=>
			{
				if(this.gameObject != null)
					GameObject.Destroy(this.gameObject);
			});

		// increase score
	}

	void OnDestroy()
	{
		if(ShipsManager.Instance != null)
		{
			if(ShipsManager.Instance.Ships.Contains(this))
			{
				ShipsManager.Instance.Ships.Remove(this);
			}
		}
	}
}
