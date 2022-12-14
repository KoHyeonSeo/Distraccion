using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartSceneManager : MonoBehaviour
{
    public GameObject gameName;
    public GameObject options;
    public GameObject credits;
    public GameObject settings;
    public GameObject audioOn;
    public GameObject audioOff;
    public GameObject contact;
    public GameObject control;

    AudioListener audioListener;

    public float menuSpeed;



    void Start()
    {
        audioListener = Camera.main.GetComponent<AudioListener>();
        audioListener.enabled = true;
        StartCoroutine("MenuFadeIn");
        credits.SetActive(false);
        settings.SetActive(false);
        audioOff.SetActive(false);
        contact.SetActive(false);
    }

    private IEnumerator MenuFadeIn()
    {
        yield return new WaitForSeconds(1);

        // GameName FadeIn
        float t = 0;
        TextMeshProUGUI name = gameName.GetComponent<TextMeshProUGUI>();
        while (t < 1)
        {
            t += menuSpeed * Time.deltaTime;
            name.color = new Color(1, 1, 1, t);
            yield return null;
        }
        name.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(1);


        // Option FadeIn
        t = 0;
        CanvasGroup cg = options.GetComponent<CanvasGroup>();
        while (t < 1)
        {
            t += menuSpeed * Time.deltaTime;
            cg.alpha = t;
            yield return null;
        }
        cg.alpha = 1;
        yield return new WaitForSeconds(1);
    }

    public void OnClickBack()
    {
        gameName.SetActive(true);
        options.SetActive(true);
        credits.SetActive(false);
        settings.SetActive(false);
        contact.SetActive(false);
        control.SetActive(false);
        Debug.Log("Back");
    }

    public void OnClickExplore()
    {
        Debug.Log("Explore");
        SceneManager.LoadScene(1);
    }

    public void OnClickCredits()
    {
        gameName.SetActive(false);
        options.SetActive(false);
        credits.SetActive(true);
        Debug.Log("Credits");
    }
    public void OnClickControl()
    {
        control.SetActive(true);
    }

    public void OnClickSettings()
    {
        gameName.SetActive(false);
        options.SetActive(false);
        settings.SetActive(true);
        Debug.Log("Settings");
    }
      
    public void OnClickAudioToOff()
    {
        AudioListener.volume = 0;
        audioOn.SetActive(false);
        audioOff.SetActive(true);
        Debug.Log("Audio Off");
    }
    public void OnClickAudioToOn()
    {
        AudioListener.volume = 1;
        audioOff.SetActive(false);
        audioOn.SetActive(true);
        Debug.Log("Audio On");
    }

    public void OnClickContact()
    {
        gameName.SetActive(false);
        options.SetActive(false);
        contact.SetActive(true);
        Debug.Log("Contact");
    }

    public void OnClickGithub()
    {
        Application.OpenURL("https://github.com/KoHyeonSeo/Distraccion");
    }

    public void OnClickQuitGame()
    {
        Debug.Log("Quit");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif 
    }
}
