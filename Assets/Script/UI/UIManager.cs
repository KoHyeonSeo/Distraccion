using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextUI textUI;
    [SerializeField] private TextUI textUI2;
    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }
    private void Start()
    {
        textUI.Starting();
    }
    public void TextUIStarting()
    {
        textUI.Starting();
    }
    public void TextUIStarting2()
    {
        textUI2.Starting();
    }
}
