using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBlock_Block : MonoBehaviour
{
    [Header("움직임 제한 블록")]
    public Transform from;
    public Transform to;
    
    [Header("움직일 방향 축 선택(*하나만 선택*)")]
    // 유니티 좌표계 방향대로 from블록, to블록 선택

    public bool moveX = false;
    public bool moveY = false;  
    public bool moveZ = false;  

    Vector3 mousePosition;
    Vector3 firstPos;
    float mx;
    float my;
    float mz;
    float d1;
    float d2;

    private void Start()
    {
        // 오브젝트 처음 위치
        firstPos = transform.position;
        if (moveX)
        {
            // from 블럭과의 거리
            d1 = Mathf.Abs(transform.position.x - from.transform.position.x) - (from.lossyScale.x / 2) - (transform.lossyScale.x /2);
            // to 블럭과의 거리
            d2 = Mathf.Abs(transform.position.x - to.transform.position.x) - (to.lossyScale.x / 2) - (transform.lossyScale.x / 2);
        }

        else if (moveY)
        {
            // from 블럭과의 거리
            d1 = Mathf.Abs(transform.position.y - from.transform.position.y) - (from.lossyScale.y / 2) - (transform.lossyScale.y / 2);
            // to 블럭과의 거리
            d2 = Mathf.Abs(transform.position.y - to.transform.position.y) - (to.lossyScale.y / 2) - (transform.lossyScale.y / 2);
        }

        else if (moveZ)
        {
            // from 블럭과의 거리
            d1 = Mathf.Abs(transform.position.z - from.transform.position.z) - (from.lossyScale.z / 2) - (transform.lossyScale.z / 2);
            // to 블럭과의 거리
            d2 = Mathf.Abs(transform.position.z - to.transform.position.z) - (to.lossyScale.z / 2) - (transform.lossyScale.z / 2);

            print(d1);
            print(d2);
        }
    }

    /// <summary>
    /// 드래그 이벤트
    /// - 사용자가 GUIElement 또는 Collider를 클릭하고 마우스 버튼을 계속 누르고 있는 경우 호출
    /// - 마우스 버튼이 눌리고 있는 동안에, 매 프레임마다 호출
    /// </summary>
    private void OnMouseDrag()
    {
        float distanceToScreen = Camera.main.WorldToScreenPoint(transform.position).z;
        // 마우스 좌표 받아오기
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen);
        print(mousePosition);
        if (moveX)
        {
            float worldX = Camera.main.ScreenToWorldPoint(mousePosition).x;
            mx = Mathf.Clamp(worldX , firstPos.x - d1, firstPos.x + d2);
            transform.position = new Vector3(mx, firstPos.y, firstPos.z);
        }
        else if (moveY)
        {
            float worldY = Camera.main.ScreenToWorldPoint(mousePosition).y;
            my = Mathf.Clamp(worldY, firstPos.y - d1, firstPos.y + d2);
            print(my);
            transform.position = new Vector3(firstPos.x, my, firstPos.z);
        }
        else if (moveZ)
        {
            float worldZ = Camera.main.ScreenToWorldPoint(mousePosition).z;
            mz = Mathf.Clamp(worldZ, firstPos.z - d1, firstPos.z + d2);
            print(mz);
            transform.position = new Vector3(firstPos.x, firstPos.y, mz);
        }
    }
}
