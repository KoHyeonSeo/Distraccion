using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionComplete : ScriptableObject
{
    /// <summary>
    /// �� Mission�� Enemy�� ������ ��� ������Ƽ
    /// </summary>
    public GameObject Enemy { get; set; }
    /// <summary>
    /// �� Mission�� �����ϴ� Player�� ������ ��� ������Ƽ
    /// </summary>
    public GameObject Player { get; set; }
    /// <summary>
    /// �� �̼��� �����ϴµ� �ʿ��� ������
    /// </summary>
    public GameObject Item { get; set; }
    /// <summary>
    /// �����Ҷ�, false�� ����
    /// MonoBehavior�� Start�� ������ �Ѵ�.
    /// </summary>
    public bool Start { get; set; }
    public abstract void MissionCompleteSetting();
}
