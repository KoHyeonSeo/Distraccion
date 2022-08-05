using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllllll : MonoBehaviour
{
    public Transform currentCube;
    public Transform clickedCube;
    public Transform startCube;
    public List<Transform> path = new List<Transform>();
    private bool isMoving = false;
    int k = 0;
    private void Start()
    {
        RayCastDown();
        startCube = currentCube;
    }
    private void Update()
    {
        RayCastDown();

        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(mouseRay, out mouseHit))
            {
                if (mouseHit.transform.GetComponent<WalkBlock>() != null)
                {
                    Init();
                    clickedCube = mouseHit.transform;
                    currentCube.GetComponent<WalkBlock>().isVisited = true;
                    Checking(currentCube);
                    isMoving = true;
                }
            }
        }
        if (isMoving)
        {
            StartCoroutine(Move());
        }
    }
    private void Init()
    {
        k = 0;
        for (int i = 0; i < path.Count; i++)
        {
            path[i].GetComponent<WalkBlock>().isVisited = false;
        }
        startCube.GetComponent<WalkBlock>().isVisited = false;
        path.Clear();
    }
    private IEnumerator Move()
    {
        while (k < path.Count)
        {
            transform.LookAt(path[k].position + new Vector3(0, 1f, 0));
            transform.position = Vector3.Lerp(transform.position, path[k].position + new Vector3(0, 1f, 0), Time.deltaTime * 0.1f);
            Vector3 myDist = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 targetDist = new Vector3(path[k].position.x, 0, path[k].position.z);
            if (Vector3.Distance(myDist, targetDist) < 0.05f)
            {
                k++;
            }
            yield return null;
        }
        isMoving = false;
        yield return null;

    }
    private void Checking(Transform cube)
    {
        Transform minBlock = null;
        float min = float.MaxValue;
        WalkBlock walkblock = cube.GetComponent<WalkBlock>();
        if(cube == clickedCube)
        {
            return;
        }
        for (int i = 0; i < walkblock.possiblePaths.Count; i++)
        {
            if (walkblock.possiblePaths[i].GetComponent<WalkBlock>().isVisited)
            {
                continue;
            }
            float distance = Vector3.Distance(clickedCube.position, walkblock.possiblePaths[i].transform.position);
            if (min > distance)
            {
                min = distance;
                minBlock = walkblock.possiblePaths[i].transform;
            }
        }
        if (minBlock)
        {
            Debug.Log($"Me = {cube}");
            Debug.Log($"minBlock = {minBlock}");
            minBlock.GetComponent<WalkBlock>().isVisited = true;
            path.Add(minBlock);
            Checking(minBlock);
        }
    }
    public void RayCastDown()
    {
        Ray playerRay = new Ray(transform.position, -transform.up);
        RaycastHit playerHit;
        if (Physics.Raycast(playerRay, out playerHit))
        {
            if (playerHit.transform.GetComponent<WalkBlock>() != null)
            {
                currentCube = playerHit.transform;
            }
        }
    }
}
