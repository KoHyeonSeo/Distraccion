using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class PlayerEndAnim : MonoBehaviour
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
        if (pd.time >= 10 && pd.time <= 25)
        {
            anim.SetTrigger("Move");
        }
        else if (pd.time >= 58 && pd.time <= 91)
        {
            anim.SetTrigger("Move");

        }
        else if (pd.time >= 26 && pd.time <= 29)
        {
            anim.SetTrigger("Walk");
        }
        else
        {
            anim.SetTrigger("Idle");
        }
    }
}
