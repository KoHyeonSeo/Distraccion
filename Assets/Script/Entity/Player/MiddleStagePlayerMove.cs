using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleStagePlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private PlayerInput playerInput;
    private Animator animator;
    private Rigidbody rigid;
    private bool isJumping = false;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();  
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

        float x = playerInput.XKeyBoardAxis;
        Vector3 dir = x * Vector3.right;
        if (x != 0 && !isJumping)
        {
            if (x < 0)
            {
                Vector3 local = transform.localScale;
                if (local.z > 0)
                    local.z *= -1;
                transform.localScale = local;
            }
            else if (x > 0)
            {
                Vector3 local = transform.localScale;
                if (local.z < 0)
                    local.z *= -1;
                transform.localScale = local;
            }
            animator.SetTrigger("Run");
        }
        else if (x == 0 && !isJumping)
        {
            animator.SetTrigger("Idle");
        }
        if (playerInput.UseItemButton && !isJumping)
        {
            isJumping = true;
            animator.SetTrigger("Jump");
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        transform.position += dir * speed * Time.deltaTime;

    }
    private void OnCollisionEnter(Collision collision)
    {
        transform.parent = collision.transform;
        animator.SetTrigger("JumpEnd");
        isJumping = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        transform.parent = null;
    }
}
