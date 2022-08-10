using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionComplete : ScriptableObject
{
    /// <summary>
    /// 이 Mission의 Enemy의 정보를 담는 프로퍼티
    /// </summary>
    public GameObject Enemy { get; set; }
    /// <summary>
    /// 이 Mission을 수행하는 Player의 정보를 담는 프로퍼티
    /// </summary>
    public GameObject Player { get; set; }
    /// <summary>
    /// 이 미션을 수행하는데 필요한 아이템
    /// </summary>
    public GameObject Item { get; set; }
    /// <summary>
    /// 시작할때, false로 시작
    /// MonoBehavior의 Start의 역할을 한다.
    /// </summary>
    public bool Start { get; set; }
    public abstract void MissionCompleteSetting();
}
