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
        //수직속도 구하기
        yVelocity += gravity * Time.deltaTime;

        //바닥에 닿아있다면
        if(cc.collisionFlags == CollisionFlags.Below)
        {
            //수직 속도를 0으로 하고 싶다.
            yVelocity = 0;
            //점프 카운트 초기화
            jumpCnt = 1;
        }
        //SpaceBar을 눌렀을 때 Jump
        if (playerInput.UseItemButton && jumpCnt >= 1)
        {
            yVelocity = jumpForce;
            jumpCnt--;
        }

        //이동
        Vector3 dir = playerInput.XKeyBoardAxis * Vector3.right;
        dir.y = yVelocity;

        cc.Move(dir * speed * Time.deltaTime);
    }
}
