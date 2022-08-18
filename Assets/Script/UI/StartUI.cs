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
        float _timeStartedLerping = Time.time;  // ����ð�
        float timeSinceStarted = Time.time - _timeStartedLerping;  // ����ð�
        float percentageComplete = timeSinceStarted / lerpTime;

        // Lerping 
        while (true)
        {
            // Time.deltaTime�� ��Ȯ�� ������ �ٴٸ��� ���� �� �����Ƿ� ���� ���
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            // EndOfFrame���� ��ٸ� �� while�� Finish
            if (percentageComplete >= 1) break;
            yield return new WaitForEndOfFrame();
        }
        print("Done");
    }
}
