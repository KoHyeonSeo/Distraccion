using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    public Transform Target { get; set; }

    private Vector3 dir;
    private float curTime = 0;
    private float bugTime = 5;
    private void Start()
    {
        dir = (Target.position - transform.position).normalized;
    }
    private void Update()
    {
        if (Target)
        {
            curTime += Time.deltaTime;
            transform.position += dir * speed * Time.deltaTime;
            if (curTime > bugTime)
            {
                Destroy(gameObject, 0.5f);
            }

        }
    }
}
