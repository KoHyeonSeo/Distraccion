using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBlock : MonoBehaviour
{
    //[Header("박스 움직임 제한")]
    //public float xMin, xMax, yMin, yMax;
    public GameObject from;
    public GameObject to;
    bool fromCollision = false;
    bool toCollision = false;

    private void OnMouseDrag()
    {
        //while (fromCollision == true | toCollision == true)
        //{
            float distanceToScreen = Camera.main.WorldToScreenPoint(transform.position).z;
            //// 범위 제한
            //float mx = Mathf.Clamp(Input.mousePosition.x, xMin, xMax);
            //float my = Mathf.Clamp(Input.mousePosition.y, yMin, yMax);
            // 마우스 좌표 받아오기
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen);
            // 마우스 좌표를 스크린 투 월드로 바꾸고 이 객체의 위치로 설정해 준다.
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == from)
        {
            fromCollision = true;
        }
        if (collision.gameObject == to)
        {
            toCollision = true;
        }
    }
}
