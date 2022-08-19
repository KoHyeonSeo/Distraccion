using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartUI : MonoBehaviour
{
    public Image fadeImg;
    public List<GameObject> fadeList = new List<GameObject>();

    public float loadSpeed;
    public float stringSpeed;
    public float fadeOutSpeed;
    public float buttonSpeed;

    private bool isReady = false;

    TextMeshProUGUI loadText;
    CanvasGroup stageName;
    Image panelImage;
    Image buttonImage;

    private void Awake()
    {
        GameObject load = fadeList[0];
        loadText = load.GetComponent<TextMeshProUGUI>();
        GameObject stage = fadeList[1];
        stageName = stage.GetComponent<CanvasGroup>();
        GameObject panel = fadeList[2];
        panelImage = panel.GetComponent<Image>();
        GameObject button = fadeList[3];
        buttonImage = button.GetComponent<Image>();
    }

    public void Start()
    {
        fadeImg.gameObject.SetActive(true);
        fadeImg.color = new Color(0, 0, 0, 1.0f);
        if (!isReady)
        {
            StartCoroutine("FadeOut");
        }
    }

    private IEnumerator FadeOut()
    {
        // 1. Loading
        float t = 0;
        while (t <1)
        {
            t += loadSpeed * Time.deltaTime;
            loadText.color = new Color(1, 1, 1, t);
            yield return null;
        }
        loadText.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(2);


        // 2. StageName
        t = 0;
        while (t < 1)
        {
            t += stringSpeed * Time.deltaTime;
            stageName.alpha = t;
            yield return null;
        }
        stageName.alpha = 1;
        yield return new WaitForSeconds(5);


        // 3. Fade Out 
        t = 0;
        while (t < 0.5f)
        {
            t += fadeOutSpeed * Time.deltaTime;
            loadText.color = new Color(1, 1, 1, 0);
            panelImage.color = new Color(1, 1, 1, 1-t);
            yield return null;
        }
        panelImage.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(2);


        // 4. Button
        // 버튼 누르면 - stageName, 버튼, panel 이미지 모두 변경
        t = 0;
        while (t < 1)
        {
            t += buttonSpeed * Time.deltaTime;
            buttonImage.color = new Color(1, 1, 1, t);
            yield return null;
        }
        buttonImage.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(2);


        // 5. Fade Out Completely & Game Start
        t = 0.5f;
        while (t < 1)
        {
            t += fadeOutSpeed * Time.deltaTime;
            panelImage.color = new Color(1, 1, 1, 1-t);
            yield return null;
        }
        panelImage.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(2);
    }


    //public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 0.5f)
    //{
    //    float _timeStartedLerping = Time.time;  // 현재시간
    //    float timeSinceStarted = Time.time - _timeStartedLerping;  // 경과시간
    //    float percentageComplete = timeSinceStarted / lerpTime;

    //    // Alpha Lerping 
    //    while (true)
    //    {
    //        // Time.deltaTime은 정확한 지점에 다다르지 못할 수 있으므로 직접 계산
    //        timeSinceStarted = Time.time - _timeStartedLerping;
    //        percentageComplete = timeSinceStarted / lerpTime;

    //        float currentValue = Mathf.Lerp(start, end, percentageComplete);
    //        cg.alpha = currentValue;

    //        // EndOfFrame까지 기다린 뒤 while문 Finish
    //        if (percentageComplete >= 1) break;
    //        yield return new WaitForEndOfFrame();
    //    }
    //    print("Done");
    //}
}
