using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    [SerializeField] private List<ButtonAndUpMap> buttons = new List<ButtonAndUpMap>();
    private void Update()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            if (Input.GetKeyDown((KeyCode)(49 + i)))
            {
                buttons[i].isMoving = true;
            }
        }
    }
}
