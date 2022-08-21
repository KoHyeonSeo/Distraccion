using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MissionFail/DeleteEnemy")]
public class DeleteEnemyFail : MissionFail
{
    private float curTime = 0;
    public float bigScaleTime = 1f;
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
        Enemy.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime * 5;
        curTime += Time.deltaTime;
        if (curTime > bigScaleTime)
        {
            Vector3 dir = Player.transform.position - Enemy.transform.position;
            dir.Normalize();
            float aggro = Enemy.GetComponent<EnemyDetection>().aggroRange;
            Player.transform.position = Vector3.Lerp(Player.transform.position, Player.transform.position + dir * (aggro + 2), Time.deltaTime);
            if (Vector3.Distance(Player.transform.position, Enemy.transform.position) > aggro + 2)
            {
                Enemy.GetComponent<Enemy>().IsStartFail = false;
                curTime = 0;
            }
        }
    }
}
