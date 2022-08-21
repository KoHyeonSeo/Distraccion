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
    /// 인덱스를 넣어 해당 아이템을 소유한 상태로 바꾸는 쓰기 전용 프로퍼티
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
    /// 현재 갖고 있는 아이템을 나타내는 인덱스 프로퍼티
    /// </summary>
    public int CurItemIndex { get; set; }
    #endregion

    #region 플레이어관련
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

    #region Scene Start관련
    //void PlayStart()
    //{
    //    // StartUI가 모두 재생되고 시작 버튼이 눌려 게임 준비가 완료되면 
    //    if (StartUI.Instance.isReady)
    //    {
    //        // PlayerInput 스크립트 활성화
    //        playerGameobject.GetComponent<PlayerInput>().enabled = true;
    //        print($"2 : {Time.time}");
    //    }
    //}
    #endregion
}
