using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {
	
	void Update () {
	    if (Input.anyKeyDown)
        {
            StartGame();
        }
	}

    public void StartGame()
    {
        Application.LoadLevel("Gameplay");
    }
}
