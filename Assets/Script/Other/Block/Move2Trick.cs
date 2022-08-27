using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 만약 DragBlock_Pos 스크립트의 isTrick이 true이면 내 태그를 Trick으로 바꾼다.

public class Move2Trick : MonoBehaviour
{
    public DragBlock_Pos dragBlock;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (dragBlock.isTrick)
        {
            tag = "Trick";
        }
    }
}
