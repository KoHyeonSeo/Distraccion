using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Walkable : MonoBehaviour
{
    public List<GamePath> possiblePaths = new List<GamePath>();

    [Space]

    public Transform previousBlock;

    [Space]

    [Header("Booleans")]
    public bool isStair = false;


    [Space]

    [Header("Offsets")]
    public float WalkPointOffset = 0.5f;
    public float stairOffset = 0.4f;
    public GamePath path;
    public GamePath MyPath { get { return path; } set { path = value; } }
    private void Start()
    {
        path.target = transform;
        path.MyVisited = false;
        MyPath = path;
    }
    public void Call()
    {
        Debug.Log($"Me: {this.gameObject} Visited: {MyPath.MyVisited}");
    }
    public Vector3 GetWalkPoint()
    {
        float stair = isStair ? stairOffset : 0;
        return transform.position + transform.up * WalkPointOffset - transform.up * stair;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        float stair = isStair ? .4f : 0;
        Gizmos.DrawSphere(GetWalkPoint(), 0.1f);
    }

    [Serializable]
    public class GamePath
    {
        public Transform target;
        public bool MyVisited;
    }
}
