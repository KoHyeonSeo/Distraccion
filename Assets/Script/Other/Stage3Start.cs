using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Start : MonoBehaviour
{
    public Transform topButton;
    public Transform column1;
    public Transform column2;
    PlayerMove player;
    [SerializeField] private Transform startNode;

    private void Awake()
    {
        //topButton = GameObject.Find("ButtonCube").transform;
        //column1 = GameObject.Find("Column").transform;
        //column2 = GameObject.Find("Column2").transform;
        player = GetComponent<PlayerMove>();
    }
    void Start()
    {
        StartCoroutine(stageStart());
    }


    private IEnumerator stageStart()
    {
        // 카메라 쉐이크
        CameraControl.Instance.OnShakeCamera(1);
        yield return new WaitForSeconds(1);

        // Top버튼 회전
        float z = 0;
        while (z > -180)
        {
            z -= Time.deltaTime * 50;
            topButton.localEulerAngles = new Vector3(0, -90, z);
            yield return null;
        }
        topButton.localEulerAngles = new Vector3(0, -90, -180);
        yield return new WaitForSeconds(1);

        // 플레이어 startNode로 이동
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 25;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - t);
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 5.8f);
        yield return new WaitForSeconds(1);

        // column 아래로 이동
        float y = 0;
        while (y < -3.0648865f)
        {
            y -= Time.deltaTime * 25;
            column1.position = new Vector3(column1.localPosition.x, column1.localPosition.y - y, column1.localPosition.z);
            column2.position = new Vector3(column2.localPosition.x, column2.localPosition.y - y, column2.localPosition.z);
            yield return null;
        }
        column1.position = new Vector3(column1.localPosition.x, -2.783195f, column1.localPosition.z);
        column2.position = new Vector3(column2.localPosition.x, -2.783195f, column2.localPosition.z);
        yield return new WaitForSeconds(1);
    }
}
