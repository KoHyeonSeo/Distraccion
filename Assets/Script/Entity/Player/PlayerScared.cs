using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScared : MonoBehaviour
{
    [SerializeField] private float brave = 100;
    [SerializeField] private float scaredSpeed = 5;
    [SerializeField] private Slider scaredSlider;

    /// <summary>
    /// Damage�� ������ ��� �������� ���δ�.
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
    //���¹ٸ� ���߱� ����
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
    //��� ������ 0���� �������� ȣ��
    //Die�� ���� ����
    public void RunAway()
    {
        Debug.Log("���");
    }

}
