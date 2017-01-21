using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipsManager : MonoBehaviour 
{
	public static ShipsManager Instance;

	[SerializeField]
	Transform shipsParent;
	[SerializeField]
	List<ShipSpawner> shipSpawners;

	public GameObject ShipPrefab;
	
	public List<Ship> Ships;
	
	public void Init () 
	{
		Instance = this;
		
		Ships = new List<Ship>();

		for(int i = 0; i < shipSpawners.Count; i++)
		{
			shipSpawners[i].Init();
		}
	}
	
	public void GenerateShip(Vector3 pos, Vector3 rot) 
	{
		GameObject go = GameObject.Instantiate(ShipPrefab);
		go.transform.SetParent(shipsParent);
		go.transform.localPosition = pos;
		go.transform.localEulerAngles = rot;

		go.GetComponent<Ship>().Init(pos);
	}
}
