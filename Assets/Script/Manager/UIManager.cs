using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextUI textUI;
    [SerializeField] private TextUI textUI2;
    [SerializeField] private TextUI textUI3;

    private bool control1;
    private bool control2;
    private bool control3;  
    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }
    private void Start()
    {
        if (textUI)
        {
            control1 = textUI.playerControl;
            textUI.playerControl = true;
        }
        else if (textUI2)
        {
            control2 = textUI2.playerControl;
            textUI2.playerControl = true;
        }
        else if (textUI3)
        {
            control3 = textUI3.playerControl;
            textUI3.playerControl = true;
        }
    }
    public void TextUIStarting()
    {
        textUI.playerControl = control1;
        if (textUI2)
            textUI2.playerControl = true;
        else if (textUI3)
            textUI3.playerControl = true;
        
        textUI.Starting();
    }
    public void TextUIStarting2()
    {
        textUI2.playerControl = control2;
        if (textUI)
            textUI.playerControl = true;
        else if (textUI3)
            textUI3.playerControl = true;
        
        textUI2.Starting();
    }
    public void TextUIStarting3()
    {
        textUI3.playerControl = control3;
        if (textUI2)
            textUI2.playerControl = true;
        else if (textUI)
            textUI.playerControl = true;

        textUI3.Starting();
    }
    public void OnButtonSkip()
    {
        SceneManager.LoadScene(3);
    }
}
