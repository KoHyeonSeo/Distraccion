using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBlock_Pos : MonoBehaviour
{
    [Header("움직임 제한 위치")]
    public Vector3 from;
    public Vector3 to;
    
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
        if (moveX)
        {
            print("x11111111");
            float worldX = Camera.main.ScreenToWorldPoint(mousePosition).x;
            print("x22222222");
            mx = Mathf.Clamp(worldX , from.x, to.x);
            print("x33333333");
            transform.position = new Vector3(mx, firstPos.y, firstPos.z);
            print("x44444444");
        }
        else if (moveY)
        {
            print("11111111");
            float worldY = Camera.main.ScreenToWorldPoint(mousePosition).y;
            print("22222222");
            my = Mathf.Clamp(worldY, from.y, to.y);
            print(my);
            print("33333333");
            transform.position = new Vector3(firstPos.x, my, firstPos.z);
            print("44444444");
        }
        else if (moveZ)
        {
            float worldZ = Camera.main.ScreenToWorldPoint(mousePosition).z;
            mz = Mathf.Clamp(worldZ, from.z, to.z);
            print(mz);
            transform.position = new Vector3(firstPos.x, firstPos.y, mz);
        }
    }
}
