using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    int fallNum;  // ���������ϴ� ��� ��
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
    /// player�� FallingBlock�� ����
    /// (1) FallingBlock �ڽ� ������� �ð����� �ΰ� Rigidbody�� Use Gravity Ȱ��ȭ
    /// (2) player�� ���̻� ������ �� �� ���� �����
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
