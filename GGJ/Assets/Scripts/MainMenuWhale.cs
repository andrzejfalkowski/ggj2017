using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MainMenuWhale : MonoBehaviour {

    [SerializeField]
    private Transform tail;
    const float MAX_ROTATION = 25f;

	private void Start () {
        tail.localEulerAngles = new Vector3(0f, 0f, -MAX_ROTATION);
        tail.DORotate(new Vector3(0f, 0f, 2 * MAX_ROTATION), 2f, RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetId("WhaleTail" + GetInstanceID());
	}

    // Swim off the screen.
    public void SwimAway(bool right)
    {
        int sign = (right) ? 1 : -1; 
        DOTween.Kill("WhaleTail" + GetInstanceID(), false);
        DOTween.To(() => tail.localEulerAngles,
            (Vector3 rotation) => tail.localEulerAngles = rotation,
            new Vector3(0f, 0f, sign * MAX_ROTATION), 0.5f)
            .OnComplete(() => transform.parent.DORotate(new Vector3(0f, 0f, -sign * 120f),
                2f, RotateMode.LocalAxisAdd));
    }
}
