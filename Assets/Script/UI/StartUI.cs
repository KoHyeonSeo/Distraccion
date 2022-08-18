using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    public CanvasGroup uiElement;
    
    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 0.5f)
    {
        float _timeStartedLerping = Time.time;  // 현재시간
        float timeSinceStarted = Time.time - _timeStartedLerping;  // 경과시간
        float percentageComplete = timeSinceStarted / lerpTime;

        // Lerping 
        while (true)
        {
            // Time.deltaTime은 정확한 지점에 다다르지 못할 수 있으므로 직접 계산
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            // EndOfFrame까지 기다린 뒤 while문 Finish
            if (percentageComplete >= 1) break;
            yield return new WaitForEndOfFrame();
        }
        print("Done");
    }
}
