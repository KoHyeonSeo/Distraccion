using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    int fallNum;  // ���������ϴ� ��� ��
    bool isOnce = false;  // �ڷ�ƾ �ѹ��� ����ϵ��� �����ϴ� ����
    public bool onFallingBlock = false;  // �÷��̾ FallingBlock���� ȸ���� ���� ��� true�� �Ǵ� ����
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
            // �÷��̾ ã�� Path ����Ʈ���� FallingBlock ���� ��Ҹ� ��� �����.
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
    /// player�� FallingBlock�� ����
    /// (1) FallingBlock �ڽ� ������� �ð����� �ΰ� Rigidbody�� Use Gravity Ȱ��ȭ
    /// (2) player�� ���̻� ������ �� �� ���� �����
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

