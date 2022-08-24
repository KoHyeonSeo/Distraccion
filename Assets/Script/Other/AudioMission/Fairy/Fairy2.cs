using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fairy2 : MonoBehaviour
{
    [SerializeField] TextUI endingText;
    [SerializeField] int nextStage;

    private void Update()
    {
        if (endingText.isEnd)
        {
            SceneManager.LoadScene(nextStage);
        }
    }
}
