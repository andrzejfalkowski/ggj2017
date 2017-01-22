using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Fish : MonoBehaviour {

    [SerializeField]
    Fish leader;
    protected Vector3 goal;
    protected float speed = 3f;
    protected Vector3 relationToLeader;

    protected const float SHOAL_DISTANCE = 0.05f;
    protected const float MAX_SPEED = 0.03f;
    protected const float ISLAND_ROTATION_SPEED = 1f;
    protected const float SCREEN_RANGE_X = 6.2f;
    protected const float SCREEN_RANGE_Y = 4.5f;
    const float MAX_ANGLE = 45f;

    protected float rotationSpeed = 0.005f;
    protected float islandNoGoZone = 2f;
    protected float minTimeToChangeGoal = 2f;
    protected float maxTimeToChangeGoal = 5f;

    protected void Awake()
    {
        if (leader != null)
        {
            relationToLeader = leader.transform.position - transform.position;
        }
        else
        {
            DOVirtual.DelayedCall(Random.Range(minTimeToChangeGoal, maxTimeToChangeGoal),
                () => goal = ChooseGoal());
        }
    }

    protected void Update () {
        if (GameManager.Instance.paused)
        {
            return;
        }
        if (Mathf.Abs(Vector3.Distance(GameManager.Instance.IslandObject.transform.position,
                transform.position)) < islandNoGoZone)
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

    protected Vector3 ChooseGoal()
    {
        DOVirtual.DelayedCall(Random.Range(minTimeToChangeGoal, maxTimeToChangeGoal),
            () => goal = ChooseGoal());
        return new Vector3(
            Random.Range(-SCREEN_RANGE_X, SCREEN_RANGE_X),
            Random.Range(-SCREEN_RANGE_Y, SCREEN_RANGE_Y),
            0f);
    }

    protected void Move()
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
