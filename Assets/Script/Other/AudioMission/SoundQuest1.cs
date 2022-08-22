using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundQuest1 : MonoBehaviour
{
    [SerializeField] private Mic mic;
    [SerializeField] private GameObject resetButton;
    
    private int curGround = 0;
    private PlayerInput input;
    private List<Vector3> firstPosition = new List<Vector3>();
    private void Start()
    {
        if(GameManager.Instance.playerGameobject)
            input = GameManager.Instance.playerGameobject.GetComponent<PlayerInput>();  
        for(int i = 0; i < transform.childCount; i++)
        {
            firstPosition.Add(transform.GetChild(i).position);
        }
    }
    private void Update()
    {
        if(!input)
            input = GameManager.Instance.playerGameobject.GetComponent<PlayerInput>();

        if (curGround < transform.childCount)
            transform.GetChild(curGround).transform.position = firstPosition[curGround] + new Vector3(0, mic.rmsValue / 10, 0);
        if (input.EnterKey)
        {
            curGround = curGround + 1 > transform.childCount ? curGround : curGround + 1;
        }
    }
    public void OnButtonReset()
    {
        StartCoroutine("ResetTurn");
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = firstPosition[i];
        }
        curGround = 0;
    }
    public IEnumerator ResetTurn()
    {
        float angle = 0;
        while (true)
        {
            if (angle >= 360)
            {
                yield break;

            }
            resetButton.GetComponent<RectTransform>().RotateAround(resetButton.GetComponent<RectTransform>().position, new Vector3(0, 0, 1), -Time.deltaTime * 400);
            angle += Time.deltaTime * 400;
            yield return null;
        }
    }
}
