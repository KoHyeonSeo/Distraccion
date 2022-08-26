using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBlock_Pos : MonoBehaviour
{
    [Header("움직일 방향 축 선택(*하나만 선택*)")]
    // 유니티 좌표계 방향대로 from블록, to블록 선택
    public bool moveX = false;
    public bool moveY = false;  
    public bool moveZ = false;
    [Space]
    public bool isDrag;
    [Header("움직임 제한 위치")]
    public float testMinY;
    public float testMaxY;
    public float value = 0;

    Vector3 firstPos;

    PlayerInput player;
    Transform playerCurrentNode;
   


    private void LateUpdate()
    {
        if (!player)
        {
            player = GameManager.Instance.playerGameobject.GetComponent<PlayerInput>();
            playerCurrentNode = player.gameObject.GetComponent<PlayerMove>().currentNode;
        }
        isDrag = false;

        // 처음 마우스 좌표값 저장
        // 플레이어의 currentNode = PlayerInput.PointBlock 일때만 실행
        if (Input.GetMouseButton(0) && playerCurrentNode.gameObject == gameObject)
        {
            firstPos = Input.mousePosition;
            if ((transform.position + Vector3.up * player.YMouseOut).y < testMaxY && (transform.position + Vector3.up * player.YMouseOut).y >testMinY)
            {
                transform.position += Vector3.up * player.YMouseOut;
            }
            else if((transform.position + Vector3.up * player.YMouseOut).y >= testMaxY)
            {
                Vector3 setting = transform.position;
                setting.y = testMaxY;
                transform.position = setting;
            }
            else if ((transform.position + Vector3.up * player.YMouseOut).y >= testMinY)
            {
                Vector3 setting = transform.position;
                setting.y = testMinY;
                transform.position = setting;
            }
            //Debug.Log(player.YMouseOut);
        }
    }
}
