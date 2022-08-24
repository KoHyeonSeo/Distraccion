using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest1EndingQuest : MonoBehaviour
{
    [SerializeField] private Mic mic;
    [SerializeField] private GameObject audioSlider;

    private Slider slider;
    public bool isPlaying = false;
    private void Start()
    {
        mic.maxValue = 200;
        slider = audioSlider.GetComponent<Slider>();
        slider.maxValue = mic.maxValue;
        audioSlider.SetActive(false);
    }
    private void Update()
    {
        if (isPlaying)
        {
            if (!audioSlider.activeSelf)
                audioSlider.SetActive(true);
            if (mic.rmsValue >= mic.maxValue)
            {
                UIManager.Instance.TextUIStarting3();
                isPlaying = false;
            }
            slider.value = mic.rmsValue;
        }
    }
}
