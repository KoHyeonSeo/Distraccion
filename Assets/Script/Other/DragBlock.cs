using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBlock : MonoBehaviour
{
    [Header("움직임 제한 블록")]
    public Transform from;
    public Transform to;
    
    [Header("움직일 방향 축 선택")]
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
        if (moveX)
        {
            // 오브젝트 처음 위치
            firstPos = transform.position;
            // from 블럭과의 거리
            float d1 = Mathf.Abs(transform.position.x - from.transform.position.x) - (from.lossyScale.x / 2) - (transform.lossyScale.x /2);
            // to 블럭과의 거리
            float d2 = Mathf.Abs(transform.position.x - to.transform.position.x) - (to.lossyScale.x / 2) - (transform.lossyScale.x / 2);
        }
    }
    private void OnMouseDrag()
    {
        float distanceToScreen = Camera.main.WorldToScreenPoint(transform.position).z;
        // 마우스 좌표 받아오기
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen);
        if (moveX)
        {
            print(firstPos.x);
            float worldX = Camera.main.ScreenToWorldPoint(mousePosition).x;
            mx = Mathf.Clamp(worldX , firstPos.x - d1, firstPos.x + d2);
            transform.position = new Vector3(mx, transform.position.y, transform.position.z); 
        }
        else if (moveY)
        {
            float distance = Mathf.Abs(from.position.y - to.position.y) - from.localScale.y - to.localScale.y;
            float my = Mathf.Clamp(Camera.main.ScreenToWorldPoint(mousePosition).y, -(distance / 2), (distance / 2));
            transform.position = new Vector3(transform.position.x, my, transform.position.z);
        }
        else if (moveZ)
        {
            float distance = Mathf.Abs(from.position.z - to.position.z) - from.localScale.z - to.localScale.z;
            float mz = Mathf.Clamp(Camera.main.ScreenToWorldPoint(mousePosition).z, -(distance / 2), (distance / 2));
            transform.position = new Vector3(transform.position.x, transform.position.y, mz);
        }
    }
}
