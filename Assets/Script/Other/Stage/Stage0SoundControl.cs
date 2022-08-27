using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0SoundControl : MonoBehaviour
{
    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioClip sound2;
    [SerializeField] private AudioClip sound3;
    [SerializeField] private Portal portal;
    [SerializeField] private float distance = 5f;

    private AudioSource audioSource;

    private float curTime = 0;
    private float curTime2 = 0;
    private bool isOnce = false;
    private bool isOnce2 = false;
    private Vector3 endPos;
    private PlayerInput player;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
        portal.isPause = true;
        endPos = transform.position + Vector3.up * distance;
    }
    private void Update()
    {
        if (!player)
        {
            player = GameManager.Instance.playerGameobject.GetComponent<PlayerInput>();
        }
        if (!portal.isStart)
        {
            curTime += Time.deltaTime;
            if (curTime >= sound1.length-1 && !isOnce)
            {
                audioSource.loop = true;
                audioSource.clip = sound2;
                audioSource.Play();
                isOnce = true;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * 0.1f);
            player.playerControl = true;
            if (!isOnce2)
            {
                audioSource.Stop();
                audioSource.loop = false;
                audioSource.volume = 1;
                audioSource.clip = sound3;
                audioSource.Play();
                isOnce2 = true;
            }
            curTime2 += Time.deltaTime;
            if (curTime2 >= sound3.length - 8)
            {
                player.playerControl = false;
                portal.isPause = false;
            }
        }
    }


}
