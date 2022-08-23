using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2Camera : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private GameObject Player;
    private void Start()
    {
        Player = GameManager.Instance.playerGameobject;
    }
    private void Update()
    {
        Vector3 tran = transform.position;
        tran.y = Mathf.Lerp(transform.position.y, Player.transform.position.y, speed * Time.deltaTime);
        transform.position = tran;

    }
}
