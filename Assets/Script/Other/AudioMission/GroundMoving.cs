using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMoving : MonoBehaviour
{
    [SerializeField] private float downValue = 1.5f;
    private Vector3 firstPosition;

    IEnumerator Moving()
    {
        Debug.Log("0");
        while (Vector3.Distance(firstPosition + Vector3.down * downValue, transform.position) >0.1f)
        {
            Debug.Log("1 " + Vector3.Distance(firstPosition + Vector3.down * downValue, transform.position));
            transform.position = Vector3.Lerp(firstPosition, firstPosition + Vector3.down * downValue, 0.01f);
            yield return null;
        }

        while (Vector3.Distance(firstPosition, transform.position) > 0.1f)
        {
            Debug.Log("2 " + Vector3.Distance(firstPosition, transform.position));
            transform.position = Vector3.Lerp(firstPosition + Vector3.down * downValue, firstPosition, 0.01f);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            firstPosition = transform.position;
            StartCoroutine(Moving());
        }
    }
}
