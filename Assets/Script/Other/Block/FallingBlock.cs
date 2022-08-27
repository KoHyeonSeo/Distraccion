using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    int fallNum;  // 떨어져야하는 블록 수
    bool isOnce = false;  // 코루틴 한번만 재생하도록 제어하는 변수
    public bool onFallingBlock = false;  // 플레이어가 FallingBlock에서 회전만 했을 경우 true가 되는 변수
    PlayerMove player;
    AudioSource blockFallSound;

    void Start()
    {
        player = GameManager.Instance.playerGameobject.GetComponent<PlayerMove>();
        fallNum = transform.childCount;
        blockFallSound = GetComponent<AudioSource>();
    }
    void LateUpdate()
    {
        if (!player)
        {
            player = GameManager.Instance.playerGameobject.GetComponent<PlayerMove>();
        }
        if (player.currentNode.name == transform.name && !isOnce)
        {
            // 플레이어가 찾은 Path 리스트에서 FallingBlock 이후 요소를 모두 지운다.
            int idx = player.findPath.IndexOf(transform.GetComponent<Node>());
            if(player.findPath.Count - 1 > idx)
            {
                player.findPath.RemoveRange(idx+1, player.findPath.Count - idx - 1);

            }
            if (onFallingBlock)
            {
                player.anim.SetTrigger("Idle");
                print("idle");
            }
            StartCoroutine("FallingStart");
            isOnce = true;
        }
    }

    /// <summary>
    /// player가 FallingBlock에 오면
    /// (1) FallingBlock 자식 순서대로 시간차를 두고 Rigidbody의 Use Gravity 활성화
    /// (2) player가 더이상 앞으로 갈 수 없게 만들기
    /// </summary>
    IEnumerator FallingStart()
    {
        yield return new WaitForSeconds(0.1f);
        blockFallSound.Play();
        for (int i = 0; i < fallNum; i++)
        {
            transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
            yield return new WaitForSeconds(0.1f);
        }
    }    

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
    }
}

