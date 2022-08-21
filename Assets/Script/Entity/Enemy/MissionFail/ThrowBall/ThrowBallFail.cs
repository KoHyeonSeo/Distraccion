using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MissionFail/ThrowBall")]
public class ThrowBallFail : MissionFail
{
    [SerializeField] private Material mat;
    [SerializeField] private Material prevMat;
    private float curTime = 0;
    [SerializeField] private float colorTime = 1f;
    public override void MissionFailSetting()
    {
        if (!Start)
        {
            Start = true;
            Vector3 target = Enemy.transform.position;
            target.y = 0;
            Player.transform.LookAt(target);


            Vector3 target1 = Player.transform.position;
            target1.y = 0;
            Enemy.transform.LookAt(target1);
        }
        Vector3 dir = Player.transform.forward;
        dir.Normalize();
        float aggro = Enemy.GetComponent<EnemyDetection>().aggroRange;
        Player.transform.position = Vector3.Lerp(Player.transform.position, Player.transform.position + -dir * (aggro + 2), Time.deltaTime);

        curTime += Time.deltaTime;
        if (curTime >= colorTime)
        {
            Enemy.GetComponent<MeshRenderer>().material = prevMat;
            Enemy.GetComponent<Enemy>().IsStartFail = false;
            curTime = 0;
        }
        else
        {
            Enemy.GetComponent<MeshRenderer>().material = mat;
        }
    }
}
