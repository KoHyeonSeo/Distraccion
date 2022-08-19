using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AutoRotation : MonoBehaviour
{
    [Serializable]
    public struct ChooseAxis
    {
        public GameObject block;
        public bool X;
        public bool Y;
        public bool Z;
    }
    [Header("��ϼ���")]
    [SerializeField] private List<ChooseAxis> chooseAxis = new List<ChooseAxis>();
    [SerializeField] private float blockAngle = 90f;
    [SerializeField] private float buttonTime = 0.5f;

    [Space]
    [Header("�Ҹ�")]
    [SerializeField] private AudioClip audioClip;

    private List<float> axis = new List<float>();
    private bool isOnce = false;
    private float curTime = 0;
    public bool isRotate = false;
    private AudioSource audioSource;
    
    private void Awake()
    {
        for (int i = 0; i < chooseAxis.Count; i++)
        {
            axis.Add(0);
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isRotate)
        {
            for (int i = 0; i < chooseAxis.Count; i++)
            {
                if (axis[i] < blockAngle)
                {                    
                    chooseAxis[i].block.transform.RotateAround(chooseAxis[i].block.transform.GetChild(0).position, new Vector3(1 * Convert.ToInt32(chooseAxis[i].X),
                        1 * Convert.ToInt32(chooseAxis[i].Y),
                        1 * Convert.ToInt32(chooseAxis[i].Z)), Time.deltaTime * 90f);
                    axis[i] += Time.deltaTime * 90f;
                }
            }
        }

        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    isOnce = true;
        //    isRotate = true;
        //    CameraControl.Instance.OnShakeCamera(1, 0.3f);
        //    StartCoroutine("ButtonDown");
        //}
    }
    IEnumerator ButtonDown()
    {
        while (curTime < buttonTime)
        {
            curTime += Time.deltaTime;
            transform.position += Vector3.down  * Time.deltaTime;
            yield return null;
        }
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOnce)
        {
            isOnce = true;
            isRotate = true;
            CameraControl.Instance.OnShakeCamera();
            audioSource.PlayOneShot(audioClip);
            StartCoroutine("ButtonDown");
        }
    }
}
