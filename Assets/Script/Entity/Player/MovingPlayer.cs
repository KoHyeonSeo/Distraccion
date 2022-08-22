using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    [Range(0, 1)]
    public static float vTest = 0;

    public static List<Vector3> dataSets = new List<Vector3>();
    /// <summary>
    /// v(vTest)�� ������ �־����� ��ġ�� return
    /// v�� ������ ���� �ִ°��� �ε巯�� �̵��� ������ �ɰ��̴�.
    /// v�� fixedDeltaTime�̳�, deltaTime�� ���ϴ� ���� �����Ѵ�.(�ð��� ���� �̵��ϰ� ���� ���)
    /// �ð��� ���� �̵����� �ʰ� ���� ��� ������ ���� �־��ִ� ���� �����Ѵ�.
    /// </summary>                                      
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3 BezierPoint(float v)
    {
        if (v < 0 || v > 1)
        {
            Debug.LogError("�������� ������(v)�� 0�� 1 ���̷� �־������մϴ�.");
        }
        vTest = v;
        return PhysicsUtility.BezierCurve(dataSets, v);
    }

}
