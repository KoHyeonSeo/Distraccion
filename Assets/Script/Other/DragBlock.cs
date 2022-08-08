using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBlock : MonoBehaviour
{
    private void OnMouseDrag()
    {
        float distanceToScreen = Camera.main.WorldToScreenPoint(transform.position).z;
        // 마우스 좌표를 받아오기
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen);
        // 마우스 좌표를 스크린 투 월드로 바꾸고 이 객체의 위치로 설정해 준다.
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
