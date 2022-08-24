using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public bool control = true;
    [SerializeField] private TextUI textUI;
    [SerializeField] private TextUI textUI2;
    [SerializeField] private TextUI textUI3;
    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }
    private void Start()
    {
        if (textUI)
            textUI.playerControl = control;
        else if (textUI2)
            textUI2.playerControl = true;
        else if (textUI3)
            textUI3.playerControl = true;
        textUI.Starting();
    }
    public void TextUIStarting()
    {
        textUI.playerControl = control;
        if (textUI2)
            textUI2.playerControl = true;
        else if (textUI3)
            textUI3.playerControl = true;
        
        textUI.Starting();
    }
    public void TextUIStarting2()
    {
        textUI2.playerControl = control;
        if (textUI)
            textUI.playerControl = true;
        else if (textUI3)
            textUI3.playerControl = true;
        
        textUI2.Starting();
    }
    public void TextUIStarting3()
    {
        textUI3.playerControl = control;
        if (textUI2)
            textUI2.playerControl = true;
        else if (textUI)
            textUI.playerControl = true;

        textUI3.Starting();
    }
}
