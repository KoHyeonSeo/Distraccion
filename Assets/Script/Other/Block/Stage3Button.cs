using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Button : MonoBehaviour
{
    [SerializeField] private float buttonTime = 0.5f;
    [SerializeField] private float buttonDown = -0.1f;
    [SerializeField] private AudioClip audioClip;
    private bool isOnce = false;
    private float curTime = 0;
    private AudioSource audioSource;
    public StairMoving stair;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOnce)
        {
            isOnce = true;
            stair.isRotating = true;
            CameraControl.Instance.OnShakeCamera(1, 0.05f);
            audioSource.PlayOneShot(audioClip);
            StartCoroutine("ButtonDown");
        }
    }

    IEnumerator ButtonDown()
    {
        while (curTime < buttonTime)
        {
            curTime += Time.deltaTime;
            transform.position += new Vector3(0, buttonDown, 0) * Time.deltaTime;
            yield return null;
        }
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    
}
