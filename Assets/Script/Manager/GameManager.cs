using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        playerGameobject = GameObject.Find("Player");
    }

    #region 아이템관련
    [Serializable]
    public struct ItemBox
    {
        public GameObject Item;
        public bool isHaveItem;
    }

    [SerializeField] private List<ItemBox> itemBoxes;
    /// <summary>
    /// 아이템 리스트 프로퍼티
    /// </summary>
    public List<ItemBox> ItemProp {
        get { return itemBoxes; }
        set { itemBoxes = value; } 
    }
    /// <summary>
    /// 인덱스를 넣어 해당 아이템을 소유한 상태로 바꾸는 쓰기 전용 프로퍼티
    /// </summary>
    public int SetHaveItem
    {
        set
        {
            ItemBox item = itemBoxes[value];
            if (!item.isHaveItem)
            {
                item.isHaveItem = true;
                itemBoxes[value] = item;    
            }
        }
    }
    /// <summary>
    /// 현재 갖고 있는 아이템을 나타내는 인덱스 프로퍼티
    /// </summary>
    public int CurItemIndex { get; set; }
    #endregion

    public GameObject playerGameobject { get; private set; }
}
