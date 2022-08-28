using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.SceneManagement;

public class DirectionAction_End2 : MonoBehaviour
{
    PlayableDirector pd;


    [Header("���� scene")]
    [SerializeField] private int nextStage;


    void Start()
    {
        pd = GetComponent<PlayableDirector>();

    }


    void Update()
    {
        // 10.5�ʿ��� ����īƮ �ڽ����� vcam3 �־��ֱ�
        if (pd.time >= 10.5f)
        {
            
        }


        //����ð��� �ٵȴٸ�
        if (pd.duration - pd.time <= 0.05f)
        {
            SceneManager.LoadScene(nextStage);
        }
    }
}
