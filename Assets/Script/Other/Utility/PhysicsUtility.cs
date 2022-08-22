using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsUtility : MonoBehaviour
{ /// <summary>
  /// ���ձ��ϴ� ����
  /// </summary>
  /// <param name="n"></param>
  /// <param name="i"></param>
  /// <returns></returns>

    public static float Combination(int n, int i)
    {
        if (i < 0)
        {
            return 1;
        }
        else if (i > n)
        {
            return 1;
        }
        float p = 1;
        for (int j = 0; j < i; j++)
        {
            p *= (n - j);
        }
        float rP = 1;
        for (int k = 1; k <= i; k++)
        {
            rP *= k;
        }
        return p / rP;
    }
    /// <summary>
    /// ������ ��� ���� ������ ��ġ <br/>
    /// </summary>
    /// <param name="dataset"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Vector3 BezierCurve(List<Vector3> dataset, float t)
    {
        if (dataset.Count <= 1)
        {
            throw new InvalidOperationException("�� �� �̻��� ���� ������ �־���մϴ�.");
        }
        int n = dataset.Count - 1;
        Vector3 B = new Vector3();
        for (int i = 0; i <= n; i++)
        {
            B += Combination(n, i) * Mathf.Pow(t, i) * Mathf.Pow((1 - t), n - i) * dataset[i];
        }
        return B;
    }
}
