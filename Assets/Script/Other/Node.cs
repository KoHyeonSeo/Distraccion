using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //출발점 기준
    public float gCost;
    //도작점 기준
    public float hCost;
    //최종 Cost
    public float fCost;
    // 부모 노드
    public Node parent;

    private Enemy enemy;
    void Start()
    {

    }

    void Update()
    {
        if (enemy)
        {
            if (enemy.IsDead)
            {
                gameObject.layer = LayerMask.NameToLayer("Node");
            }
        }
    }

    // 비용 계산
    public void SetCost(Vector3 x, Vector3 y)
    {
        gCost = (x - transform.position).sqrMagnitude;  // 시작 노드와의 거리 
        hCost = (y - transform.position).sqrMagnitude;  // 타겟 노드와의 거리 
        fCost = gCost + hCost;
    }

    // 기즈모 그리기
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.gray;
    //    Gizmos.DrawSphere(transform.position, 0.3f);
    //}
    // 선택된 노드 기즈모
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, 0.4f);
    //}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            gameObject.layer = 0;
            enemy = collision.collider.GetComponent<Enemy>();
        }
    }
}
