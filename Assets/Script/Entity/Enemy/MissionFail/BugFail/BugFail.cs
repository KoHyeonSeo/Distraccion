using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MissionFail/Bug")]
public class BugFail : MissionFail
{
    [SerializeField] private float power = 100;
    private Vector3 dir;
    private Vector3 endPos;
    public override void MissionFailSetting()
    {
        if (!Start)
        {

            Start = true;


            //Vector3 target1 = Player.transform.position;
            //target1.y = 0;
            //Enemy.transform.LookAt(target1);

            Enemy.GetComponent<Rigidbody>().useGravity = true;
            Enemy.GetComponent<Rigidbody>().freezeRotation = true;
            //Enemy.GetComponent<Rigidbody>().AddForce(power * Enemy.transform.up, ForceMode.Impulse);
            float aggro = Enemy.GetComponent<EnemyDetection>().aggroRange;
            Vector3 dir2 = Player.transform.forward;
            dir2.Normalize();
            endPos = Player.transform.position + -dir2 * (aggro -0.5f);
        }
        else
        {
            //Vector3 dir3 = Player.transform.position - Enemy.transform.position;
            //Vector3 newDir = Vector3.RotateTowards(Enemy.transform.forward, dir3.normalized, 10 * Time.deltaTime, 0.0f);
            //Enemy.transform.rotation = Quaternion.LookRotation(newDir);

            Vector3 target1 = Player.transform.position;
            target1.y = 0;
            Enemy.transform.LookAt(target1);

            Player.transform.position = Vector3.Lerp(Player.transform.position, endPos, Time.deltaTime * 5);

            if(Vector3.Distance(Player.transform.position, endPos) < 0.3f)
            {
                Player.transform.position = endPos;
                Enemy.GetComponent<Enemy>().IsStartFail = false;
                return;
            }
            //Player.transform.position = Vector3.Lerp(Player.transform.position, playerStartPos + dir * (aggro + 2), Time.deltaTime);

            //if(Mathf.Abs(Enemy.transform.position.y - startY)<=0.2f)
            //{
            //    Enemy.transform.position = new Vector3(Enemy.transform.position.x, startY, Enemy.transform.position.z);
            //    Enemy.GetComponent<Enemy>().IsStartFail = false;
            //    Start = false;
            //    Enemy.GetComponent<Rigidbody>().useGravity = false;
            //}
        }

    }

}
