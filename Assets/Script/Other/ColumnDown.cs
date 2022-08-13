using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnDown : MonoBehaviour
{
    PlayerMove player;
    [SerializeField] private Transform startNode;
    float speed;

    void Start()
    {
        player = GameManager.Instance.playerGameobject.GetComponent<PlayerMove>();
    }

    
    void Update()
    {
        if (player.currentNode == startNode && transform.position.y > -2.81f)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }
}
