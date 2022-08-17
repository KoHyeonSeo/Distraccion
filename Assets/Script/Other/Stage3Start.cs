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
        // 카메라 쉐이크
        CameraControl.Instance.OnShakeCamera(1, 0.1f );
        yield return new WaitForSeconds(2);

        // Top버튼 회전
        float z = 0;
        while (z > -180)
        {
            z -= Time.deltaTime * 100;
            topButton.localEulerAngles = new Vector3(0, -90, z);
            yield return null;
        }
        topButton.localEulerAngles = new Vector3(0, -90, -180);
        yield return new WaitForSeconds(2);

        // 플레이어 startNode로 이동
        float t = 7f;
        while (t > 5.9f)
        {
            t -= Time.deltaTime;
            player.position = new Vector3(player.position.x, player.position.y, t);
            yield return null;
        }
        player.position = new Vector3(player.position.x, player.position.y, 5.9f);
        yield return new WaitForSeconds(2);

        // column 아래로 이동
        float y = 0.28f;
        while (y > -2.8f)
        {
            CameraControl.Instance.OnShakeCamera(1, 0.03f);
            y -= Time.deltaTime;
            column1.localPosition = new Vector3(column1.localPosition.x, y, column1.localPosition.z);
            column2.localPosition = new Vector3(column2.localPosition.x, y, column2.localPosition.z);
            yield return null;
        }
        column1.localPosition = new Vector3(column1.localPosition.x, -2.8f, column1.localPosition.z);
        column2.localPosition = new Vector3(column2.localPosition.x, -2.8f, column2.localPosition.z);
        yield return new WaitForSeconds(2);

        //// 두 개 column  위치 변경 (column1의 경우 위치, 회전 바뀌기 전 값 저장해놓기)
        //Vector3 pos = new Vector3(-4.428827f, -2.8f, -8.611213f);
        //Vector3 euler = new Vector3(0, -90, 0);
        //column1.localPosition = column2.localPosition;
        ////column1.eulerAngles = column2.eulerAngles;
        //column2.localPosition = pos;
        //column2.eulerAngles = euler;
    }
}
