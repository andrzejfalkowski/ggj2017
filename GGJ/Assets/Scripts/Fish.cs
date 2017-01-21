using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Fish : MonoBehaviour {

    [SerializeField]
    Fish leader;
    private Vector3 goal;
    private float speed = 3f;
    private Vector3 relationToLeader;

    private const float SHOAL_DISTANCE = 0.05f;
    const float MAX_SPEED = 0.03f;
    const float ROTATION_SPEED = 0.005f;
    const float ISLAND_ROTATION_SPEED = 1f;
    const float ISLAND_NO_SWIM_ZONE = 2f;
    const float SCREEN_RANGE_X = 6.2f;
    const float SCREEN_RANGE_Y = 4.5f;

    const float MAX_ANGLE = 45f;

    private void Awake()
    {
        if (leader != null)
        {
            relationToLeader = leader.transform.position - transform.position;
        }
        else
        {
            DOVirtual.DelayedCall(Random.Range(1f, 5f), () => goal = ChooseGoal());
        }
    }

    private void Update () {
        if (Mathf.Abs(Vector3.Distance(GameManager.Instance.IslandObject.transform.position,
                transform.position)) < ISLAND_NO_SWIM_ZONE)
        {
            goal = 2 * transform.position - GameManager.Instance.IslandObject.transform.position;
                //ChooseGoal();
        }
        if (leader != null) { 
            if (Vector3.Distance(leader.transform.position, transform.position) < SHOAL_DISTANCE)
            {
                goal = transform.position;
            }
            else
            {
                goal = leader.transform.position - relationToLeader;
            }
        }
        Move();
	}

    private Vector3 ChooseGoal()
    {
        DOVirtual.DelayedCall(Random.Range(2f, 5f), () => goal = ChooseGoal());
        return new Vector3(
            Random.Range(-SCREEN_RANGE_X, SCREEN_RANGE_X),
            Random.Range(-SCREEN_RANGE_Y, SCREEN_RANGE_Y),
            0f);
    }

    private void Move()
    {
        Vector3 vectorToTarget = goal - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * ISLAND_ROTATION_SPEED);

        Vector3 translation = new Vector3(0f, MAX_SPEED, 0f);
        translation = Quaternion.Euler(this.transform.localEulerAngles) * translation;

        Vector3 pos = this.transform.localPosition + translation;
        this.transform.localPosition = pos;
    }
}
