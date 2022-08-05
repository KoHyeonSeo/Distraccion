using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBlock : MonoBehaviour
{
    public List<GameObject> possiblePaths = new List<GameObject>();
    public bool isVisited = false;
    private void Start()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 0.7f);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].gameObject != gameObject && (cols[i].gameObject.layer==LayerMask.NameToLayer("Block")))
                {
                    possiblePaths.Add(cols[i].gameObject);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stair"))
        {
            possiblePaths.Add(other.gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Color gizmos = Gizmos.color;

        gizmos = Color.yellow;
        gizmos.a = 0.2f;
        Gizmos.DrawSphere(transform.position, 0.7f);
    }
}
