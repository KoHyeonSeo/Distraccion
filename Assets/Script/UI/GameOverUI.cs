using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private float startTime = 12f;
    [SerializeField] private float bigSpeed = 5f;
    private float curTime = 0;
    private Vector2 endScale;
    private bool isEnd = false;
    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetComponent<CanvasScaler>().referenceResolution = new Vector2(1,1);
        endScale = new Vector2(800, 600);
    }
    private void Update()
    {
        if (!isEnd)
        {
            curTime += Time.deltaTime;
            if (curTime > startTime)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                Vector2 start = transform.GetChild(0).GetComponent<CanvasScaler>().referenceResolution;
                transform.GetChild(0).GetComponent<CanvasScaler>().referenceResolution = Vector2.Lerp(start, endScale, Time.deltaTime * bigSpeed);
                if (Vector2.Distance(start, endScale) < 0.1f)
                {
                    transform.GetChild(0).GetComponent<CanvasScaler>().referenceResolution = endScale;
                    isEnd = true;
                }
            }
        }
    }
    public void OnButtonYes()
    {
        SceneManager.LoadScene(0);
    }
    public void OnButtonQuit()
    {
        Application.Quit();
    }
}
