using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScared : MonoBehaviour
{
    [SerializeField] private float brave = 100;
    [SerializeField] private float scaredSpeed = 5;
    [SerializeField] private Slider scaredSlider;

    /// <summary>
    /// Damage를 받으면 용기 지수에서 깎인다.
    /// </summary>
    /// <param name="damage"></param>
    private void Update()
    {
        if(scaredSlider.value != brave)
        {
            scaredSlider.value = brave;
        }
    }
    public void Damage(float damage)
    {
        StartCoroutine("Scaring",damage);
    }
    //상태바를 낮추기 위함
    private IEnumerator Scaring(float damage)
    {
        float s = 0;
        float first = brave;
        while (s < damage)
        {
            s += Time.deltaTime * scaredSpeed;
            brave -= Time.deltaTime * scaredSpeed;
            if (s >= damage)
            {
                brave = first - damage;
            }
            yield return null;
        }
        if (brave <= 0)
        {
            RunAway();
        }
    }
    //용기 지수가 0보다 낮아지면 호출
    //Die랑 같은 개념
    public void RunAway()
    {
        for(int i = 0; i < GameManager.Instance.ItemProp.Count; i++)
        {
            GameManager.Instance.SetDontHaveItem = i;
        }
        Debug.Log("쥬금");
        SceneManager.LoadScene("GameOverScene");
    }

}
