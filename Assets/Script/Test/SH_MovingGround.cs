using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_MovingGround : MonoBehaviour
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
            transform.localPosition = PhysicsUtility.BezierCurve(dataSets, vTest) + new Vector3(0, 0.5f, 0);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rayDir, out hit, 5))
            {
                Debug.DrawRay(hit.point, hit.normal, Color.cyan, 200);
                transform.up = hit.normal;
                transform.up = -rayDir;
            }
            //transform.forward = rayDir;
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






































//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SH_MovingGround : MonoBehaviour
//{

//    [Range(0, 1)]
//    public float vTest = 0;
//    public float speed = 0.01f;

//    //Rigidbody rigid;
//    //Vector3 normal;

//    public List<Vector3> dataSets = new List<Vector3>();
//    //private void Awake()
//    //{
//    //    rigid = GetComponent<Rigidbody>();
//    //}
//    //private void Start()
//    //{
//    //    StartCoroutine(Move(1));
//    //}
//    //public void CallMoving(float v)
//    //{
//    //    StartCoroutine(Move(v));

//    //}
//    private IEnumerator Move(float v)
//    {
//        //while (true)
//        //{
//            while (vTest < v)
//            {
//                //float sp = speed * Time.fixedDeltaTime;
//                Vector3 rayDir = PhysicsUtility.BezierCurve(dataSets, vTest) - transform.position;
//                transform.localPosition = PhysicsUtility.BezierCurve(dataSets, vTest);
//                //transform.forward = rayDir;
//                // 베지어 커브 점을 향해 ray를 쏴서 normal 벡터 방향을 구하고 player의 up방향과 일치시키기

//                //if (Physics.Raycast(transform.position, rayDir, out hit, 5))
//                //{
//                //    Debug.DrawRay(hit.point, hit.normal, Color.green, 200);
//                //    transform.up = hit.normal;
//                //}
//                //transform.forward = PhysicsUtility.BezierCurve(dataSets, vTest);
//                //transform.LookAt(PhysicsUtility.BezierCurve(dataSets, vTest));
//                vTest = Mathf.Clamp(vTest, 0, v);
//                yield return new WaitForFixedUpdate();
//            //}
//            //yield return null;
//        }
//        //enabled = false;
//    }
//}


