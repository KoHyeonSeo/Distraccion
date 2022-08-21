using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private float curScene;
    public bool debugMode = false;
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
    /// �ε����� �־� �ش� �������� ������ ���·� �ٲٴ� ���� ���� ������Ƽ
    /// </summary>
    public int SetDontHaveItem
    {
        set
        {
            ItemBox item = itemBoxes[value];
            if (item.isHaveItem)
            {
                item.isHaveItem = false;
                itemBoxes[value] = item;
            }
        }
    }
    /// <summary>
    /// ���� ���� �ִ� �������� ��Ÿ���� �ε��� ������Ƽ
    /// </summary>
    public int CurItemIndex { get; set; }
    #endregion

    #region �÷��̾����
    public GameObject playerGameobject { get; private set; }
    private void Start()
    {
        curScene = SceneManager.GetActiveScene().buildIndex;
    }
    private void Update()
    {
        if (debugMode)
        {
            //Debug.Log(SceneManager.sceneCountInBuildSettings);
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                if (Input.GetKeyDown((KeyCode)(48 + i)))
                {
                    SceneManager.LoadScene(i);
                }
            }
        }
        if (SceneManager.GetActiveScene().buildIndex != curScene)
        {
            playerGameobject = GameObject.Find("Player");
            curScene = SceneManager.GetActiveScene().buildIndex;
        }
        //PlayStart();
    }

    #endregion

    #region Scene Start����
    //void PlayStart()
    //{
    //    // StartUI�� ��� ����ǰ� ���� ��ư�� ���� ���� �غ� �Ϸ�Ǹ� 
    //    if (StartUI.Instance.isReady)
    //    {
    //        // PlayerInput ��ũ��Ʈ Ȱ��ȭ
    //        playerGameobject.GetComponent<PlayerInput>().enabled = true;
    //        print($"2 : {Time.time}");
    //    }
    //}
    #endregion
}
