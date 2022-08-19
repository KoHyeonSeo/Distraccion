using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrickBlockMatch : MonoBehaviour
{
    private PlayerMove playerMove;

    [Serializable]
    public struct ButtonTrick
    {
        public List<GameObject> trick1Blocks;
        public List<GameObject> trick2Blocks;
    }
    
    [Space]
    [Header("버튼 당 확인해야하는 trickBlock들 낮은 블록부터 설정")]
    [SerializeField] private List<ButtonTrick> trickBlocks = new List<ButtonTrick>();

    [Space]
    [Header("Button 할당")]
    [SerializeField] private List<ButtonAndUpMap> buttons = new List<ButtonAndUpMap>();

    private void Start()
    {
        playerMove = GameManager.Instance.playerGameobject.GetComponent<PlayerMove>();
        StartCoroutine("Checking");
    }
    int seq = -1;
    private IEnumerator Checking()
    {
        while (seq < buttons.Count-1)
        {
            if (buttons[seq + 1].isEnding)
            {
                seq++;
            }
            if (seq >= 0)
            {
                playerMove.trick.Clear();
                bool[] visited = new bool[10001];
                for (int j = 0; j < trickBlocks[seq].trick2Blocks.Count; j++)
                {
                    visited[j] = false;
                }
                for (int i = 0; i < trickBlocks[seq].trick1Blocks.Count; i++)
                {
                    bool isNoBlock = true;
                    GameObject minBlock = trickBlocks[seq].trick2Blocks[0];
                    GameObject myBlock1 = trickBlocks[seq].trick1Blocks[i];
                    int minIndex = 0;
                    float minDistance = 100000;
                    Vector3 myDir = (myBlock1.transform.GetChild(0).transform.position - myBlock1.transform.position).normalized;
                    myDir.x = Mathf.Abs(myDir.x); myDir.y = Mathf.Abs(myDir.y); myDir.z = Mathf.Abs(myDir.z);
                    for (int j = 0; j < trickBlocks[seq].trick2Blocks.Count; j++)
                    {
                        if (!visited[j])
                        {
                            GameObject myBlock2 = trickBlocks[seq].trick2Blocks[j];
                            Vector3 myDir2 = (myBlock2.transform.GetChild(0).transform.position - myBlock2.transform.position).normalized;
                            myDir2.x = Mathf.Abs(myDir2.x); myDir2.y = Mathf.Abs(myDir2.y); myDir2.z = Mathf.Abs(myDir2.z);
                            if (myDir == myDir2)
                            {
                                //Debug.Log($"myBlock : {myBlock1} -> " + myDir);
                                //Debug.Log($"myBlock2 : {myBlock2} -> " + myDir2);
                                if (minDistance > Vector3.Distance(myBlock1.transform.position, myBlock2.transform.position))
                                {
                                    minDistance = Vector3.Distance(myBlock1.transform.position, myBlock2.transform.position);
                                    minBlock = myBlock2;
                                    minIndex = j;
                                    isNoBlock = false;
                                }
                            }
                        }
                    }
                    if (!isNoBlock)
                    {
                        PlayerMove.trickNode tricknode;
                        tricknode.trick1 = myBlock1.GetComponent<Node>();
                        tricknode.trick2 = minBlock.GetComponent<Node>();
                        visited[minIndex] = true;
                        //Debug.Log($"myBlock1 = {myBlock1}");
                        //Debug.Log($"minBlock = {minBlock}");
                        playerMove.trick.Add(tricknode);
                    }
                }
            }
            yield return null;
        }
    }
}
