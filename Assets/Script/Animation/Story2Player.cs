using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story2Player : MonoBehaviour
{
    public float walksTime = 9.5f;
    public float kneeTime = 12.5558f;
    public float IdleTime = 22f;

    private float curTime = 0;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        curTime += Time.deltaTime;
        if(0<= curTime && curTime < walksTime)
        {
        }
        else if( curTime>=walksTime &&curTime < kneeTime)
        {
            anim.SetTrigger("Walk");
        }
        else if(curTime >= kneeTime && curTime <IdleTime)
        {
            anim.SetTrigger("Knee");
        }
        else if (curTime >= IdleTime)
        {
            anim.SetTrigger("Idle");
        }
    }
}
