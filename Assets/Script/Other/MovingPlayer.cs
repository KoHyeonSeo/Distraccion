using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlayer : MonoBehaviour
{
    [Range(0, 1)]
    public static float vTest = 0;

    public static List<Vector3> dataSets = new List<Vector3>();
    /// <summary>
    /// v(vTest)와 방향을 주어지면 위치값 return
    /// v에 정밀한 값을 주는것이 부드러운 이동에 도움이 될것이다.
    /// v에 fixedDeltaTime이나, deltaTime을 곱하는 것을 권장한다.(시간에 따라 이동하고 싶은 경우)
    /// 시간에 따라 이동하지 않고 싶은 경우 정밀한 값을 넣어주는 것을 권장한다.
    /// </summary>                                      
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3 BezierPoint(float v)
    {
        if (v < 0 || v > 1)
        {
            Debug.LogError("베지어의 보간값(v)는 0과 1 사이로 주어져야합니다.");
        }
        vTest = v;
        return PhysicsUtility.BezierCurve(dataSets, v);
    }

}
