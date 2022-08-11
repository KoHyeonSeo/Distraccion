using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MissionComplete/Bug")]
public class BugComplete : MissionComplete
{
    [SerializeField] private float waitTime = 1.5f;
    [SerializeField] private float backDistance = 2f;
    [SerializeField] private Transform runAwayPos;
    [SerializeField] private float flySpeed = 1;
    private GameObject bug;
    private float curTime = 0;
    private Vector3 firstPos;
    private Vector3 endPos;
    private float runAwayTime = 5f;
    private bool isRunAway = false;
    private bool isOnceCall = false;
    public override void MissionCompleteSetting()
    {
        if (!Start)
        {
            bug = Instantiate(Item);
            bug.transform.position = Player.transform.GetChild(0).transform.position;
            bug.transform.forward = Player.transform.forward;
            bug.GetComponent<Bug>().Target = Enemy.transform;
            firstPos = Enemy.transform.position;
            endPos = Enemy.transform.position - Enemy.transform.forward * backDistance;
            Start = true;
        }
        else
        {
            if (!isRunAway)
            {
                curTime += Time.deltaTime;
                if (curTime > waitTime)
                {
                    Enemy.transform.position = Vector3.Lerp(firstPos, endPos, 0.05f);
                    if (Vector3.Distance(Enemy.transform.position, endPos) < 0.1f)
                    {
                        isRunAway = true;
                        curTime = 0;
                    }
                }
            }
            else
            {
                if (!isOnceCall)
                {
                    Enemy.GetComponent<Enemy>().StartCoroutine(Fly());
                    isOnceCall = true;
                }
                Enemy.transform.LookAt(runAwayPos);
                curTime += Time.deltaTime;
                Enemy.GetComponent<Enemy>().StartCoroutine(Fly());
                if (curTime > runAwayTime)
                {
                    Enemy.GetComponent<Enemy>().IsStartComplete = false;
                    Enemy.GetComponent<Enemy>().IsDead = true;
                }
            }
        }
    }
    private IEnumerator Fly()
    {
        while (true)
        {
            float y = flySpeed * Mathf.Sin(Time.time);
            Enemy.transform.position += new Vector3(0, y * 0.008f, 0);
            if (y < 0.001f && y >= -0.001f)
            {
                flySpeed = Random.Range(0.5f, 3.1f);
            }
        }
    }
}
