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

    #region �����۰���
    [Serializable]
    public struct ItemBox
    {
        public GameObject Item;
        public bool isHaveItem;
    }

    [SerializeField] private List<ItemBox> itemBoxes;
    /// <summary>
    /// ������ ����Ʈ ������Ƽ
    /// </summary>
    public List<ItemBox> ItemProp {
        get { return itemBoxes; }
        set { itemBoxes = value; } 
    }
    /// <summary>
    /// �ε����� �־� �ش� �������� ������ ���·� �ٲٴ� ���� ���� ������Ƽ
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
    /// ���� ���� �ִ� �������� ��Ÿ���� �ε��� ������Ƽ
    /// </summary>
    public int CurItemIndex { get; set; }
    #endregion

    public GameObject playerGameobject { get; private set; }
}
