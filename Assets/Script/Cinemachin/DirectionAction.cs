using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.SceneManagement;

public class DirectionAction : MonoBehaviour
{
    [SerializeField] private int nextStage;
    PlayableDirector pd;

    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.Play();
    }
    private void Update()
    {
        //����ð��� �ٵȴٸ�
        if (pd.time >= pd.duration)
        {
            SceneManager.LoadScene(nextStage);
        }
    }
}
