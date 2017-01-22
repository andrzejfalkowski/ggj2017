using UnityEngine;
using System.Collections;

public class WaterMovement : MonoBehaviour 
{
	const float MAX_CHANGE = 0.3f;
	const float SPEED = 0.2f;
	bool moveBack = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.Instance.paused)
        {
            return;
        }
        Vector3 pos = this.transform.localPosition;
		pos.x = pos.x + (moveBack ? -Time.deltaTime : Time.deltaTime) * SPEED;
		pos.y = pos.y + (moveBack ? -Time.deltaTime : Time.deltaTime) * SPEED;
		this.transform.localPosition = pos;

		this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f * (pos.x + MAX_CHANGE));

		if(!moveBack && pos.x > MAX_CHANGE)
			moveBack = true;
		else if(moveBack && pos.x < -MAX_CHANGE)
			moveBack = false;
	}
}
