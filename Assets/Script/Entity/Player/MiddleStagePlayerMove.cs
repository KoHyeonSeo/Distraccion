using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleStagePlayerMove : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();  
        rb = GetComponent<Rigidbody>(); 
    }
    private void Update()
    {
        //SpaceBar�� ������ �� Jump
        if (playerInput.UseItemButton)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
        Vector3 dir = Vector3.right * playerInput.XKeyBoardAxis;
        transform.position += dir * speed * Time.deltaTime;
    }
}
