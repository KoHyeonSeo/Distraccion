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
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();  
        cc = GetComponent<CharacterController>();
    }
    private void Update()
    {
        //V = V0 + at
        //�����ӵ� ���ϱ�
        yVelocity += gravity * Time.deltaTime;

        //�ٴڿ� ����ִٸ�
        if(cc.collisionFlags == CollisionFlags.Below)
        {
            //���� �ӵ��� 0���� �ϰ� �ʹ�.
            yVelocity = 0;
            //���� ī��Ʈ �ʱ�ȭ
            jumpCnt = 1;
        }
        //SpaceBar�� ������ �� Jump
        if (playerInput.UseItemButton && jumpCnt >= 1)
        {
            yVelocity = jumpForce;
            jumpCnt--;
        }

        //�̵�
        Vector3 dir = playerInput.XKeyBoardAxis * Vector3.right;
        dir.y = yVelocity;

        cc.Move(dir * speed * Time.deltaTime);
    }
}
