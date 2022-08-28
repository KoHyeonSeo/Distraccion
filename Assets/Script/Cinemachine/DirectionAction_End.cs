using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class DirectionAction_End : MonoBehaviour
{
    PlayableDirector pd;

    [Header("돌리카트")]
    public CinemachineDollyCart dollyCart;
    public CinemachineVirtualCamera vCam3;

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
            print("in");
        }
    }
}
