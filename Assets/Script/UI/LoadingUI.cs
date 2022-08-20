using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingUI : MonoBehaviour
{
    public Image fadeImg;
    public List<GameObject> fadeList = new List<GameObject>();

    public float loadSpeed;
    public float stringSpeed;
    public float fadeOutSpeed;
    public float buttonSpeed;
    public float buttonOutSpeed;

    public bool gameReady = false;
    private bool isClicked = false;
    private bool isOnce = true;

    TextMeshProUGUI loadText;
    CanvasGroup stageName;
    Image buttonImage;
    PlayerInput playerInput;

    private void Awake()
    {
        GameObject load = fadeList[0];
        loadText = load.GetComponent<TextMeshProUGUI>();
        GameObject stage = fadeList[1];
        stageName = stage.GetComponent<CanvasGroup>();
        GameObject button = fadeList[2];
        buttonImage = button.GetComponent<Image>();
    }

    public void Start()
    {
        // StartUI ���������� player �������� �ʵ��� ����
        fadeImg.gameObject.SetActive(true);
        fadeImg.color = new Color(0, 0, 0, 1.0f);
        StartCoroutine("FadeOut");
    }
    private void Update()
    {
        if (isClicked && isOnce)
        {
            StartCoroutine("GameReady");
            isClicked = false;
            isOnce = false;
        }
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1);

        // 1. Loading
        float t = 0;
        while (t < 1)
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
            loadText.color = new Color(1, 1, 1, 1 - t*2);
            fadeImg.color = new Color(0, 0, 0, 1 - t);
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
    }

    private IEnumerator GameReady()
    {
        float t = 0;
        while (t < 1)
        {
            t += buttonOutSpeed * Time.deltaTime;
            print("0000000");
            stageName.alpha = 1 - t;
            buttonImage.color = new Color(1, 1, 1, 1 - t);
            fadeImg.color = new Color(0, 0, 0, 0.5f - t / 2);
            yield return null;
        }
        stageName.alpha = 0;
        buttonImage.color = new Color(1, 1, 1, 0);
        fadeImg.color = new Color(0, 0, 0, 0);
        print("1111111");
        yield return new WaitForSeconds(1);
    }
    public void OnClickStartButton()
    {
        isClicked = true;
    }
}
    // ��ư ������ - stageName, ��ư, panel �̹��� ��� �������
    //t = 0;
    //    if (isClicked)
    //    {
    //            while (t < 1)
    //            {
    //                t += buttonOutSpeed * Time.deltaTime;
    //                print("0000000");
    //                stageName.alpha = 1 - t;
    //                buttonImage.color = new Color(1, 1, 1, 1 - t);
    //                fadeImg.color = new Color(0, 0, 0, 0.5f - t / 2);
    //                yield return null;
    //            }
    //            stageName.alpha = 0;
    //            buttonImage.color = new Color(1, 1, 1, 0);
    //            fadeImg.color = new Color(0, 0, 0, 0);
    //            print("1111111");
    //            isClicked = false;

    //isClicked = false;
    //else
    //{
    //    isReady = true;
    //    print("2222222");
    //    yield break;
    //}
    //yield return null;
    //print("33333333");

        //while (true)
        //{
        //    if (isClicked)
        //    {
        //        while (t < 1)
        //        {
        //            t += buttonOutSpeed * Time.deltaTime;
        //            print("0000000");
        //            stageName.alpha = 1 - t;
        //            buttonImage.color = new Color(1, 1, 1, 1 - t);
        //            fadeImg.color = new Color(0, 0, 0, 0.5f - t / 2);
        //            yield return null;
        //        }
        //        stageName.alpha = 0;
        //        buttonImage.color = new Color(1, 1, 1, 0);
        //        fadeImg.color = new Color(0, 0, 0, 0);
        //        isClicked = false;
        //        print("1111111");
        //    }
        //    else
        //    {
        //        isReady = true;
        //        print("2222222");
        //        yield break;
        //    }
        //    yield return null;
        //    print("33333333");
        //}
    
