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
    bool start = false;
    float rotSpeed = 2;
    float speed = 2;
    float downSpeed = 1;

    private void Awake()
    {
        topButton = GameObject.Find("ButtonCube").transform;
        column1 = GameObject.Find("Column").transform;
        column2 = GameObject.Find("Column2").transform;
        player = GetComponent<PlayerMove>();
    }
    void Start()
    {
        StartCoroutine("Delay");
    }

    private IEnumerator Delay()
    {
        yield return StartCoroutine(CamShake());
        yield return StartCoroutine(TopButtonRotation());
        yield return StartCoroutine(PlayerToStart());
        yield return StartCoroutine(ColumnDown());
    }

    private IEnumerator CamShake()
    {
        // 카메라 쉐이크
        CameraControl.Instance.OnShakeCamera(1);
        yield return new WaitForSeconds(1);
        //print("1111");
    }

    private IEnumerator TopButtonRotation()
    {
        if (Mathf.Abs(topButton.rotation.z) < 180)
        {
            // Top button -180도 회전
            topButton.rotation = Quaternion.Lerp(topButton.rotation, Quaternion.Euler(0, 0, -180), Time.deltaTime * rotSpeed);
            yield return new WaitForSeconds(1);
            //print("2222");
        }
    }

    private IEnumerator PlayerToStart()
    {
        // 플레이어 startNode로 이동
        Vector3 targetPos = startNode.position + Vector3.up;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime*speed);
        yield return new WaitForSeconds(1);
        //print("3333");
    }

    private IEnumerator ColumnDown()
    {
        // 플레이어가 startNode에 서면 column 아래로 이동
        Vector3 targetPos1 = new Vector3(column1.position.x, -2.84f, column1.position.z);
        column1.position = Vector3.Lerp(column1.position, targetPos1, Time.deltaTime*downSpeed);
        Vector3 targetPos2 = new Vector3(column2.position.x, -2.84f, column2.position.z);
        column2.position = Vector3.Lerp(column2.position, targetPos2, Time.deltaTime* downSpeed);
        yield return new WaitForSeconds(1);
        //print("4444");
    }
}
