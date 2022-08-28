using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class DirectionAction_End : MonoBehaviour
{
    PlayableDirector pd;
    public ParticleSystem star1;
    public ParticleSystem star2;
    public CinemachineDollyCart dollyCart;

    void Start()
    {
        pd = GetComponent<PlayableDirector>();

    }

    
    void Update()
    {
        // star1, star2 돌리캠 자식으로 넣어주기
        //    if (pd.time >= 9.8f)
        //    {
        //        star1.transform.parent = dollyCart.transform;
        //        star2.transform.parent = dollyCart.transform;
        //    }
    }
}
