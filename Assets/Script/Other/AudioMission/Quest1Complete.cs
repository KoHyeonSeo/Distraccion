using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1Complete : MonoBehaviour
{
    bool isOnce = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isOnce)
            {
                isOnce = true;
                UIManager.Instance.TextUIStarting2();
            }
        }
    }
}
