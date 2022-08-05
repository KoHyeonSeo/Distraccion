using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MissionComplete/test2")]
public class TestMissionComplete2 : MissionComplete
{
    //공을 타겟 위치를 향해 던지고 
    //Enemy는 저 공쪽으로 간다.
    [SerializeField] private GameObject targetPosition;
    [SerializeField] private float throwSpeed = 1000;
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private float walkingTime = 2;
    [SerializeField] private float EnemywalkingTime = 3;
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
        if(isThrow)
        {
            curTime += Time.deltaTime;
            if (curTime < EnemywalkingTime)
            {
                Enemy.transform.position += dir * walkSpeed * Time.deltaTime;
            }
        }
    }
    //Player가 공을 던지는 행위 구현
    private IEnumerator Throw()
    {
        dir = targetPosition.transform.position - Player.transform.position;
        dir.Normalize();
        Player.transform.LookAt(new Vector3(targetPosition.transform.position.x, Player.transform.position.y, targetPosition.transform.position.z));
        while (curTime < walkingTime)
        {
            curTime += Time.deltaTime;
            Player.transform.position += dir * walkSpeed * Time.deltaTime;
            yield return null;
        }
        curTime = 0;
        item = Instantiate(Item);
        item.transform.position = Player.transform.GetChild(0).transform.position;
        item.GetComponent<Rigidbody>().AddForce(dir * throwSpeed);
        isThrow = true;
        yield return null;
    }
}
