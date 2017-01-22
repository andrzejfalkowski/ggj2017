using UnityEngine;
using System.Collections;

public class HintScreenController : MonoBehaviour {

    public bool startingGame = false;
    [SerializeField]
    GameObject pressAnyHint;

    public void Show(bool value, bool startingGame = false)
    {
        this.startingGame = startingGame;
        gameObject.SetActive(value);
        pressAnyHint.SetActive(startingGame);
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
