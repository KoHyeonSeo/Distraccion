using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleStagePlayerMove : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();  
    }
    private void Update()
    {
        //SpaceBar�� ������ �� Jump
        if (playerInput.UseItemButton)
        {

        }
    }
}
