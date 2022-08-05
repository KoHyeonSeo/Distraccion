using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MissionFail/test2")]
public class TestMissionFail2 : MissionFail
{
    [SerializeField] private Material mat;
    [SerializeField] private Material prevMat;
    private float curTime = 0;
    public override void MissionFailSetting()
    {

    }
}
