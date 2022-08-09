using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBlock : MonoBehaviour
{
    [Header("������ ���� ���")]
    public Transform from;
    public Transform to;
    
    [Header("������ ���� �� ����")]
    public bool moveX = false;
    public bool moveY = false;
    public bool moveZ = false;

    private void OnMouseDrag()
    {
        float distanceToScreen = Camera.main.WorldToScreenPoint(transform.position).z;
        // ���콺 ��ǥ �޾ƿ���
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen);
        // ���콺 ��ǥ�� ��ũ�� �� ����� �ٲٰ� �� ��ü�� ��ġ�� ������ �ش�.
        if (moveX == true)
        {
            float distance = Mathf.Abs(from.position.x - to.position.x) - from.localScale.x - to.localScale.x;
            float mx = Mathf.Clamp(Camera.main.ScreenToWorldPoint(mousePosition).x, -(distance / 2), (distance / 2));
            transform.position = new Vector3(mx, transform.position.y, transform.position.z);
        }
        else if (moveY == true)
        {
            float distance = Mathf.Abs(from.position.y - to.position.y) - from.localScale.y - to.localScale.y;
            float my = Mathf.Clamp(Camera.main.ScreenToWorldPoint(mousePosition).y, -(distance / 2), (distance / 2));
            transform.position = new Vector3(transform.position.x, my, transform.position.z);
        }
        else if (moveZ == true)
        {
            float distance = Mathf.Abs(from.position.z - to.position.z) - from.localScale.z - to.localScale.z;
            float mz = Mathf.Clamp(Camera.main.ScreenToWorldPoint(mousePosition).z, -(distance / 2), (distance / 2));
            transform.position = new Vector3(transform.position.x, transform.position.y, mz);
        }
    }
}
