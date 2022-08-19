using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void OnClickStartButton()
    {
        StartUI.Instance.isClicked = true;
    }
}
