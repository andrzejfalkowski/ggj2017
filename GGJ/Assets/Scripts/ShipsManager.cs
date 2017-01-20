using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipsManager : MonoBehaviour 
{
	public static ShipsManager Instance;
	
	public GameObject ShipPrefab;
	
	public List<Ship> Ships;
	
	public void Init () 
	{
		Instance = this;
		
		Ships = new List<Ship>();
	}
	
	public void GenerateShip(Vector2 pos) 
	{
		GameObject go = GameObject.Instantiate(ShipPrefab);
		
		go.GetComponent<Ship>().Init(pos);
	}
}
