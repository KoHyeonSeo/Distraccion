using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� DragBlock_Pos ��ũ��Ʈ�� isTrick�� true�̸� �� �±׸� Trick���� �ٲ۴�.

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
