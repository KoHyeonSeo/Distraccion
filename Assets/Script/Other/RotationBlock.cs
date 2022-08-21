using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotationBlock : MonoBehaviour
{
    [Serializable]
    public struct ChooseAxis
    {
        public GameObject block;
        public bool X;
        public bool Y;
        public bool Z;
    }
    [Header("핸들 돌아가는 소리 설정")]
    [SerializeField] private List<AudioClip> clips = new List<AudioClip>();
    [SerializeField] private int angle = 61;

    [Space]
    [Header("핸들 중심축 설정")]
    [SerializeField] private ChooseAxis handleAxis;

    [Space]
    [Header("회전 오브젝트 설정")]
    [SerializeField] private List<ChooseAxis> chooseAxis = new List<ChooseAxis>();
    [SerializeField] private float rotationBlockSettinf = 0.1f;
    [SerializeField] private float handleSetting = 10f;

    [Space]
    [Header("핸들과 오브젝트 회전 방향 설정 (1 or -1)")]
    [SerializeField] private int handleDir = 1;
    [SerializeField] private int objectDir = -1;


    [Space]
    [Header("레버의 핸들 관련 오브젝트 등록")]
    [SerializeField] private List<GameObject> Handle = new List<GameObject>();
    [SerializeField] private bool isHandleControl = false;

    private List<float> axis = new List<float>();
    private PlayerInput playerInput;

    private float handleMove = 0;
    private float blockMove = 0;
    private int dir = 0;
    private bool isOnce = false;
    private bool afterRotate = false;
    private bool canNotRotate = false;
    private AudioSource audioSource;
    private bool audioOnce = false;
    private int clipIndex = 0;
    private bool useAudio = false;
    private void Start()
    {
        handleAxis.block = gameObject;
        //리스트 각도들 초기화
        for (int i = 0; i < chooseAxis.Count; i++)
        {
            axis.Add(0);
        }
        if (clips.Count > 0)
        {
            useAudio = true;
            audioSource = GetComponent<AudioSource>();
        }
    }
    private void Update()
    {
        if (!playerInput)
        {
            playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
            //Debug.Log(1);
        }
        else
        {
            if (useAudio)
            {
                Vector3 eulerAnglesHandle = transform.eulerAngles;
                float curRotationHandle = eulerAnglesHandle.x * Convert.ToInt32(handleAxis.X) +
                        eulerAnglesHandle.y * Convert.ToInt32(handleAxis.Y) +
                        eulerAnglesHandle.z * Convert.ToInt32(handleAxis.Z);
                for (int j = 360; j >= 0; j -= angle)
                {
                    if (Mathf.Abs(curRotationHandle - j) < 0.2f && !audioOnce)
                    {
                        audioSource.PlayOneShot(clips[clipIndex]);
                        clipIndex = clipIndex + 1 >= clips.Count ? 0 : clipIndex + 1;
                        audioOnce = true;
                        break;
                    }
                    else if (Mathf.Abs(curRotationHandle - j) > 0.2f)
                    {
                        audioOnce = false;
                    }
                }
            }
                //Debug.Log("playerInput.PointBlock = " + playerInput.PointBlock);
                //Debug.Log("gameObject = " + gameObject);
            if (!canNotRotate)
            {

                if (playerInput.PointBlock == gameObject)
                {
                    if (playerInput.InteractKey)
                    {
                        RotateBlock();
                        afterRotate = true;
                    }
                    if (Input.GetMouseButtonUp(0) && afterRotate)
                    {
                        handleMove = 0;
                        blockMove = 0;
                        isOnce = false;
                        for (int i = 0; i < chooseAxis.Count; i++)
                        {
                            Vector3 eulerAngles = chooseAxis[i].block.transform.eulerAngles;
                            float curRotation = eulerAngles.x * Convert.ToInt32(chooseAxis[i].X) +
                                    eulerAngles.y * Convert.ToInt32(chooseAxis[i].Y) +
                                    eulerAngles.z * Convert.ToInt32(chooseAxis[i].Z);
                            float targetAngle;
                            for (int j = 270; j >= 0; j -= 90)
                            {
                                if (curRotation > j)
                                {
                                    if (curRotation > j + 45)
                                    {
                                        targetAngle = j + 90;
                                    }
                                    else
                                    {
                                        targetAngle = j;
                                    }
                                    StartCoroutine(AutoRotate(curRotation, targetAngle, i));
                                    break;
                                }
                            }
                        }
                        if (isHandleControl)
                        {
                            Vector3 eulerAngles = transform.eulerAngles;
                            float curRotation = eulerAngles.x * Convert.ToInt32(handleAxis.X) +
                                    eulerAngles.y * Convert.ToInt32(handleAxis.Y) +
                                    eulerAngles.z * Convert.ToInt32(handleAxis.Z);
                            float targetAngle;
                            for (int j = 270; j >= 0; j -= 90)
                            {
                                if (curRotation > j)
                                {
                                    if (curRotation > j + 45)
                                    {
                                        targetAngle = j + 90;
                                    }
                                    else
                                    {
                                        targetAngle = j;
                                    }
                                    StartCoroutine(AutoRotateHandle(curRotation, targetAngle));
                                    break;
                                }
                            }
                        }
                        afterRotate = false;
                    }
                }
            }
        }
    }
    private IEnumerator AutoRotateHandle(float current, float target)
    {
        float timing = 0;

        float firstAngle = (target > current) ? (target + 12.5f) : (target - 12.5f);
        float nextAngle = firstAngle;

        while (timing < 1.0f)
        {
            float angle = Mathf.Lerp(current, nextAngle, timing);

           transform.rotation = Quaternion.Euler(new Vector3(angle * Convert.ToInt32(handleAxis.X),
                    angle * Convert.ToInt32(handleAxis.Y),
                    angle * Convert.ToInt32(handleAxis.Z)));

            timing += Time.deltaTime * 5;

            if (timing >= 1.0f)
            {
                transform.rotation = Quaternion.Euler(new Vector3(nextAngle * Convert.ToInt32(handleAxis.X),
                        nextAngle * Convert.ToInt32(handleAxis.Y),
                        nextAngle * Convert.ToInt32(handleAxis.Z)));

                if (nextAngle != target)
                {
                    current = nextAngle;
                    nextAngle = target;
                    timing = 0;
                }
            }

            yield return null;
        }
        target = (target >= 360) ? (target - 360) : (target);
        transform.rotation = Quaternion.Euler(new Vector3(target * Convert.ToInt32(handleAxis.X),
                target * Convert.ToInt32(handleAxis.Y),
                target * Convert.ToInt32(handleAxis.Z)));
    }
    private IEnumerator AutoRotate(float current, float target, int index)
    {
        float timing = 0;

        float firstAngle = (target > current) ? (target + 12.5f) : (target - 12.5f);
        float nextAngle = firstAngle;

        while (timing < 1.0f)
        {
            float angle = Mathf.Lerp(current, nextAngle, timing);

            chooseAxis[index].block.transform.rotation = Quaternion.Euler(new Vector3(angle * Convert.ToInt32(chooseAxis[index].X),
                    angle * Convert.ToInt32(chooseAxis[index].Y),
                    angle * Convert.ToInt32(chooseAxis[index].Z)));

            timing += Time.deltaTime * 5;

            if (timing >= 1.0f)
            {
                chooseAxis[index].block.transform.rotation = Quaternion.Euler(new Vector3(nextAngle * Convert.ToInt32(chooseAxis[index].X),
                        nextAngle * Convert.ToInt32(chooseAxis[index].Y),
                        nextAngle * Convert.ToInt32(chooseAxis[index].Z)));

                if (nextAngle != target)
                {
                    current = nextAngle;
                    nextAngle = target;
                    timing = 0;
                }
            }

            yield return null;
        }
        target = (target >= 360) ? (target - 360) : (target);
        chooseAxis[index].block.transform.rotation = Quaternion.Euler(new Vector3(target * Convert.ToInt32(chooseAxis[index].X),
                target * Convert.ToInt32(chooseAxis[index].Y),
                target * Convert.ToInt32(chooseAxis[index].Z)));
    }
    private void RotateBlock()
    {
        //마우스 입력에 따른 회전
        Vector2 rotation = new Vector2(playerInput.XMouseOut, playerInput.YMouseOut);
        if (!isOnce && rotation.x != 0)
        {
            isOnce = true;
            dir = (int)rotation.normalized.x;
            //Debug.Log(dir);
        }
        Vector2 handle = rotation * handleSetting;
        handleMove = (Mathf.Abs(handle.x) + Mathf.Abs(handle.y)) * dir * handleDir;
        //핸들 회전
        transform.Rotate(handleMove * Convert.ToInt32(handleAxis.X), handleMove * Convert.ToInt32(handleAxis.Y), handleMove * Convert.ToInt32(handleAxis.Z));

        //회전 오브젝트 회전
        for (int i = 0; i < chooseAxis.Count; i++)
        {
            Vector2 rotateBlock = rotation * rotationBlockSettinf;
            blockMove = (Mathf.Abs(rotateBlock.x) + Mathf.Abs(rotateBlock.y)) * dir * objectDir;
            //여러 오브젝트들로 묶여진 회전블록 회전
            if (chooseAxis[i].block.transform.childCount > 0)
            {
                chooseAxis[i].block.transform.RotateAround(chooseAxis[i].block.transform.GetChild(0).position, new Vector3(blockMove * Convert.ToInt32(chooseAxis[i].X),
                    blockMove * Convert.ToInt32(chooseAxis[i].Y),
                    blockMove * Convert.ToInt32(chooseAxis[i].Z)), Time.deltaTime * 100);
            }
            //하나의 오브젝트인 회전블록 회전
            else
            {
                chooseAxis[i].block.transform.Rotate(blockMove * Convert.ToInt32(chooseAxis[i].X),
                    blockMove * Convert.ToInt32(chooseAxis[i].Y),
                    blockMove * Convert.ToInt32(chooseAxis[i].Z));
            }
        }
    }
    public IEnumerator HandleShortSetting()
    {
        canNotRotate = true;
        for(int i = 0; i < Handle.Count; i++)
        {
            while (Vector3.Distance(Handle[i].transform.localScale, new Vector3(Handle[i].transform.localScale.x, 0.5f, Handle[i].transform.localScale.z)) > 0.01f)
            {
                Handle[i].transform.localScale = Vector3.Lerp(Handle[i].transform.localScale, new Vector3(Handle[i].transform.localScale.x, 0.5f, Handle[i].transform.localScale.z), 0.1f);
                yield return null;
            }
            yield return null;
        }
    }
    public IEnumerator HandleLongSetting()
    {
        canNotRotate = false;
        for (int i = 0; i < Handle.Count; i++)
        {
            while (Vector3.Distance(Handle[i].transform.localScale, new Vector3(Handle[i].transform.localScale.x, 1, Handle[i].transform.localScale.z)) > 0.01f)
            {
                Handle[i].transform.localScale = Vector3.Lerp(Handle[i].transform.localScale, new Vector3(Handle[i].transform.localScale.x, 1, Handle[i].transform.localScale.z), 0.1f);
                yield return null;
                continue;
            }
            yield return null;
        }
    }
}