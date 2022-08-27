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
    public bool isStart = false;
    public bool isPause = false;
    private float value = 0;
    private Vector3 endPos;

    private void Start()
    {
        effect.SetActive(false);
        endPos = effect.transform.position + Vector3.down * distance;
    }
    private void Update()
    {
        if (isStart)
        {
            value += speed * Time.deltaTime;
            effect.transform.position = Vector3.Lerp(effect.transform.position, endPos, value);
            if(value >= distance)
            {
                if (!isPause)
                {
                    SceneManager.LoadScene(nextStage);
                }
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
