using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story2Crow : MonoBehaviour
{
    public float attack1Time = 14f;
    public float attackTime = 21.8833f;

    private float curTime = 0;
    private Animator anim;
    private bool isOnce = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= attack1Time && !isOnce)
        {
            isOnce = true;
            anim.SetTrigger("AttackA");
        }
        else if (curTime >= attackTime)
        {
            anim.SetTrigger("Attack");
        }
    }
}
