using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //public bool walkAble = false;

    //����� ����
    public float gCost;
    //������ ����
    public float hCost;
    //���� Cost
    public float fCost;
    // �θ� ���
    public Node parent;

    void Start()
    {

    }

    void Update()
    {

    }

    // ��� ���
    public void SetCost(Vector3 x, Vector3 y)
    {
        gCost = (x - transform.position).sqrMagnitude;  // ���� ������ �Ÿ� 
        hCost = (y - transform.position).sqrMagnitude;  // Ÿ�� ������ �Ÿ� 
        fCost = gCost + hCost;
    }

    // ����� �׸���
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.gray;
    //    Gizmos.DrawSphere(transform.position, 0.3f);
    //}
    // ���õ� ��� �����
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, 0.4f);
    //}
}
