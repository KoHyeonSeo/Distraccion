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
    Vector3 startScale;
    private void Start()
    {
        startScale = transform.localScale;
        player = GameManager.Instance.playerGameobject;
        mission.Enemy = gameObject;
        material = GetComponent<MeshRenderer>().material;
        mission.MissionStart = false;
        missionComplete.Start = false;
    }
    private void Update()
    {
        //mission 안에서 지속적으로 Detection한다.
        //Update역할
        mission.MissionSetting();
        //Enemy Dead
        if (IsDead && !isEnd)
        {
           StartCoroutine(Dead());
            isEnd = true;
        }
        else
        {
            //Player가 missionZone에 닿았을 경우
            if (missionTriggerZone.MissionStart)
            {
                mission.MissionStart = true;
                mission.chooseItem = missionTriggerZone.ChooseItem;
                missionTriggerZone.MissionStart = false;
            }
            //Player가 Enemy의 Detection에 걸렸다면 missionFail 계속 실행
            //MissionFailSetting -> Update
            if (IsStartFail)
            {
                missionFail.Enemy = gameObject;
                missionFail.Player = ColliderObject;
                missionFail.MissionFailSetting();
            }
            //Player가 미션을 성공했다면 missionComplete 계속 실행
            //MissionCompleteSetting -> Update 역할
            else if (IsStartComplete)
            {
                missionComplete.Enemy = gameObject;
                missionComplete.Player = player;
                missionComplete.Item = ColliderObject;
                missionComplete.MissionCompleteSetting();
            }
            //혹시 Enemy의 크기가 달라진 상태로 왔다면, 원상복구 시켜줌
            else
            {
                if (transform.localScale != startScale)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, startScale, 0.01f);
                }
            }
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

    #region Regarcy
    //public void MissionFail()
    //{
    //    if (!IsStartFail)
    //    {
    //        IsStartFail = true;
    //        missionFail.Enemy = gameObject;
    //        missionFail.Player = ColliderObject;
    //        missionFail.MissionFailSetting();
    //    }
    //}
    //public void MissionSuccess()
    //{
    //    if (!IsStartComplete)
    //    {
    //        IsStartComplete = true;
    //        missionComplete.Enemy = gameObject;
    //        missionComplete.Item = ColliderObject;
    //        missionComplete.Player = player;
    //        missionComplete.MissionCompleteSetting();
    //    }
    //}
    #endregion

}
