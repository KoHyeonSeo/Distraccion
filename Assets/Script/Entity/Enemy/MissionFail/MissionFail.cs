using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionFail : ScriptableObject
{
    /// <summary>
    /// �� Mission�� Enemy�� ������ ��� ������Ƽ
    /// </summary>
    public GameObject Enemy { get; set; }
    /// <summary>
    /// �� Mission�� �����ϴ� Player�� ������ ��� ������Ƽ
    /// </summary>
    public GameObject Player { get; set; }

    public abstract void MissionFailSetting();
}
