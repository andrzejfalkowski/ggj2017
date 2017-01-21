using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Whale : MonoBehaviour 
{
	public bool left = true;

	const float ROTATION_SPEED = 2f;
	const float ACCELERATION = 0.002f;
	const float SPEED_DROP = 0.0005f;
	const float MAX_SPEED = 0.05f;

	float currentSpeed = 0f;

	[SerializeField]
	Image powerBar;
	float currentPowerLevel = 1f;
	const float WAVE_LOAD_TIME = 1f; 

	[SerializeField]
	Tail tail;

	public void Init()
	{

	}

	public void Update()
	{
		if(left ? Input.GetKey(KeyCode.A) : Input.GetKey(KeyCode.LeftArrow))
		{
			Vector3 rot = this.transform.localEulerAngles;
			rot.z+= ROTATION_SPEED;
			this.transform.localEulerAngles = rot;

			tail.RotateRight();
		}
		else if(left ? Input.GetKey(KeyCode.D) : Input.GetKey(KeyCode.RightArrow))
		{
			Vector3 rot = this.transform.localEulerAngles;
			rot.z -= ROTATION_SPEED;
			this.transform.localEulerAngles = rot;

			tail.RotateLeft();
		}

		if(left ? Input.GetKey(KeyCode.W) :  Input.GetKey(KeyCode.UpArrow))
		{
			currentSpeed = Mathf.Min(currentSpeed + ACCELERATION, MAX_SPEED);
		}
		else
		{
			currentSpeed = Mathf.Max(currentSpeed - SPEED_DROP, 0f);
		}

		Vector3 translation = new Vector3(0f, currentSpeed, 0f);
		translation = Quaternion.Euler(this.transform.localEulerAngles) * translation;

		Vector3 pos = this.transform.localPosition + translation;
		Vector3 screenPos = Camera.main.WorldToViewportPoint(pos);
		if(screenPos.x < 0f || screenPos.x > 1f || screenPos.y < 0f || screenPos.y > 1f)
			pos -= translation;
		this.transform.localPosition = pos;

		if(currentPowerLevel >= 1f && (left ? Input.GetKeyDown(KeyCode.Space) : Input.GetKeyDown(KeyCode.Return)))
		{
			WavesManager.Instance.GenerateWave(this.transform.localPosition);

			currentPowerLevel = 0f;
		}

		if(currentPowerLevel < 1f)
			currentPowerLevel += Time.deltaTime / WAVE_LOAD_TIME;

		powerBar.fillAmount = currentPowerLevel;
	}
}
