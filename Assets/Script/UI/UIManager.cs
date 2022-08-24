using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextUI textUI;
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
}
