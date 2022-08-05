using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTriggerZone : MonoBehaviour
{
    public bool MissionStart = false;
    public GameObject ChooseItem { get; set; }
    private GameObject player;
    private bool isChecking = false;
    private void Start()
    {
        player = GameManager.Instance.playerGameobject;
    }
    private void Update()
    {
        if (player.GetComponent<PlayerInput>().UseItemButton && isChecking)
        {
            Debug.Log("아이템 사용!");
            //Debug.Log("1. 발동");
            if (GameManager.Instance.ItemProp[GameManager.Instance.CurItemIndex].isHaveItem)
            {
                //Debug.Log("2. 아이템 있음");
                MissionStart = true;
                ChooseItem = GameManager.Instance.ItemProp[GameManager.Instance.CurItemIndex].Item;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("0. 닿음");
        if (other.CompareTag("Player"))
        {
            Debug.Log("나는 player");
            isChecking = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("0. 닿음");
        if (other.CompareTag("Player"))
        {
            Debug.Log("나는 player");
            isChecking = false;
        }
    }
}
