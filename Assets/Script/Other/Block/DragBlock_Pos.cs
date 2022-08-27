using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DragBlock_Pos : MonoBehaviour
{
    [Serializable]
    public struct ChooseDragBlock
    {
        public GameObject dragBlock;
        public bool differ;
    }

    [Header("드래그할 블록 선택")]
    [SerializeField] private List<ChooseDragBlock> chooseDrag = new List<ChooseDragBlock>();

    [Header("움직일 방향 축 선택(*하나만 선택*)")]
    public bool moveX = false;
    public bool moveY = false;  
    public bool moveZ = false;
    [Space]
    public bool isDrag;
    [Header("움직임 제한 위치")]
    public float testMinY;
    public float testMaxY;
    public float value = 0;

    //[Header("드래그 높이에 따른 소리")]
    //[SerializeField] private AudioClip clips;
    //[SerializeField] private int height = 2;

    Vector3 firstPos;

    PlayerInput player;
    public GameObject trickNode;


    Vector3 dir;
    public bool isTrick = false;
    private bool isOnce = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        
        if (!player)
        {
            player = GameManager.Instance.playerGameobject.GetComponent<PlayerInput>();
        }
        isDrag = false;
        
        // 처음 마우스 좌표값 저장
        if (Input.GetMouseButton(0) && player.PointBlock == gameObject)
        {
            if (!isOnce)
            {
                isOnce = true;
                audioSource.Play();
            }
            firstPos = Input.mousePosition;
            // TwinMove Block 
            for (int i = 0; i < chooseDrag.Count; i++)
            {
                Transform block = chooseDrag[i].dragBlock.transform;

                if (chooseDrag[i].differ)
                {
                    dir = -Vector3.up;
                }
                else
                {
                    dir = Vector3.up;
                }

                if ((block.position +  dir * player.YMouseOut).y < testMaxY && (block.position + dir * player.YMouseOut).y > testMinY)
                {
                    block.position += dir * player.YMouseOut;
                }
                else if ((block.position + dir * player.YMouseOut).y >= testMaxY)
                {
                    Vector3 setting = block.position;
                    setting.y = testMaxY;
                    block.position = setting;
                    isTrick = true;
                }
                else if ((block.position + dir * player.YMouseOut).y >= testMinY)
                {
                    Vector3 setting = block.position;
                    setting.y = testMinY;
                    block.position = setting;
                    isTrick = true;
                }
            }
            // 자기 자신 
            if ((transform.position + Vector3.up * player.YMouseOut).y < testMaxY && (transform.position + Vector3.up * player.YMouseOut).y > testMinY)
            {
                transform.position += Vector3.up * player.YMouseOut;
            }
            else if ((transform.position + Vector3.up * player.YMouseOut).y >= testMaxY)
            {
                Vector3 setting = transform.position;
                setting.y = testMaxY;
                transform.position = setting;
            }
            else if ((transform.position + Vector3.up * player.YMouseOut).y >= testMinY)
            {
                Vector3 setting = transform.position;
                setting.y = testMinY;
                transform.position = setting;
            }
        }
        else if(!Input.GetMouseButton(0))
        {
            isOnce = false;
        }
    }
}
