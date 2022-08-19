using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartUI : MonoBehaviour
{
    // singleton
    public static StartUI Instance;

    public Image fadeImg;
    public List<GameObject> fadeList = new List<GameObject>();

    public float loadSpeed;
    public float stringSpeed;
    public float fadeOutSpeed;
    public float buttonSpeed;

    public bool isReady = false;
    public bool isClicked = false;

    TextMeshProUGUI loadText;
    CanvasGroup stageName;
    Image buttonImage;
    PlayerInput playerInput;

    private void Awake()
    {
        Instance = this;
        GameObject load = fadeList[0];
        loadText = load.GetComponent<TextMeshProUGUI>();
        GameObject stage = fadeList[1];
        stageName = stage.GetComponent<CanvasGroup>();
        GameObject button = fadeList[2];
        buttonImage = button.GetComponent<Image>();
    }

    public void Start()
    {
        // StartUI 끝날때까지 player 움직이지 않도록 설정
        fadeImg.gameObject.SetActive(true);
        fadeImg.color = new Color(0, 0, 0, 1.0f);
        if (!isReady)
        {
            StartCoroutine("FadeOut");
            isReady = true;
            print("4444444");
        }
        print("55555555");
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1);

        // 1. Loading
        float t = 0;
        while (t <1)
        {
            t += loadSpeed * Time.deltaTime;
            loadText.color = new Color(1, 1, 1, t);
            yield return null;
        }
        loadText.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(1);


        // 2. StageName
        t = 0;
        while (t < 1)
        {
            t += stringSpeed * Time.deltaTime;
            stageName.alpha = t;
            yield return null;
        }
        stageName.alpha = 1;
        yield return new WaitForSeconds(1);


        // 3. Fade Out 
        t = 0;
        while (t < 0.5f)
        {
            t += fadeOutSpeed * Time.deltaTime;
            loadText.color = new Color(1, 1, 1, 1-t);
            fadeImg.color = new Color(0, 0, 0, 1-t);
            yield return null;
        }
        loadText.color = new Color(1, 1, 1, 0);
        fadeImg.color = new Color(0, 0, 0, 0.5f);

        // 4. Button
        t = 0;
        while (t < 1)
        {
            t += buttonSpeed * Time.deltaTime;
            buttonImage.color = new Color(1, 1, 1, t);
            yield return null;
        }
        buttonImage.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(1);

        // 버튼 누르면 - stageName, 버튼, panel 이미지 모두 사라지게
        t = 0;
        while (true)
        {
            if (isClicked)
            {
                while (t < 1)
                {
                    t += buttonSpeed * Time.deltaTime;
                    stageName.alpha = 1 - t;
                    buttonImage.color = new Color(1, 1, 1, 1 - t);
                    fadeImg.color = new Color(0, 0, 0, 0.5f - t / 2);
                    print("Ready");
                    print("111111111");
                    yield return null;
                }
                stageName.alpha = 0;
                buttonImage.color = new Color(1, 1, 1, 0);
                fadeImg.color = new Color(0, 0, 0, 0);
                isClicked = false;
                print("22222222");
            }
            break;
        }
    }
}
