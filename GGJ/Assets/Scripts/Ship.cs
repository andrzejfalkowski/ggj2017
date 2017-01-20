using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour 
{
	float speed = 5f;

	const float MAX_SPEED = 0.05f;

	const float ROTATION_SPEED = 0.005f;

	const float MAX_ANGLE = 45f;
	float targetAngle = 0f;
	float changeAngle = 0f;
	bool turningLeft = true;

	public void Init(Vector3 pos) 
	{
		changeAngle = UnityEngine.Random.Range(-MAX_ANGLE, MAX_ANGLE);
		targetAngle = this.transform.localEulerAngles.z + changeAngle;

		turningLeft = (Mathf.Sign(targetAngle) == 1);
	}

	public void Update()
	{
		Vector3 rot = this.transform.localEulerAngles;
		if(Mathf.Abs(targetAngle - rot.z) > 1f)
		{	
			rot.z = rot.z + changeAngle * ROTATION_SPEED;
			this.transform.localEulerAngles = rot;
		}

		Vector3 translation = new Vector3(0f, MAX_SPEED, 0f);
		translation = Quaternion.Euler(this.transform.localEulerAngles) * translation;
		
		Vector3 pos = this.transform.localPosition + translation;
		this.transform.localPosition = pos;
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
