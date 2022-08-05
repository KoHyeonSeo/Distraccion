using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleControlBlock : MonoBehaviour
{
    private GameObject player;
    private PlayerMove playerMove;
    private RotationBlock lever;
    [SerializeField] private List<GameObject> controlBlock = new List<GameObject>(); 
    private bool isOnce = false;
    private bool isCheck = false;
    private void Start()
    {
        lever = GetComponent<RotationBlock>();
        player = GameManager.Instance.playerGameobject;
        playerMove = player.GetComponent<PlayerMove>();
    }
    private void Update()
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


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Player"))
    //    {
    //        lever.StartCoroutine(lever.HandleShortSetting());
    //    }
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log("¶³¾îÁü");
    //    if (collision.collider.CompareTag("Player"))
    //    {
    //        lever.StartCoroutine(lever.HandleLongSetting());
    //    }
    //}
}
