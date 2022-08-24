using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2Complete : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.TextUIStarting2();
        }
    }
}
