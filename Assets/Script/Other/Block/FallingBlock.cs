using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    int fallNum;  // 떨어져야하는 블록 수
    PlayerMove player;
    public bool onBlock = false;
    

    void Start()
    {
        player = GameManager.Instance.playerGameobject.GetComponent<PlayerMove>();
        fallNum = transform.childCount;
        StartCoroutine("FallingStart");
    }
    void Update()
    {
        if (player.currentNode == gameObject)
        {
            onBlock = true;
        }
    }

    /// <summary>
    /// player가 FallingBlock에 오면
    /// (1) FallingBlock 자식 순서대로 시간차를 두고 Rigidbody의 Use Gravity 활성화
    /// (2) player가 더이상 앞으로 갈 수 없게 만들기
    /// </summary>
    IEnumerator FallingStart()
    {
        yield return new WaitForSeconds(1);

        if (onBlock)
        {
            for (int i = 0; i < fallNum; i++)
            {
                transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                yield return new WaitForSeconds(1);
            }
        }
    }
}
