using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MissionComplete/Bug")]
public class BugComplete : MissionComplete
{
    [Header("Mission")]
    [SerializeField] private float waitTime = 1.5f;
    [SerializeField] private float backDistance = 2f;
    [SerializeField] private Transform runAwayPos;
    [SerializeField] private float flyRange = 1;
    [SerializeField] private float runAwaySpeed = 4;
    [SerializeField] private float runAwayTime = 5f;
    
    [Space]
    [Header("Audio")]
    [SerializeField] private AudioClip audioClip;

    private GameObject bug;
    private float curTime = 0;
    private bool isRunAway = false;
    private bool isOnceCall = false;
    private float backTime = 1f;
    private Vector3 flyDir;
    private bool audioOnce = false;
    private void Init()
    {
        Enemy.GetComponent<Rigidbody>().useGravity = false;
        Start = true;
        isRunAway = false;
        isOnceCall = false;
        curTime = 0;
    }
    public override void MissionCompleteSetting()
    {
        if (!Start)
        {
            audioOnce = false;
            Enemy.GetComponent<Enemy>().animator.SetTrigger("RunAway");

            Init();
            bug = Instantiate(Item);
            bug.transform.position = Player.transform.GetChild(0).transform.position;
            bug.transform.forward = Player.transform.forward;
            bug.GetComponent<Bug>().Target = Enemy.transform;
            flyDir = (-runAwayPos.position - Enemy.transform.position).normalized;
        }
        else
        {
            if (!isRunAway)
            {
                Enemy.transform.LookAt(runAwayPos.position);
                if (!audioOnce)
                {
                    audioOnce = true;
                    Enemy.GetComponent<Enemy>().audioSource.clip = audioClip;
                    Enemy.GetComponent<Enemy>().audioSource.loop = true;
                    Enemy.GetComponent<Enemy>().audioSource.Play();
                }
                curTime += Time.deltaTime;
                if (curTime > waitTime)
                {
                    Vector3 dir = -Enemy.transform.forward;
                    dir.Normalize();
                    float aggro = Enemy.GetComponent<EnemyDetection>().aggroRange;
                    Enemy.transform.position = Vector3.Lerp(Enemy.transform.position, Enemy.transform.position + dir * backDistance, Time.deltaTime);
                    if (curTime > waitTime + backTime)
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
                Enemy.transform.LookAt(runAwayPos.position);
                curTime += Time.deltaTime;
                Enemy.transform.position += flyDir * runAwaySpeed * Time.deltaTime;
                if (curTime > runAwayTime)
                {
                    Enemy.GetComponent<Enemy>().IsStartComplete = false;
                    Enemy.GetComponent<Enemy>().isDieAnimationUse = true;
                    Enemy.GetComponent<Enemy>().IsDead = true;
                }
            }
        }
    }
    private IEnumerator Fly()
    {
        while (true)
        {
            float y = flyRange * Mathf.Sin(Time.time );
            Enemy.transform.position += new Vector3(0, y * 0.008f, 0);
            if (y < 0.001f && y >= -0.001f)
            {
                flyRange = Random.Range(0.5f, 1f);
            }
            yield return null;
        }
    }
}
