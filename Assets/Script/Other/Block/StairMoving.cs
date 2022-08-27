using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StairMoving : MonoBehaviour
{
    [Serializable]
    public struct ChooseAxis
    {
        public bool X;
        public bool Y;
        public bool Z;
    }
    [SerializeField] private ChooseAxis choiceAxis;
    [SerializeField] private Transform point;
    [SerializeField] private float blockAngle = 180;
    private float axis = 0;
    public bool isRotating = false;

    private void Update()
    {
        if (isRotating)
        {
            if (axis < blockAngle)
            {
                transform.RotateAround(point.position, new Vector3(Convert.ToInt32(choiceAxis.X),
                    Convert.ToInt32(choiceAxis.Y),
                    Convert.ToInt32(choiceAxis.Z)), Time.deltaTime * blockAngle * 0.5f);
                axis += Time.deltaTime * blockAngle * 0.5f;
            }
        }
    }
}
