using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBlock : MonoBehaviour
{
    private void OnMouseDrag()
    {
        float distanceToScreen = Camera.main.WorldToScreenPoint(transform.position).z;
        // ���콺 ��ǥ�� �޾ƿ���
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen);
        // ���콺 ��ǥ�� ��ũ�� �� ����� �ٲٰ� �� ��ü�� ��ġ�� ������ �ش�.
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
