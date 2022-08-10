using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MissionComplete/ThrowBall")]
public class ThrowBallComplete : MissionComplete
{
    //���� Ÿ�� ��ġ�� ���� ������ 
    //Enemy�� �� �������� ����.
    [SerializeField] private GameObject targetPosition;
    [SerializeField] private float throwSpeed = 1000;
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private float walkingTime = 2;
    [SerializeField] private float EnemywalkingTime = 3;
    [SerializeField] private float backDistance = 3f;
    private GameObject item;
    Vector3 dir;
    private float curTime = 0;
    private bool isThrow = false;
    public override void MissionCompleteSetting()
    {
        if (!Start)
        {
            curTime = 0;
            isThrow = false;
            Enemy.GetComponent<Enemy>().StartCoroutine(Throw());
            Start = true;
        }
        if (isThrow)
        {
            curTime += Time.deltaTime;
            Enemy.transform.position += -dir * walkSpeed * Time.deltaTime;
            if (curTime > EnemywalkingTime)
            {
                Enemy.GetComponent<Enemy>().IsDead = true;
                Enemy.GetComponent<Enemy>().IsStartComplete = false;
            }
        }
    }
    //Player�� ���� ������ ���� ����
    private IEnumerator Throw()
    {
        dir = targetPosition.transform.position - Player.transform.position;
        dir.Normalize();
        Vector3 targetPos = Player.transform.position - (Enemy.transform.position - Player.transform.position).normalized * backDistance;
        //Player.transform.LookAt(new Vector3(targetPos.x, Player.transform.position.y, targetPos.z));
        while (Vector3.Distance(Player.transform.position, targetPos) > 0.1f)
        {
            Player.transform.position = Vector3.Lerp(Player.transform.position, targetPos, 0.01f);
            yield return null;
        }

        Player.transform.LookAt(new Vector3(targetPosition.transform.position.x, Player.transform.position.y, targetPosition.transform.position.z));
        curTime = 0;
        item = Instantiate(Item);
        item.transform.position = Player.transform.GetChild(0).transform.position;
        item.gameObject.layer = 0;
        item.GetComponent<Rigidbody>().AddForce(-dir * throwSpeed);
        yield return new WaitForSeconds(1f);
        Enemy.transform.LookAt(new Vector3(targetPosition.transform.position.x, Player.transform.position.y, targetPosition.transform.position.z));
        yield return new WaitForSeconds(1.5f);
        isThrow = true;
    }
}
