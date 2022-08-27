using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MissionComplete/ThrowBall")]
public class ThrowBallComplete : MissionComplete
{
    [Header("Mission")]
    //공을 타겟 위치를 향해 던지고 
    //Enemy는 저 공쪽으로 간다.
    [SerializeField] private GameObject targetPosition;
    [SerializeField] private float throwSpeed = 1000;
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private float walkingTime = 2;
    [SerializeField] private float EnemywalkingTime = 3;
    [SerializeField] private float backDistance = 3f;


    [Header("Audio")]
    [SerializeField] private AudioClip audioClip;
    private GameObject item;
    private Vector3 dir;
    private float curTime = 0;
    private bool isThrow = false;
    private bool audioOnce = false;

    public override void MissionCompleteSetting()
    {
        if (!Start)
        {
            Start = true;
            audioOnce = false;
            curTime = 0;
            isThrow = false;
            Enemy.GetComponent<Enemy>().StartCoroutine(Throw());
            GameManager.Instance.SetDontHaveItem = GameManager.Instance.CurItemIndex;
        }
        if (isThrow)
        {
            if (!audioOnce)
            {
                audioOnce = true;
                Enemy.GetComponent<Enemy>().animator.SetTrigger("Fly");
                Enemy.GetComponent<Enemy>().audioSource.clip = audioClip;
                Enemy.GetComponent<Enemy>().audioSource.loop = true;
                Enemy.GetComponent<Enemy>().audioSource.Play();
            }
            curTime += Time.deltaTime;
            Enemy.transform.position += -dir * walkSpeed * Time.deltaTime;
            if (curTime > EnemywalkingTime)
            {
                Destroy(Enemy);
            }
        }
    }
    //Player가 공을 던지는 행위 구현
    private IEnumerator Throw()
    {
        Vector3 targetPos = Player.transform.position - Player.transform.forward  * backDistance;
        //Player.transform.LookAt(new Vector3(targetPos.x, Player.transform.position.y, targetPos.z));
        Player.transform.LookAt(targetPos);
        //while (Vector3.Distance(Player.transform.position, targetPos) > 0.1f)
        //{
        //    Player.transform.position = Vector3.Lerp(Player.transform.position, targetPos, 0.01f);
        //    yield return null;
        //}

        Player.transform.LookAt(new Vector3(-targetPosition.transform.position.x, Player.transform.position.y, -targetPosition.transform.position.z));
        curTime = 0;
        item = Instantiate(Item);
        dir = targetPosition.transform.position - Player.transform.position;
        dir.Normalize();
        item.transform.position = Player.transform.GetChild(0).transform.position;
        item.gameObject.layer = 0;
        item.GetComponent<Rigidbody>().AddForce(-dir * throwSpeed * Time.deltaTime,ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        Enemy.transform.LookAt(new Vector3(-targetPosition.transform.position.x, Enemy.transform.position.y, targetPosition.transform.position.z));
        yield return new WaitForSeconds(1.5f);
        isThrow = true;
    }
}
