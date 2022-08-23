using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleStagePlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -20f;
    private float yVelocity = 0;
    private PlayerInput playerInput;
    private CharacterController cc;
    private int jumpCnt = 1;
    private Animator animator;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();  
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //V = V0 + at
        //�����ӵ� ���ϱ�
        yVelocity += gravity * Time.deltaTime;

        if (jumpCnt == 0 && cc.collisionFlags == CollisionFlags.Below)
        {
            animator.SetTrigger("JumpEnd");
            //���� �ӵ��� 0���� �ϰ� �ʹ�.
            yVelocity = 0;
            //���� ī��Ʈ �ʱ�ȭ
            jumpCnt = 1;
        }
        //SpaceBar�� ������ �� Jump
        if (playerInput.UseItemButton && jumpCnt >= 1)
        {
            animator.SetTrigger("Jump");
            yVelocity = jumpForce;
            jumpCnt--;
        }
        float x = playerInput.XKeyBoardAxis;
        if (x != 0 && cc.collisionFlags == CollisionFlags.Below)
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
        else if(x == 0 && cc.collisionFlags == CollisionFlags.Below)
        {
            animator.SetTrigger("Idle");
        }
        //�̵�
        Vector3 dir = x * Vector3.right;
        dir.y = yVelocity;

        cc.Move(dir * speed * Time.deltaTime);
    }
}
