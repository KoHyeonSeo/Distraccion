using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBlock_Pos : MonoBehaviour
{
    [Header("움직임 제한 위치")]
    public Vector3 min;
    public Vector3 max;
    
    [Header("움직일 방향 축 선택(*하나만 선택*)")]
    // 유니티 좌표계 방향대로 from블록, to블록 선택
    public bool moveX = false;
    public bool moveY = false;  
    public bool moveZ = false;
    [Space]
    public bool isDrag;

    Vector3 firstPos;
    Vector3 mousePos;

    private void Start()
    {
        // 해당 월드 좌표를 스크린 좌표로 변경
        min = Camera.main.WorldToScreenPoint(min);
        max = Camera.main.WorldToScreenPoint(max);

        // mousePosition의 값을 최소 최대값으로 제한하여 움직임 범위 정하기
        if (moveX)
        {

        }
        else if (moveY)
        {

            mousePos.y = Mathf.Clamp(mousePos.y, min.y, max.y);
        }
        else if (moveZ)
        {

        }
    }

    private void Update()
    {
        isDrag = false;
        // 처음 마우스 좌표값 저장
        if(Input.GetMouseButtonDown(0))
        {
            firstPos = Input.mousePosition;
            //print($"firstPos1: {firstPos}");
        }
    }

    /// <summary>
    /// 드래그 이벤트
    /// - 사용자가 GUIElement 또는 Collider를 클릭하고 마우스 버튼을 계속 누르고 있는 경우 호출
    /// - 마우스 버튼이 눌리고 있는 동안에, 매 프레임마다 호출
    /// </summary>
    private void OnMouseDrag()
    {
        isDrag = true;
        if (moveX)
        {

        }
        else if (moveY)
        {
            
            print($" mousePos.y : {mousePos.y}");  // 마우스 스크린 y값
            print($" point block : {GameManager.Instance.playerGameobject.GetComponent<PlayerInput>().PointBlock}");  // 가리키고 있는 블록 정보
            transform.position += Vector3.up * (Input.mousePosition.y - firstPos.y) * Time.deltaTime * 0.5f;
        }
        else if (moveZ)
        {

        }





        //isDrag = true;
        //float distanceToScreen = Camera.main.WorldToScreenPoint(transform.position).z;
        //// 마우스 좌표 받아오기
        //mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen);
        //if (moveX)
        //{
        //    float worldX = Camera.main.ScreenToWorldPoint(mousePosition).x;
        //    mx = Mathf.Clamp(worldX , from.x, to.x);
        //    transform.position = new Vector3(mx, firstPos.y, firstPos.z);
        //}
        //else if (moveY)
        //{
        //    print("11111111");
        //    float worldY = Camera.main.ScreenToWorldPoint(mousePosition).y;
        //    print("22222222");
        //    //my = Mathf.Clamp(worldY, from.y, to.y);
        //    print("33333333");
        //    transform.position = new Vector3(firstPos.x, worldY, firstPos.z);
        //    print("44444444");
        //}
        //else if (moveZ)
        //{
        //    float worldZ = Camera.main.ScreenToWorldPoint(mousePosition).z;
        //    mz = Mathf.Clamp(worldZ, from.z, to.z);
        //    print(mz);
        //    transform.position = new Vector3(firstPos.x, firstPos.y, mz);
        //}
    }
}
