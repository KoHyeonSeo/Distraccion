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
    public int CurItemIndex { get; set; }
    #endregion

    public GameObject playerGameobject { get; private set; }
}
