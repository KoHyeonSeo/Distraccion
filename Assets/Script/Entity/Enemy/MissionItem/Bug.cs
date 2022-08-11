using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    public Transform Target { get; set; }

    private void Update()
    {
        Vector3 dir = (Target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        if (Vector3.Distance(Target.position, transform.position) < 0.1f)
        {
            Destroy(gameObject, 1.5f);
        }
    }
}
