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

    [SerializeField] private float buttonTime = 0.5f; 

    [Space]
    [Header("움직일 블록 등록 및 설정")]
    [SerializeField] private List<MovingBlock> movingBlocks = new List<MovingBlock>();
    
    private float curTime = 0;
    private float buttonCurTime = 0;
    private List<List<Vector3>> movingPoints = new List<List<Vector3>>();
    private bool isOnce = false;
    public bool isMoving = false;
    private bool isEnding = false;
    private bool isEndingOnce = false;

    private void Start()
    {
        for (int i = 0; i < movingBlocks.Count; i++)
        {
            movingPoints.Add(new List<Vector3>());
            for (int j = 0; j < movingBlocks[i].blocks.Count; j++)
            {
                movingPoints[i].Add(Vector3.zero);
            }
        }

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
            StartCoroutine("Moving");
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
                //waiting기다리는중
                if (curTime < movingBlocks[i].waitingTime)
                {
                    isEnding = false;
                    continue;
                }
                //이동 완료 시
                if(Vector3.Distance(movingBlocks[i].blocks[0].transform.position, movingPoints[i][0]) < 0.5f)
                {
                    continue;
                }
                //이동 미완료시
                else
                {
                    isEnding = false;
                    //이동
                    for(int j = 0; j < movingBlocks[i].blocks.Count; j++)
                    {
                        movingBlocks[i].blocks[j].transform.position =
                            Vector3.Lerp(movingBlocks[i].blocks[j].transform.position, movingPoints[i][j], Time.deltaTime * movingBlocks[i].speed);
                    }
                }
            }
            //모든 블록이 이동을 마쳤다면
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
