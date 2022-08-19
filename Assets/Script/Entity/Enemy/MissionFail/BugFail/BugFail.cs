using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MissionFail/Bug")]
public class BugFail : MissionFail
{
    [SerializeField] private float power = 100;
    private float startY = 0;
    private float aggro;
    private Vector3 dir;
    private Vector3 playerStartPos;
    public override void MissionFailSetting()
    {
        if (!Start)
        {
            Start = true;
            startY = Enemy.transform.position.y;
            Enemy.GetComponent<Rigidbody>().useGravity = true;
            Enemy.GetComponent<Rigidbody>().freezeRotation = true;
            //Enemy.GetComponent<Rigidbody>().AddForce(power * Enemy.transform.up, ForceMode.Impulse);
            dir = Player.transform.position - Enemy.transform.position;
            aggro = Enemy.GetComponent<EnemyDetection>().aggroRange;
            playerStartPos = Player.transform.position;
            dir.Normalize();
        }
        else
        {
            Player.transform.position = Vector3.Lerp(Player.transform.position, playerStartPos + dir * (aggro + 2), Time.deltaTime);
            if(Mathf.Abs(Enemy.transform.position.y - startY)<=0.2f)
            {
                Enemy.transform.position = new Vector3(Enemy.transform.position.x, startY, Enemy.transform.position.z);
                Enemy.GetComponent<Enemy>().IsStartFail = false;
                Start = false;
                Enemy.GetComponent<Rigidbody>().useGravity = false;
            }
        }

    }

}
