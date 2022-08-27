using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fairy2 : MonoBehaviour
{
    [SerializeField] TextUI endingText;
    [SerializeField] int nextStage;
    [SerializeField] private float animTime = 1f;

    private float curTime = 0;
    private Animator animator;
    private AudioSource audioSource;
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();  
    }
    private bool isOnce = false;
    private void Update()
    {
        if (endingText.isEnd)
        {
            curTime += Time.deltaTime;
            animator.SetTrigger("Cast Spell");
            if (!isOnce)
            {
                isOnce = true;
                audioSource.Play();
            }
            if (curTime > animTime)
            {
                SceneManager.LoadScene(nextStage);
            }
        }
    }
}
