using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float aggroRange = 10f;
    public float delayTime = 5f;
    private bool isChecking = false;
    public GameObject Target { get; set; }
    private void Update()
    {
        isChecking = false;
        Collider[] cols = Physics.OverlapSphere(transform.position, aggroRange);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].CompareTag("Player"))
                {
                    Target = cols[i].gameObject;
                    isChecking = true;
                }
            }
            if (!isChecking)
            {
                Target = null;
            } 
        }
        else
        {
            Target = null;
        }
    }
    private void OnDrawGizmos()
    {
        Color gizmos = Gizmos.color;

        gizmos = Color.yellow;
        gizmos.a = 0.2f;
        Gizmos.DrawSphere(transform.position, aggroRange);
    }
}
