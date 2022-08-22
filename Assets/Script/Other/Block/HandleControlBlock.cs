using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleControlBlock : MonoBehaviour
{
    private PlayerMove playerMove;
    private RotationBlock lever;
    [SerializeField] private List<GameObject> controlBlock = new List<GameObject>(); 
    private bool isOnce = false;
    private bool isCheck = false;
    private void Start()
    {
        lever = GetComponent<RotationBlock>();
    }
    private void Update()
    {
        if (!playerMove)
        {
            playerMove = GameManager.Instance.playerGameobject.GetComponent<PlayerMove>();
        }
        else
        {
            isCheck = false;
            for (int i = 0; i < controlBlock.Count; i++)
            {
                if (playerMove.currentNode == controlBlock[i].transform)
                {
                    if (!isOnce)
                    {
                        lever.StartCoroutine(lever.HandleShortSetting());
                        isOnce = true;
                    }
                    isCheck = true;
                }
            }
            if (!isCheck)
            {
                lever.StartCoroutine(lever.HandleLongSetting());
                isOnce = false;
            }
        }
    }


}
