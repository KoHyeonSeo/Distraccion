using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Mission mission;
    [SerializeField] private MissionComplete missionComplete;
    [SerializeField] private MissionFail missionFail;
    [SerializeField] private MissionTriggerZone missionTriggerZone;
    //미션은 계속 use하면서 있다가 플레이어가 미션 조건을 달성할 시 
    //MissionComplete라는 스크립트 오브젝터블을 발동
    private Material material;
    private bool isEnd = false;
    private GameObject player;

    public bool IsStartFail { get; set; }
    public bool IsStartComplete { get; set; }
    public bool IsDead { get; set; }
    public bool IsCheckingItem { get; set; }
    public GameObject ColliderObject { get; set; }

    private void Start()
    {
        player = GameManager.Instance.playerGameobject;
        mission.Enemy = gameObject;
        material = GetComponent<MeshRenderer>().material;
        mission.MissionStart = false;
        missionComplete.isOnce = false;
    }
    private void Update()
    {
        mission.MissionSetting();
        if (IsDead && !isEnd)
        {
           StartCoroutine(Dead());
            isEnd = true;
        }
        else
        {
            if (missionTriggerZone.MissionStart)
            {
                mission.MissionStart = true;
                mission.chooseItem = missionTriggerZone.ChooseItem;
                missionTriggerZone.MissionStart = false;
            }
            if (IsStartFail)
            {
                missionFail.Enemy = gameObject;
                missionFail.Player = ColliderObject;
                missionFail.MissionFailSetting();
            }
            else if (IsStartComplete)
            {
                missionComplete.Enemy = gameObject;
                missionComplete.Player = player;
                missionComplete.MissionCompleteSetting();
            }
            else
            {
                if (transform.localScale != new Vector3(1, 1, 1))
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), 0.01f);
                }
            }
        }
    }
    public void MissionFail()
    {
        if (!IsStartFail)
        {
            IsStartFail = true;
            missionFail.Enemy = gameObject;
            missionFail.Player = ColliderObject;
            missionFail.MissionFailSetting();
        }
    }
    public void MissionSuccess()
    {
        if (!IsStartComplete)
        {
            IsStartComplete = true;
            missionComplete.Enemy = gameObject;
            missionComplete.Item = ColliderObject;
            missionComplete.Player = player;
            missionComplete.MissionCompleteSetting();
        }
    }
    IEnumerator Dead()
    {
        float alpha = material.color.a;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * 0.5f;
            material.color = new Color(material.color.r,material.color.g,material.color.b ,alpha);
            yield return null;
        }
        Destroy(gameObject);
    }
}
