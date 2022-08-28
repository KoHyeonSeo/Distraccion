using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.SceneManagement;

public class DirectionAction_End : MonoBehaviour
{
    PlayableDirector pd;

    [Header("����īƮ")]
    public CinemachineDollyCart dollyCart;
    public CinemachineVirtualCamera vCam3;

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
            dollyCart.enabled = true;
        }

        //����ð��� �ٵȴٸ�
        if (pd.duration - pd.time <= 0.05f)
        {
            SceneManager.LoadScene(nextStage);
        }
    }
}
