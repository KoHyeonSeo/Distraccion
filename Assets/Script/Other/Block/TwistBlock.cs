using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistBlock : MonoBehaviour
{
    [Header("블록 설정")]
    [SerializeField] private List<GameObject> block = new List<GameObject>();
    [SerializeField] private float buttonTime = 0.5f;
    [SerializeField] private float targetAngle = 90f;
    [SerializeField] private float speed = 25f;
    
    private List<float> angle = new List<float>();
    private List<List<float>> blocksAngle = new List<List<float>>();
    private List<int> childNum = new List<int>();

    private bool isOnce = false;
    public bool isTwist = false;
    private float curTime = 0;
    private void Start()
    {
        for (int i = 0; i < block.Count; i++)
        {
            childNum.Add(block[i].transform.childCount);  
            angle.Add(targetAngle / childNum[i]);
            blocksAngle.Add(new List<float>());
            for (int j = 0; j < childNum[i]; j++)
            {
                blocksAngle[i].Add(0);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            isOnce = true;
            StartCoroutine(ButtonDown());
            isTwist = true;
        }
        if (isTwist)
        {
            for (int i = 0; i < blocksAngle.Count; i++)
            {
                StartCoroutine(TwistingBlock(block[i],i));
            }
        }

    }
    IEnumerator TwistingBlock(GameObject block,int index)
    {
        for (int i = 1; i < childNum[index]; i++)
        {
            if (blocksAngle[index][i] < angle[index] * i)
            {
                // Twist해야하는 블록 이름이 'TwistBlock_x'일 경우 x축 기준 회전
                if (block.name == "TwistBlock_x")
                {
                    // 자식위치, X축 기준으로 Time.fixedDeltaTime * speed(속도)로 움직임
                    block.transform.GetChild(i).RotateAround(block.transform.GetChild(i).position,
                    new Vector3(-1, 0, 0),  Time.fixedDeltaTime* speed);  // 0.009736f
                    // 각도 누적값
                    blocksAngle[index][i] += Time.fixedDeltaTime * speed;//0.009736f * speed;
                }
                else
                {
                    block.transform.GetChild(i).RotateAround(block.transform.GetChild(i).position,
                    new Vector3(0, -1, 0), Time.fixedDeltaTime * speed);
                    blocksAngle[index][i] += Time.fixedDeltaTime * speed;
                }
            }
            yield return null;
        }
    }
    IEnumerator ButtonDown()
    {
        while (curTime < buttonTime)
        {
            curTime += Time.deltaTime;
            transform.position += Vector3.down * Time.deltaTime;
            yield return null;
        }
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOnce)
        {
            isOnce = true;
            isTwist = true;
            CameraControl.Instance.OnShakeCamera(1);
            StartCoroutine(ButtonDown());
        }
    }
}
