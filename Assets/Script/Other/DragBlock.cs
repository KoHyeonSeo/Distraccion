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

    private void OnMouseDrag()
    {
        //while (fromCollision == true | toCollision == true)
        //{
            float distanceToScreen = Camera.main.WorldToScreenPoint(transform.position).z;
            //// ���� ����
            //float mx = Mathf.Clamp(Input.mousePosition.x, xMin, xMax);
            //float my = Mathf.Clamp(Input.mousePosition.y, yMin, yMax);
            // ���콺 ��ǥ �޾ƿ���
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen);
            // ���콺 ��ǥ�� ��ũ�� �� ����� �ٲٰ� �� ��ü�� ��ġ�� ������ �ش�.
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
