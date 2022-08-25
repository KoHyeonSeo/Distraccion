using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story1Player : MonoBehaviour
{
    private float kneeTime = 51.2f;
    private float idleBackTime = 80.7f;
    private float curTime = 0;
    private Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= kneeTime && curTime < idleBackTime)
        {
            Debug.Log("Knee");
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Knee");
        }
        else
        {
            Debug.Log("Idle");
            animator.SetTrigger("Idle");
        }
    }
}
