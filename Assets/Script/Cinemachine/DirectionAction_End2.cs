using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.SceneManagement;

public class DirectionAction_End2 : MonoBehaviour
{
    PlayableDirector pd;


    [Header("다음 scene")]
    [SerializeField] private int nextStage;


    void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

    float logoTime = 0;
    void Update()
    {
        //재생시간이 다된다면
        if (pd.duration - pd.time <= 0.05f)
        { 
            SceneManager.LoadScene(nextStage);
        }
    }
}
