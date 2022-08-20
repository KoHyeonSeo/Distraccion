using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEndingMove : MonoBehaviour
{
    [SerializeField] private float walkTime = 4f;
    [SerializeField] private float idleTime = 7f;
    [SerializeField] private float jumpTime = 7.8f;
    [SerializeField] private float runAwayTime = 20f;
    [SerializeField] private AudioClip sream;
    [SerializeField] private Transform runAwayPosition;
    [SerializeField] private float runAwaySpeed = 3f;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float backSpeed = 2f;

    private AudioSource audioSource;
    private Animator animator;
    private float curTime = 0f;
    private bool isPlayOnce = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime <= walkTime)
        {
            Vector3 dir = enemy.transform.position - transform.position;
            transform.position += dir.normalized * backSpeed * Time.deltaTime;
        }
        else if (curTime > walkTime && curTime < idleTime)
        {
            Vector3 dir = enemy.transform.position - transform.position;
            animator.SetTrigger("Idle");
            Vector3 newDir = Vector3.RotateTowards(transform.forward, dir.normalized, turnSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
        else if (curTime >= idleTime && curTime < jumpTime)
        {
            animator.SetTrigger("Jump");
        }
        else if (curTime >= jumpTime && curTime <= runAwayTime)
        {
            if (!isPlayOnce)
            {
                audioSource.Play();
                isPlayOnce = true;
            }
            Vector3 dir = runAwayPosition.position - transform.position;
            Vector3 look = runAwayPosition.position;
            look.y = 0;
            transform.LookAt(look);
            animator.SetTrigger("Run");
            transform.position += dir.normalized * runAwaySpeed * Time.deltaTime;
        }
        
    }

}
