using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonAndUpMap : MonoBehaviour
{

    [Serializable]
    public struct MovingBlock
    {
        public List<GameObject> blocks;
        public Vector3 dir;
        public float waitingTime;
        public float distance;
        public float speed;
    }
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float buttonTime = 0.5f; 
    [SerializeField] private GameObject door;
    [SerializeField] private bool isHaveDoor;

    [Space]
    [Header("������ ��� ��� �� ����")]
    [SerializeField] private List<MovingBlock> movingBlocks = new List<MovingBlock>();

    public bool isMoving = false;
    public bool isEnding = false;
    private AudioSource audioSource;
    private float curTime = 0;
    private float buttonCurTime = 0;
    private List<List<Vector3>> movingPoints = new List<List<Vector3>>();
    private bool isOnce = false;
    private bool isEndingOnce = false;
    private bool isOnceChecking = false;
    private bool isOnceShaking = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //MovingPoint ����Ʈ �ʱ�ȭ
        for (int i = 0; i < movingBlocks.Count; i++)
        {
            movingPoints.Add(new List<Vector3>());
            for (int j = 0; j < movingBlocks[i].blocks.Count; j++)
            {
                movingPoints[i].Add(Vector3.zero);
            }
        }
        //MovingPoint ����Ʈ�� ������ ���
        for (int i = 0; i < movingBlocks.Count; i++)
        {
            for (int j = 0; j < movingBlocks[i].blocks.Count; j++)
            {
                movingPoints[i][j] =
                    movingBlocks[i].blocks[j].transform.position +
                    movingBlocks[i].dir.normalized * movingBlocks[i].distance;

            }
        }
    }
    private void Update()
    {
        if (isEnding && !isEndingOnce)
        {
            isOnceShaking = false;
            isEndingOnce = true;
            for (int i = 0; i < movingBlocks.Count; i++)
            {
                for (int j = 0; j < movingBlocks[i].blocks.Count; j++)
                {
                    if (movingBlocks[i].blocks[j].GetComponent<IsHaveStairMoving>())
                    {
                        movingBlocks[i].blocks[j].GetComponent<IsHaveStairMoving>().CallStairMoving();
                    }
                }
            }
        }
        if (isMoving && !isEnding)
        {
            if (!isOnceShaking)
            {
                audioSource.PlayOneShot(audioClip);
                CameraControl.Instance.OnShakeCamera(1, 0.05f);
                isOnceShaking = true;
            }
            if (isHaveDoor && !isOnceChecking)
            {
                isOnceChecking = true;
                door.GetComponent<DoorBlockRotate>().isRotating = true;
            }
            if (isHaveDoor)
            {
                curTime += Time.deltaTime;
                if (curTime > 3)
                {
                    curTime = 0;
                    isHaveDoor = false;
                }
            }
            else
            {
                StartCoroutine("Moving");
            }
        }
    }
    IEnumerator ButtonDown()
    {
        while (buttonCurTime < buttonTime)
        {
            buttonCurTime += Time.deltaTime;
            transform.position += Vector3.down * Time.deltaTime;
            yield return null;
        }
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
    private IEnumerator Moving()
    {
        while (true)
        {
            curTime += Time.deltaTime * 0.01f;
            isEnding = true;
            for (int i = 0; i < movingBlocks.Count; i++)
            {
                //waiting��ٸ�����
                if (curTime < movingBlocks[i].waitingTime)
                {
                    isEnding = false;
                    continue;
                }
                //�̵� �Ϸ� ��
                if(Vector3.Distance(movingBlocks[i].blocks[0].transform.position, movingPoints[i][0]) < 0.5f)
                {
                    continue;
                }
                //�̵� �̿Ϸ��
                else
                {
                    isEnding = false;
                    //�̵�
                    for(int j = 0; j < movingBlocks[i].blocks.Count; j++)
                    {
                        movingBlocks[i].blocks[j].transform.position =
                            Vector3.Lerp(movingBlocks[i].blocks[j].transform.position, movingPoints[i][j], Time.deltaTime * movingBlocks[i].speed);
                    }
                }
            }
            //��� ����� �̵��� ���ƴٸ�
            if (isEnding)
            {
                Debug.Log("End");
                isMoving = false;
                StopAllCoroutines();
            }
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOnce)
        {
            isOnce = true;
            isMoving = true;
            //CameraControl.Instance.OnShakeCamera(1, 0.3f);
            StartCoroutine("ButtonDown");
        }
    }
}
