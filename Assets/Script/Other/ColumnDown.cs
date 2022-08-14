using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnDown : MonoBehaviour
{
    PlayerMove player;
    [SerializeField] private Transform startNode;
    float speed;
    bool isOnce = true;
    float downSpeed = 1;

    void Start()
    {
        player = GameManager.Instance.playerGameobject.GetComponent<PlayerMove>();
    }


    void Update()
    {
        if (player.currentNode == startNode && isOnce == true)
        {
            CameraControl.Instance.OnShakeCamera(2);
            Vector3 downPoint = new Vector3(transform.position.x, -2.811f, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, downPoint, Time.deltaTime * downSpeed);
            isOnce = false;
            //StartCoroutine("Down");
            //StopAllCoroutines();
            //while (true)
            //{
            //    if (transform.position.y < -2.811f)
            //    {
            //        break;
            //    }
            //    //transform.position += Vector3.down * downSpeed * Time.deltaTime;
            //    Vector3 downPoint = new Vector3(transform.position.x, -2.811f, transform.position.z);
            //    transform.position = Vector3.Lerp(transform.position, downPoint, Time.deltaTime*downSpeed);

            //}
        }
    }

    //private IEnumerator Down()
    //{
    //    while (true)
    //    {
    //        transform.position += Vector3.down * downSpeed * Time.deltaTime;
    //        if (transform.position.y  < -2.811f)
    //        {
    //            print(transform.position.y);
    //            yield break;
    //        }
    //        yield return null;
    //    }
}
