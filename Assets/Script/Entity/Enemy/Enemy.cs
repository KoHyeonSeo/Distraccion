using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("���� ���� ����")]
    [SerializeField] private AudioClip failSound;

    [Space]
    [Header("�̼ǰ��� ����")]
    [SerializeField] private Mission mission;
    [SerializeField] private MissionComplete missionComplete;
    [SerializeField] private MissionFail missionFail;
    [SerializeField] private MissionTriggerZone missionTriggerZone;

    [Space]
    [Header("Enemy ���� ����")]
    [SerializeField] private float scaredPower = 10;
    //�̼��� ��� use�ϸ鼭 �ִٰ� �÷��̾ �̼� ������ �޼��� �� 
    //MissionComplete��� ��ũ��Ʈ �������ͺ��� �ߵ�
    private Material material;
    private bool isEnd = false;
    private GameObject player;
    private bool isSoundOnce = false;
    public bool IsDead;
    public bool isDieAnimationUse = true;
    [System.NonSerialized] public AudioSource audioSource;
    [System.NonSerialized] public Animator animator;
    public bool IsStartFail { get; set; }
    public bool IsStartComplete { get; set; }
    public bool IsCheckingItem { get; set; }
    public GameObject ColliderObject { get; set; }
    Vector3 startScale;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        startScale = transform.localScale;
        mission.Enemy = gameObject;
        material = GetComponent<MeshRenderer>().material;
        mission.MissionStart = false;
        missionComplete.Start = false;
        missionFail.Start = false;
    }
    private void Update()
    {
        if (!player)
        {
            player = GameManager.Instance.playerGameobject;
        }
        else
        {
            //mission �ȿ��� ���������� Detection�Ѵ�.
            //Update����
            mission.MissionSetting();
            //Enemy Dead
            if (IsDead && !isEnd)
            {
                StartCoroutine(Dead());
                isEnd = true;
            }
            else
            {
                //Player�� missionZone�� ����� ���
                if (missionTriggerZone.MissionStart)
                {
                    mission.MissionStart = true;
                    mission.chooseItem = missionTriggerZone.ChooseItem;
                    missionTriggerZone.MissionStart = false;
                }
                //Player�� Enemy�� Detection�� �ɷȴٸ� missionFail ��� ����
                //MissionFailSetting -> Update
                if (IsStartFail)
                {
                    if (!isSoundOnce)
                    {
                        ColliderObject.GetComponent<PlayerScared>().Damage(scaredPower);
                        audioSource.clip = failSound;
                        audioSource.Play();
                        isSoundOnce = true;
                    }
                    animator.SetTrigger("Attack");
                    missionFail.Enemy = gameObject;
                    missionFail.Player = ColliderObject;
                    missionFail.MissionFailSetting();
                }
                else if(!IsStartFail && !IsStartComplete)
                {
                    isSoundOnce = false;
                }
                //Player�� �̼��� �����ߴٸ� missionComplete ��� ����
                //MissionCompleteSetting -> Update ����
                else if (IsStartComplete)
                {
                    missionComplete.Enemy = gameObject;
                    missionComplete.Player = player;
                    missionComplete.Item = ColliderObject;
                    missionComplete.MissionCompleteSetting();
                }
                //Ȥ�� Enemy�� ũ�Ⱑ �޶��� ���·� �Դٸ�, ���󺹱� ������
                if (transform.localScale != startScale)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, startScale, 0.01f);
                }
            }
        }
    }

    IEnumerator Dead()
    {
        if (isDieAnimationUse)
        {
            animator.SetTrigger("Death");
        }
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
