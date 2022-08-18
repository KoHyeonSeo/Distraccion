using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{

    [Range(0, 1)]
    public float vTest = 0;
    public float speed = 0.01f;

    //Rigidbody rigid;

    public List<Vector3> dataSets = new List<Vector3>();
    //private void Awake()
    //{
    //    rigid = GetComponent<Rigidbody>();
    //}
    private void Start()
    {
        StartCoroutine(Move());
    }
    private IEnumerator Move()
    {
        while (true)
        {
            while (vTest < 1)
            {
                float sp = speed * Time.fixedDeltaTime;
                transform.position = PhysicsUtility.BezierCurve(dataSets, vTest);
                transform.LookAt(PhysicsUtility.BezierCurve(dataSets, vTest));
                vTest = Mathf.Clamp01(vTest + sp);  
                yield return new WaitForFixedUpdate();
            }
            //while (vTest > 0)
            //{
            //    float sp = speed * Time.fixedDeltaTime;
            //    vTest = Mathf.Clamp01(vTest - sp);
            //    transform.position = PhysicsUtility.BezierCurve(dataSets, vTest);
            //    transform.LookAt(PhysicsUtility.BezierCurve(dataSets, vTest));
            //    yield return new WaitForFixedUpdate();
            //}
        }
    }
}