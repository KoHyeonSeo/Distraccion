using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeActive : MonoBehaviour
{
    public List<GameObject> stairs = new List<GameObject>();


    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            stairs.Add(transform.GetChild(i).gameObject);
        }
    }


    void Update()
    {
        // 계단이 해당 z rotation값 내에 있을 때만 계단 레이어 노드로!
        if (Mathf.Abs(transform.localEulerAngles.z) >= 177 && Mathf.Abs(transform.localEulerAngles.z) <= 181)
        {
            for (int i = 0; i < 3; i++)
            {
                stairs[i].layer = LayerMask.NameToLayer("Node");
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                stairs[i].layer = LayerMask.NameToLayer("Default");
            }
        }
    }
}
