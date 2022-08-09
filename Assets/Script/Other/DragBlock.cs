using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBlock : MonoBehaviour
{
    //[Header("�ڽ� ������ ����")]
    //public float xMin, xMax, yMin, yMax;
    public GameObject from;
    public GameObject to;
    bool fromCollision = false;
    bool toCollision = false;
    public bool moveX = false;
    public bool moveY = false;

    private void OnMouseDrag()
    {
        while (fromCollision == true | toCollision == true)
        {
            float distanceToScreen = Camera.main.WorldToScreenPoint(transform.position).z;
            // ���콺 ��ǥ �޾ƿ���
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen);
            // ���콺 ��ǥ�� ��ũ�� �� ����� �ٲٰ� �� ��ü�� ��ġ�� ������ �ش�.
            if (moveX == true)
            {
                transform.position = new Vector3(Camera.main.ScreenToWorldPoint(mousePosition).x, transform.position.y, transform.position.z);
            }
            if (moveY == true)
            {
                transform.position = new Vector3(transform.position.x, Camera.main.ScreenToWorldPoint(mousePosition).y, transform.position.z);
            }
        }
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
