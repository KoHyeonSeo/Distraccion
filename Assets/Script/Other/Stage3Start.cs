using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Start : MonoBehaviour
{
    public Transform topButton;
    public Transform column1;
    public Transform column2;
    Transform player;
    [SerializeField] private Transform startNode;

    void Start()
    {
        player = GameManager.Instance.playerGameobject.transform;
        StartCoroutine(stageStart());
    }


    private IEnumerator stageStart()
    {
        // ī�޶� ����ũ
        CameraControl.Instance.OnShakeCamera(1);
        yield return new WaitForSeconds(1);

        // Top��ư ȸ��
        float z = 0;
        while (z > -180)
        {
            z -= Time.deltaTime * 100;
            topButton.localEulerAngles = new Vector3(0, -90, z);
            yield return null;
        }
        topButton.localEulerAngles = new Vector3(0, -90, -180);
        yield return new WaitForSeconds(1);

        // �÷��̾� startNode�� �̵�
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 0.01f;
            player.position = new Vector3(player.position.x, player.position.y, player.position.z + t);
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 5.8f);
        yield return new WaitForSeconds(1);

        // column �Ʒ��� �̵�
        float y = 0;
        while (y > -3.0648865f)
        {
            y -= Time.deltaTime*0.01f;
            column1.localPosition = new Vector3(column1.localPosition.x, column1.localPosition.y - y, column1.localPosition.z);
            column2.localPosition = new Vector3(column2.localPosition.x, column2.localPosition.y - y, column2.localPosition.z);
            yield return null;
        }
        column1.localPosition = new Vector3(column1.localPosition.x, -2.783195f, column1.localPosition.z);
        column2.localPosition = new Vector3(column2.localPosition.x, -2.783195f, column2.localPosition.z);
        yield return new WaitForSeconds(1);
    }
}
