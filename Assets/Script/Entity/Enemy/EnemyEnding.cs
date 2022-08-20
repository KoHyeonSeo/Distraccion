using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnding : MonoBehaviour
{
    [SerializeField] private float attackTime = 5f;
    [SerializeField] private float againIdleTime = 10f;
    [SerializeField] private AudioClip attack;
    [SerializeField] private GameObject target;

    private AudioSource audioSource;
    private Animator animator;
    private float curTime = 0f;
    private bool isPlayOnce = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = attack;
        audioSource.loop = true;
    }
    private void Update()
    {
        transform.LookAt(target.transform.position);
        curTime += Time.deltaTime;
        //if(curTime <= attackTime)
        //{
        //    animator.SetTrigger("Idle");
        //}
        //else 
        if (curTime > attackTime && curTime < againIdleTime)
        {
            if (!isPlayOnce)
            {
                isPlayOnce = true;
                animator.SetBool("IsAttack", true);
                audioSource.Play();
            }
        }
        else if (curTime >= againIdleTime)
        {
            animator.SetBool("IsAttack", false);
            audioSource.Stop();
        }
    }
}
