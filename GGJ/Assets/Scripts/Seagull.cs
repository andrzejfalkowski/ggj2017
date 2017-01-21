using UnityEngine;
using System.Collections;

public class Seagull : Fish {
    
	// Use this for initialization
	protected void Start () {
        islandNoGoZone = 0f;
        minTimeToChangeGoal = 10f;
        maxTimeToChangeGoal = 15f;
        rotationSpeed = 0.01f;
    }


}
