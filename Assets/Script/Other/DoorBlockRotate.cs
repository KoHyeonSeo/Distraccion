using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBlockRotate : MonoBehaviour
{
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    private float axis = 0;
    public bool isRotating = false;
    private int cnt = 0;
    private void Update()
    {
        if (isRotating)
        {
            if (cnt == 0)
            {
                if (axis < 90)
                {
                    transform.RotateAround(point1.position, new Vector3(1,0,0), Time.deltaTime * 90 * 0.5f);
                    axis += Time.deltaTime * 90 * 0.5f;
                }
                else
                {
                    axis = 0;
                    cnt++;
                }
            }
            else if(cnt == 1)
            {
                if (axis < 90)
                {
                    transform.RotateAround(point2.position, new Vector3(0, -1, 0), Time.deltaTime * 90 * 0.5f);
                    axis += Time.deltaTime * 90 * 0.5f;
                }
                else
                {
                    isRotating = false;
                }
            }
        }
    }
}
