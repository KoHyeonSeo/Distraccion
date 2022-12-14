using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundQuest2 : MonoBehaviour
{
    public Mic mic;

    public int curGround;
    public List<GameObject> MovingBlocks = new List<GameObject>();
    private List<Vector3> firstPosition = new List<Vector3>();
    
    private void Start()
    {
        mic.maxValue = 140;
        for (int i = 0; i < transform.childCount; i++)
        {
            firstPosition.Add(transform.GetChild(i).position);
            MovingBlocks.Add(transform.GetChild(i).gameObject);
        }
    }
    private void Update()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if (i != curGround)
            {
                transform.GetChild(i).position = firstPosition[i];
            }
        }
        if (curGround < transform.childCount)
        {
            transform.GetChild(curGround).transform.position = firstPosition[curGround] + new Vector3(0, mic.rmsValue / 14, 0);
        }

    }
}
