using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTrick : MonoBehaviour
{
    PlayerMove player;

    void Start()
    {
        player = GameManager.Instance.playerGameobject.GetComponent<PlayerMove>();
    }

    
    void Update()
    {
        if (player.currentNode == gameObject)
        {

        }
    }
}
