using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    [Header("대화 리스트 (형식: [GameObject 이름]/대화내용 )")]
    [SerializeField] private List<string> talkingList = new List<string>();
    [SerializeField] private List<GameObject> talkingPeople = new List<GameObject>();
    [SerializeField] private float waitTime = 2;
    [SerializeField] private float tTime = 0.5f;

    private MiddleStagePlayerMove playerMove;
    private List<Text> texts = new List<Text>();
    private float curTime = 0;
    public bool isEnd = false;
    private void Awake()
    {
        for(int i = 0; i < talkingPeople.Count; i++)
        {
            texts.Add(talkingPeople[i].transform.GetChild(0).GetComponent<Text>());
        }
    }
    private void Start()
    {
        if (GameManager.Instance.playerGameobject)
        {
            playerMove = GameManager.Instance.playerGameobject.GetComponent<MiddleStagePlayerMove>();
        }
    }
    public void Starting()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        if (!playerMove)
        {
            playerMove = GameManager.Instance.playerGameobject.GetComponent<MiddleStagePlayerMove>();
        }
        playerMove.isPlaying = false;
        StartCoroutine("Texting");
    }
    IEnumerator Texting()
    {
        for (int i = 0; i < talkingList.Count; i++)
        {
            string[] talking = talkingList[i].Split('/');
            //Debug.Log(talking[0]);
            //Debug.Log(talking[1]);
            int talkinIndex = 0;
            for (int j = 0; j < talkingPeople.Count; j++)
            {
                if (talkingPeople[j].name.Contains(talking[0]))
                {
                    talkinIndex = j;
                    talkingPeople[j].SetActive(true);
                }
                else
                {
                    talkingPeople[j].SetActive(false);
                }
            }
            texts[talkinIndex].text = "";
            //Debug.Log(talking[1].Length);
            for (int j = 0; j < talking[1].Length; j++)
            {
                //Debug.Log(talking[1][j]);
                texts[talkinIndex].text += talking[1][j];
                curTime = 0;
                while (curTime < tTime)
                {
                    curTime += Time.deltaTime;
                    yield return null;
                }
            }
            curTime = 0;
            while (curTime < waitTime)
            {
                curTime += Time.deltaTime;
                yield return null;
            }
            yield return null;
        }
        transform.GetChild(0).gameObject.SetActive(false);
        isEnd = true;
        playerMove.isPlaying = true;
    }
}
