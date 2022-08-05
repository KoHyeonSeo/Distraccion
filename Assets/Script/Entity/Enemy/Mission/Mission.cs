using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Mission/Mission")]
public class Mission : ScriptableObject
{
    [SerializeField] private GameObject missionItem;
    public GameObject chooseItem { get; set; }
    public GameObject Enemy { get; set; }
    
    public bool MissionStart = false;
    public void MissionSetting()
    {
        if (!Enemy.GetComponent<Enemy>().IsCheckingItem)
        {
            //Debug.Log($"{MissionStart}");
            if (MissionStart)
            {
                //Debug.Log("3. 미션 스타트");
                if (chooseItem.name == missionItem.name)
                {
                    //Debug.Log("4. 아이템 일치");
                    Enemy.GetComponent<Enemy>().IsCheckingItem = true;
                    Enemy.GetComponent<Enemy>().ColliderObject = chooseItem;
                    Enemy.GetComponent<Enemy>().IsStartComplete = true;
                }
                else
                {
                    //Debug.Log("4. 아이템 불일치");
                    Enemy.GetComponent<Enemy>().IsCheckingItem = false;
                    MissionStart = false;
                }
            }
            else
            {
                //Debug.Log("-미션 안함");
                if (Enemy.GetComponent<EnemyDetection>().Target)
                {
                    //Debug.Log("-Player 닿음");

                    if (!Enemy.GetComponent<Enemy>().IsStartComplete && !Enemy.GetComponent<Enemy>().IsStartFail && !Enemy.GetComponent<Enemy>().IsDead)
                    {
                        if (Enemy.GetComponent<EnemyDetection>().Target.CompareTag("Player"))
                        {
                            Enemy.GetComponent<Enemy>().ColliderObject = Enemy.GetComponent<EnemyDetection>().Target;
                            Enemy.GetComponent<Enemy>().IsStartFail = true;
                        }
                    }
                }
            }
        }
    }
    
}
