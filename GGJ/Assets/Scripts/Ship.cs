using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour 
{
	float speed = 5f;

	public void Init(Vector2 pos) 
	{

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
