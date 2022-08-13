using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHaveStairMoving : MonoBehaviour
{
    [SerializeField] private GameObject stair;
    public void CallStairMoving()
    {
        stair.GetComponent<StairMoving>().isRotating = true;
    }
}
