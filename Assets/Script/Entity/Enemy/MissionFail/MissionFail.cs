using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionFail : ScriptableObject
{
    /// <summary>
    /// 이 Mission의 Enemy의 정보를 담는 프로퍼티
    /// </summary>
    public GameObject Enemy { get; set; }
    /// <summary>
    /// 이 Mission을 수행하는 Player의 정보를 담는 프로퍼티
    /// </summary>
    public GameObject Player { get; set; }

    public abstract void MissionFailSetting();
}
