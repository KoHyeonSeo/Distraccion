using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int nextStage = 0;
    [SerializeField] private float distance = 3f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject effect;
    private bool isOnce = false;
    private bool isStart = false;
    private float value = 0;
    private void Start()
    {
        effect.SetActive(false);
    }
    private void Update()
    {
        if (isStart)
        {
            value += speed * Time.deltaTime;
            effect.transform.position += Vector3.down * speed * Time.deltaTime;
            if(value >= distance)
            {
                SceneManager.LoadScene(nextStage);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOnce)
        {
            isOnce = true;
            isStart = true;
            effect.SetActive(true);
        }
    }
}
