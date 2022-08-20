using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemUI = new List<GameObject>();

    private int index;

    private void Start()
    {
        ChangeItem();
    }
    private void Update()
    {
        index = Mathf.Clamp(index, 0, itemUI.Count -1);
        if(index != GameManager.Instance.CurItemIndex || !GameManager.Instance.ItemProp[index].isHaveItem)
        {
            ChangeItem();
        }
    }
    private void ChangeItem()
    {

        index = GameManager.Instance.CurItemIndex;
        int s = itemUI.Count;
        bool isCheckHaveItem = true;
        int i = index;
        while (true)
        {
            if (!GameManager.Instance.ItemProp[i].isHaveItem)
            {
                if (s > 0)
                {
                    s--;
                    if (i + 1 >= itemUI.Count)
                    {
                        i = 0;
                    }
                    else
                    {
                        i++;
                    }
                }
                else
                {
                    isCheckHaveItem = false;
                    break;
                }
            }
            else
            {
                index = i;
                break;
            }
        }
        if (isCheckHaveItem)
        {
            GameManager.Instance.CurItemIndex = index;
            for (int j = 0; j < itemUI.Count; j++)
            {
                if (j != index)
                {
                    itemUI[j].SetActive(false);
                }
                else
                {
                    itemUI[j].SetActive(true);
                }
            }
        }
        else
        {
            for (int j = 0; j < itemUI.Count; j++)
            {
                itemUI[j].SetActive(false);
            }
        }
    }
}
