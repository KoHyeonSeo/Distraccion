using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionComplete : ScriptableObject
{
    public GameObject Enemy { get; set; }
    public GameObject Player { get; set; }
    public GameObject Item { get; set; }
    public bool isOnce { get; set; }
    public abstract void MissionCompleteSetting();
}
