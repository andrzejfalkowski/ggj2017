using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Whale : MonoBehaviour 
{
	public bool left = true;

	const float ROTATION_SPEED = 2f;
	public float acceleration = 0.002f;
	const float SPEED_DROP = 0.0005f;
	public float maxSpeed = 0.05f;

	float currentSpeed = 0f;

	[SerializeField]
	Image powerBar;
	float currentPowerLevel = 1f;
	public float WaveLoadTime = 1f; 

	[SerializeField]
	Image specialBar;
	float currentSpecialLevel = 10f;
	public float SpecialLoadTime = 10f; 

	[SerializeField]
	Tail tail;

	public void Init()
	{

	}

	public void Update()
	{
		if(!GameManager.Instance.Started || GameManager.Instance.GameOver)
			return;

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
			currentSpeed = Mathf.Min(currentSpeed + acceleration, maxSpeed);
		}
		else
		{
			currentSpeed = Mathf.Max(currentSpeed - SPEED_DROP, 0f);
		}

		Vector3 translation = new Vector3(0f, currentSpeed, 0f);
		translation = Quaternion.Euler(this.transform.localEulerAngles) * translation;

		Vector3 pos = this.transform.localPosition + translation;
		Vector3 screenPos = Camera.main.WorldToViewportPoint(pos);
		if(screenPos.x < 0.05f && translation.x < 0f)
			pos.x -= translation.x;
		else if(screenPos.x > 0.95f && translation.x > 0f)
			pos.x -= translation.x;
		else if(screenPos.y < 0.05f && translation.y < 0f)
			pos.y -= translation.y;
		else if(screenPos.y > 0.95f && translation.y > 0f)
			pos.y -= translation.y;

		this.transform.localPosition = pos;

		if(currentPowerLevel >= 1f && (left ? Input.GetKeyDown(KeyCode.R) : Input.GetKeyDown(KeyCode.O)))
		{
			WavesManager.Instance.GenerateWave(this.transform.localPosition);

			currentPowerLevel = 0f;
		}

		if(currentSpecialLevel >= 1f && (left ? Input.GetKeyDown(KeyCode.T) : Input.GetKeyDown(KeyCode.P)))
		{
			WavesManager.Instance.GenerateSpecial(this.transform.localPosition);
			
			currentSpecialLevel = 0f;
		}

		if(currentPowerLevel < 1f)
			currentPowerLevel += Time.deltaTime / WaveLoadTime;

		if(currentSpecialLevel < 1f)
			currentSpecialLevel += Time.deltaTime / SpecialLoadTime;

		powerBar.fillAmount = currentPowerLevel;
		specialBar.fillAmount = currentSpecialLevel;
	}
}
