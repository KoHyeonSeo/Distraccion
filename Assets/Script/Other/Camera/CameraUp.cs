using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUp : MonoBehaviour
{
    [System.Serializable]
    public struct CameraEvent
    {
        public ButtonAndUpMap button;
        public float distance;
    }

    [Header("Button별 카메라 움직임 (순서대로 할당)")]
    [SerializeField] private List<CameraEvent> events = new List<CameraEvent>();
    [SerializeField] private float speed = 3f;
    private int curIndex = 0;
    private float dist = 0f;
    private void Update()
    {
        if(curIndex < events.Count)
        {
            if (events[curIndex].button.isMoving || events[curIndex].button.isEnding)
            {
                transform.position+= Vector3.up * speed * Time.deltaTime;
                dist += speed * Time.deltaTime;
                if (dist >= events[curIndex].distance)
                {
                    dist = 0f;
                    curIndex++;
                }
            }
        }
    }
}
