using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionFail : ScriptableObject
{
    public GameObject Enemy { get; set; }
    public GameObject Player { get; set; }

    public abstract void MissionFailSetting();
}
