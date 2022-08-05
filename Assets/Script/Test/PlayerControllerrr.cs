using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerControllerrr : MonoBehaviour
{
    public Transform currentCube;
    public Transform clickedCube;
    public Transform startCube;
    private List<Transform> path = new List<Transform>();
    private bool isMoving = false;
    int k = 0;
    private void Update()
    {
        RayCastDown();
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(mouseRay, out mouseHit))
            {
                if (mouseHit.transform.GetComponent<Walkable>() != null)
                {
                    path.Clear();
                    startCube = currentCube;
                    clickedCube = mouseHit.transform;
                    currentCube.GetComponent<Walkable>().MyPath.MyVisited = true;
                    Checking(currentCube.gameObject);
                    isMoving = true;
                    Init();
                }
            }
        }
        else if (isMoving)
        {
            StartCoroutine(Move());
        }
    }
    private void Init()
    {
        k = 0;
        for (int i = 0; i < path.Count; i++)
        {
            path[i].GetComponent<Walkable>().MyPath.MyVisited = false;
        }
    }
    private IEnumerator Move()
    {
        Debug.Log(path.Count);
        while (k < path.Count)
        {
            transform.LookAt(path[k].position + new Vector3(0, 1f, 0));
            transform.position = Vector3.Lerp(transform.position, path[k].position + new Vector3(0, 1f, 0), Time.deltaTime*0.1f);
            Vector3 myDist = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 targetDist = new Vector3(path[k].position.x, 0, path[k].position.z);
            if(Vector3.Distance( myDist,targetDist) < 0.2f)
            {
                k++;
            }
            yield return null;
        }
        isMoving = false;
        yield return null;

    }
    private void Checking(GameObject block) 
    {
        for (int i = 0; i < block.GetComponent<Walkable>().possiblePaths.Count; i++)
        {
            block.GetComponent<Walkable>().possiblePaths[i].target.GetComponent<Walkable>().Call();
            if (block.GetComponent<Walkable>().possiblePaths[i] == null)
            {
                Debug.Log("null");
                return;
            }
            else if (block.GetComponent<Walkable>().possiblePaths[i].target == clickedCube)
            {
                Debug.Log("Clicked");
                path.Add(block.GetComponent<Walkable>().possiblePaths[i].target);
                return;
            }
            //어느 블록이 더 가까운지 봐야할듯
            else if (!block.GetComponent<Walkable>().possiblePaths[i].target.GetComponent<Walkable>().MyPath.MyVisited)
            {
                Debug.Log("is not visited");
                block.GetComponent<Walkable>().possiblePaths[i].target.GetComponent<Walkable>().MyPath.MyVisited = true;
                path.Add(block.GetComponent<Walkable>().possiblePaths[i].target);
                Checking(block.GetComponent<Walkable>().possiblePaths[i].target.gameObject);
            }
        }


        //for(int i = 0; i < block.GetComponent<Walkable>().possiblePaths.Count; i++)
        //{
        //block.GetComponent<Walkable>().Call();
        //if (block == clickedCube)
        //{
        //    Debug.Log("Clicked");
        //    path.Add(block.transform);
        //    return;
        //}
        ////어느 블록이 더 가까운지 봐야할듯
        //else
        //{
        //    Debug.Log("is not visited");
        //    Transform choice;
        //    float min = float.MaxValue;
        //    for (int i = 0; i < block.GetComponent<Walkable>().possiblePaths.Count; i++)
        //    {
        //        //Vector3.Distance()
        //    }

        //    //block.GetComponent<Walkable>().possiblePaths[i].target.GetComponent<Walkable>().MyPath.MyVisited = true;
        //    //path.Add(block.GetComponent<Walkable>().possiblePaths[i].target);
        //    //Checking(block.GetComponent<Walkable>().possiblePaths[i].target.gameObject);
        //}
        //}
    }

    public void RayCastDown()
    {
        Ray playerRay = new Ray(transform.position, -transform.up);
        RaycastHit playerHit;
        if(Physics.Raycast(playerRay, out playerHit))
        {
            if(playerHit.transform.GetComponent<Walkable>() != null)
            {
                currentCube = playerHit.transform;
            }
        }
    }
}
