using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mic : MonoBehaviour
{
    //public Slider sound_slider;
    public AudioClip clip;
    int sampleRate = 44100;
    private float[] samples;
    public float rmsValue;
    public float modulate = 2000;
    public int resultValue;
    public int cutValue = 15;
    public int maxValue = 100;
    public int Volume { get; private set; }
    private void Start()
    {
        samples = new float[sampleRate];
        clip = Microphone.Start(Microphone.devices[0].ToString(), true, 1, sampleRate);
    }

    private void Update()
    {
        //offset은 시작 위치
        clip.GetData(samples, 0); //-1f ~ 1f
        float sum = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            //sum += Mathf.Abs(samples[i]);
            sum += samples[i] * samples[i];
        }
        rmsValue = Mathf.Sqrt(sum / samples.Length);
        rmsValue *= modulate;
        rmsValue = Mathf.Clamp(rmsValue, 0, maxValue);
        resultValue = Mathf.RoundToInt(rmsValue);
        if (resultValue < cutValue)
        {
            resultValue = 0;
        }
        Volume = resultValue;
    }

}
