using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MissionComplete/test")]
public class MissionCompleteTest : MissionComplete
{
    public float goDistance = 2f;
    public float backDistance = 6f;
    private GameObject item;
    private bool IsNotPut;
    public override void MissionCompleteSetting()
    {
        if (!Start)
        {
            Enemy.GetComponent<Enemy>().StartCoroutine(Move());
            IsNotPut = false;
        }
        else
        {
            if (IsNotPut)
            {
                Vector3 dir = item.transform.position - Enemy.transform.position;
                dir.Normalize();
                Enemy.transform.position += dir * 3 * Time.deltaTime;
                if (Vector3.Distance(item.transform.position, Enemy.transform.position) < 1f)
                {
                    Debug.Log("����");
                    Enemy.GetComponent<Enemy>().IsDead = true;
                    Enemy.GetComponent<Enemy>().IsStartComplete = false;
                }
            }
        }
    }
    public IEnumerator Move()
    {
        Start = true;
        //Debug.Log("����");
        Vector3 targetPos = Player.transform.position + Player.transform.forward * goDistance;
        while (Vector3.Distance(Player.transform.position, targetPos) > 0.1f)
        {
            Player.transform.position = Vector3.Lerp(Player.transform.position, targetPos, 0.01f);
            yield return null;
        }
        item = Instantiate(Item);
        item.transform.position = Player.transform.GetChild(0).position;
        targetPos = Player.transform.position - Player.transform.forward * backDistance;
        while (Vector3.Distance(Player.transform.position, targetPos) > 0.1f)
        {
            Player.transform.position = Vector3.Lerp(Player.transform.position, targetPos, 0.01f);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        IsNotPut = true;
    }
}
