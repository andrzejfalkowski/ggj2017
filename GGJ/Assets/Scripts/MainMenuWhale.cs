using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MainMenuWhale : MonoBehaviour {

    [SerializeField]
    private Transform tail;
    const float MAX_ROTATION = 25f;

	private void Start () {
        tail.localEulerAngles = new Vector3(0f, 0f, -MAX_ROTATION);
        //DOTween.To(tail.localEulerAngles,
        //    (Vector3 rotation) => tail.localEulerAngles = rotation,
        //    new Vector3(0f, 0f, -30f), 2f);
        tail.DORotate(new Vector3(0f, 0f, 2 * MAX_ROTATION), 2f, RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
	}
}
