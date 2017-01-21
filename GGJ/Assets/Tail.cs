using UnityEngine;
using System.Collections;

public class Tail : MonoBehaviour 
{
	const float MAX_CHANGE = 35f;
	const float ROT_SPEED = 100f;
	const float ROT_BACK_SPEED = 60f;
	bool moveLeft = false;

	// Update is called once per frame
	void Update () 
	{
//		Vector3 rot = this.transform.localEulerAngles;
//		rot.z = rot.z + (moveLeft ? -Time.deltaTime : Time.deltaTime) * SPEED;
//		this.transform.localEulerAngles = rot;
//
//		if(!moveLeft && rot.z > MAX_CHANGE && rot.z < 180f)
//			moveLeft = true;
//		else if(moveLeft && rot.z < 360f - MAX_CHANGE && rot.z > 180f)
//			moveLeft = false;

		Vector3 rot = this.transform.localEulerAngles;
		if(rot.z > 180f)
			rot.z = Mathf.Min (360f, rot.z + Time.deltaTime * ROT_BACK_SPEED);
		if(rot.z < 180f)
			rot.z = Mathf.Max (0f, rot.z - Time.deltaTime * ROT_BACK_SPEED);
		this.transform.localEulerAngles = rot;
	}

	public void RotateLeft()
	{
		Vector3 rot = this.transform.localEulerAngles;
		rot.z = rot.z + Time.deltaTime * ROT_SPEED;
		if(rot.z > MAX_CHANGE && rot.z < 180f)
			rot.z = MAX_CHANGE;
		this.transform.localEulerAngles = rot;
	}

	public void RotateRight()
	{
		Vector3 rot = this.transform.localEulerAngles;
		rot.z = rot.z - Time.deltaTime * ROT_SPEED;
		if(rot.z < -MAX_CHANGE || (rot.z < 360f - MAX_CHANGE && rot.z > 180f))
			rot.z = -MAX_CHANGE;
		this.transform.localEulerAngles = rot;
	}
}
