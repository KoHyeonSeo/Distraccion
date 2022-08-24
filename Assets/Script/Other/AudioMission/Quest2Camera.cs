using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2Camera : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float fairySpeed = 3;
    [SerializeField] private GameObject Fairy;
    private GameObject Player;
    public bool fairyCamera = false;
    private void Start()
    {
        if (GameManager.Instance.playerGameobject)
        {
            Player = GameManager.Instance.playerGameobject;
        }
    }
    private void Update()
    {
        if (!Player)
        {
            Player = GameManager.Instance.playerGameobject;
        }
        if (!fairyCamera)
        {
            Vector3 tran = transform.position;
            tran.y = Mathf.Lerp(transform.position.y, Player.transform.position.y + 5, speed * Time.deltaTime);
            transform.position = tran;
        }
        else
        {
            Vector3 tran = transform.position;
            tran.y = Mathf.Lerp(transform.position.y, Fairy.transform.position.y + 5, fairySpeed * Time.deltaTime);
            transform.position = tran;
        }
    }
}
