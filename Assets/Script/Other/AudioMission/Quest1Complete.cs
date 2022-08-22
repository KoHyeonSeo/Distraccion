using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1Complete : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("미션 성공");
        }
    }
}
