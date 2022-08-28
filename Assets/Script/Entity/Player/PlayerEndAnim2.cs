using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class PlayerEndAnim2 : MonoBehaviour
{
    Animator anim;
    public PlayableDirector pd;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {

        // 플레이어 애니메이션 초로 제어
        if (pd.time >= 16)
        {
            anim.SetTrigger("Idle");
        }
        else
        {
            anim.SetTrigger("Walk");
        }
    }
}
