using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUpGround : MonoBehaviour
{
    [SerializeField] private SoundQuest2 soundQuest2;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            for(int i = 0; i < soundQuest2.MovingBlocks.Count; i++)
            {
                if (soundQuest2.MovingBlocks[i] == gameObject)
                {
                    soundQuest2.curGround = i;
                }
            }
        }
    }
}
