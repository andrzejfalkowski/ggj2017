using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MainMenuWhale : MonoBehaviour {

    [SerializeField]
    private Transform tail;
    const float MAX_TAIL_ROTATION = 25f;
    const float MAX_BODY_ROTATION = 5f;

    private void Start () {
        tail.localEulerAngles = new Vector3(0f, 0f, -MAX_TAIL_ROTATION);
        tail.DORotate(new Vector3(0f, 0f, 2 * MAX_TAIL_ROTATION), 2f, RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetId("WhaleTail" + GetInstanceID());
        transform.DORotate(new Vector3(0f, 0f, 2 * MAX_BODY_ROTATION), 2f, RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetId("WhaleBody" + GetInstanceID());
    }

    // Swim off the screen.
    public void SwimAway(bool right)
    {
        int sign = (right) ? 1 : -1; 
        DOTween.Kill("WhaleTail" + GetInstanceID());
        DOTween.Kill("WhaleBody" + GetInstanceID());
        DOTween.To(() => tail.localEulerAngles,
            (Vector3 rotation) => tail.localEulerAngles = rotation,
            new Vector3(0f, 0f, sign * MAX_TAIL_ROTATION), 0.5f)
            .OnComplete(() => transform.parent.DORotate(new Vector3(0f, 0f, -sign * 160f),
                2f, RotateMode.LocalAxisAdd));
    }
}
