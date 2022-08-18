using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{

    [Range(0, 1)]
    public float vTest = 0;
    public float speed = 0.01f;

    //Rigidbody rigid;
    //Vector3 normal;

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
        while (vTest < 1)
        {
            float sp = speed * Time.fixedDeltaTime;
            Vector3 rayDir = PhysicsUtility.BezierCurve(dataSets, vTest) - transform.position;
            transform.localPosition = PhysicsUtility.BezierCurve(dataSets, vTest);
            transform.forward = rayDir;
            // 베지어 커브 점을 향해 ray를 쏴서 normal 벡터 방향을 구하고 player의 up방향과 일치시키기

            //if (Physics.Raycast(transform.position, rayDir, out hit, 5))
            //{
            //    Debug.DrawRay(hit.point, hit.normal, Color.green, 200);
            //    transform.up = hit.normal;
            //}
            //transform.forward = PhysicsUtility.BezierCurve(dataSets, vTest);
            //transform.LookAt(PhysicsUtility.BezierCurve(dataSets, vTest));
            vTest = Mathf.Clamp01(vTest + sp);                       
            yield return new WaitForFixedUpdate();
        }
        enabled = false;
    }
}