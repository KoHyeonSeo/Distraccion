using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootStep : MonoBehaviour
{
    AudioSource footStep;
    private void Start()
    {
        footStep = GetComponent<AudioSource>();
    }
    public void OnMoveSound()
    {
        footStep.Play();
    }
    public void OnMoveSoundOff()
    {
        footStep.Stop();
    }
}
