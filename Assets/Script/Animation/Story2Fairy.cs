using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story2Fairy : MonoBehaviour
{
    public float magicTime = 21f;

    private float curTime = 0;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime>=magicTime)
        {
            anim.SetTrigger("Magic");
        }
    }
}
