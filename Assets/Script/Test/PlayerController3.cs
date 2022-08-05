using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    public Transform currentCube;
    public Transform clickedCube;
    public List<Transform> path = new List<Transform>();
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
                if (mouseHit.transform.GetComponent<WalkBlock>() != null)
                {
                }
            }
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
