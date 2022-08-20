using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OnClickExplore()
    {
        Debug.Log("Explore");
        SceneManager.LoadScene("Stage0");
    }

    public void OnClickCredits()
    {
        Debug.Log("Credits");
    }

    public void OnClickSettings()
    {
        Debug.Log("Settings");
    }

    public void OnClickContact()
    {
        Debug.Log("Contact");
    }

    public void OnClickQuitGame()
    {
#if UNITY_EDITOR
UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif 
    }
}
