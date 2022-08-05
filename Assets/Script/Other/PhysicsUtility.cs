using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsUtility : MonoBehaviour
{ /// <summary>
  /// 조합구하는 공식
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
    /// 베지에 곡선의 최종 보간된 위치 <br/>
    /// </summary>
    /// <param name="dataset"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Vector3 BezierCurve(List<Vector3> dataset, float t)
    {
        if (dataset.Count <= 1)
        {
            throw new InvalidOperationException("두 개 이상의 점의 개수를 넣어야합니다.");
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
