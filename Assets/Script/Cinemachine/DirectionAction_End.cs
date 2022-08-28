using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.SceneManagement;

public class DirectionAction_End : MonoBehaviour
{
    PlayableDirector pd;

    [Header("돌리카트")]
    public CinemachineDollyCart dollyCart;
    public CinemachineVirtualCamera vCam3;

    [Header("다음 scene")]
    [SerializeField] private int nextStage;

    void Start()
    {
        pd = GetComponent<PlayableDirector>();

    }

    
    void Update()
    {
        // 10.5초에서 돌리카트 자식으로 vcam3 넣어주기
        if (pd.time >= 10.5f)
        {
            dollyCart.enabled = true;
        }

        //재생시간이 다된다면
        if (pd.duration - pd.time <= 0.05f)
        {
            SceneManager.LoadScene(nextStage);
        }
    }
}
