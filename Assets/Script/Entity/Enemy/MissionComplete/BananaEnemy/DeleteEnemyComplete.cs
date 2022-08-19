using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MissionComplete/DeleteEnemy")]
public class DeleteEnemyComplete : MissionComplete
{
    [Header("Mission")]
    public float goDistance = 2f;
    public float backDistance = 6f;
    private GameObject item;
    private bool IsNotPut;

    [Space]
    [Header("Audio")]
    [SerializeField] private AudioClip audioClip;
    private bool audioOnce = false;
    public override void MissionCompleteSetting()
    {
        if (!Start)
        {
            audioOnce = false;
            Enemy.GetComponent<Enemy>().animator.SetTrigger("Fly");
            Enemy.GetComponent<Enemy>().StartCoroutine(Move());
            IsNotPut = false;
        }
        else
        {
            if (IsNotPut)
            {
                if (!audioOnce)
                {
                    audioOnce = true;
                    Enemy.GetComponent<Enemy>().audioSource.clip = audioClip;
                    Enemy.GetComponent<Enemy>().audioSource.loop = true;
                    Enemy.GetComponent<Enemy>().audioSource.Play();
                }
                Vector3 dir = item.transform.position - Enemy.transform.position;
                dir.Normalize();
                Enemy.transform.position += dir * 3 * Time.deltaTime;
                if (Vector3.Distance(item.transform.position, Enemy.transform.position) < 0.5f)
                {
                    Debug.Log("À¸¾Ç");
                    Destroy(item);
                    Enemy.GetComponent<Enemy>().IsDead = true;
                    Enemy.GetComponent<Enemy>().IsStartComplete = false;
                }
            }
        }
    }
    public IEnumerator Move()
    {
        Start = true;
        //Debug.Log("°¡³Ä");
        Vector3 dir = (Enemy.transform.position - Player.transform.position).normalized;
        Vector3 targetPos = Player.transform.position + dir * goDistance;
        while (Vector3.Distance(Player.transform.position, targetPos) > 0.1f)
        {
            Player.transform.position = Vector3.Lerp(Player.transform.position, targetPos, 0.01f);
            yield return null;
        }
        item = Instantiate(Item);
        item.transform.position = Player.transform.GetChild(0).position;
        item.layer = 0;
        targetPos = Player.transform.position - dir * backDistance;
        while (Vector3.Distance(Player.transform.position, targetPos) > 0.1f)
        {
            Player.transform.position = Vector3.Lerp(Player.transform.position, targetPos, 0.01f);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        IsNotPut = true;
    }
}
