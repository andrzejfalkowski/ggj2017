using UnityEngine;
using System.Collections;

public class HintScreenController : MonoBehaviour {

    public bool startingGame = false;

    public void Show(bool value, bool startingGame = false)
    {
        this.startingGame = startingGame;
        gameObject.SetActive(value);
    }

    private void Update()
    {
        if (startingGame && Input.anyKeyDown)
        {
            gameObject.SetActive(false);
            GameManager.Instance.Init();
        }
    }
}
