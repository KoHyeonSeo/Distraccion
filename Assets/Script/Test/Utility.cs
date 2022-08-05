using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public GameObject block1;
    public GameObject block2;
    private float minX;
    private float maxX;
    private float maxY;
    private void Start()
    {
        
        Bounds bounds = block1.GetComponent<BoxCollider>().bounds;
        Bounds bounds2 = block2.GetComponent<BoxCollider>().bounds;
        float dist = Vector2.Distance(new Vector2(bounds.min.x, bounds.max.y), new Vector2(bounds2.max.x, bounds2.max.y));
        Debug.Log($"dist = {dist}");
        Debug.Log($"´ä: {Mathf.Acos(dist / 7)}");
    }
}
