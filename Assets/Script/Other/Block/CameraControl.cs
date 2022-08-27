using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    //public float minZoom = 3;
    //public float maxZoom = 11;
    //public float zoomSpeed = 3;
    //public float zoomLerpSpeed = 20;
    //float scroll;
    //float zoom;

    public static CameraControl Instance;
    PlayerMove player;
    Scene scene;
    bool upOnce = false;


    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        player = GameManager.Instance.playerGameobject.GetComponent<PlayerMove>();

        if (scene.name == "Stage3")
        {
            //transform.position = new Vector3(12.9f, 24.7f, -18);
        }
    }

    private void Update()
    {
        if (!player) 
        {
            player = GameManager.Instance.playerGameobject.GetComponent<PlayerMove>();
        }
    }


    void LateUpdate()
    {
        if (!player)
        {
            player = GameManager.Instance.playerGameobject.GetComponent<PlayerMove>();
        }
        // Stage3에서 플레이어가 특정 지점에 도착하면 Camera y position 조절
        if (player.currentNode.name == "CameraUp" && scene.name == "Stage3" /*&& !upOnce*/)
        {
            StartCoroutine("CamUp");
        }


        //Zoom();
    }

    //private void Zoom()
    //{
    //    scroll = Input.GetAxis("Mouse ScrollWheel");
    //    if (scroll != 0)
    //    {
    //        zoom -= scroll * zoomSpeed;
    //        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
    //        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoom, Time.deltaTime * zoomLerpSpeed);
    //    }
    //}

    float shakeTime;
    float shakeIntensity;
    public void OnShakeCamera(float shakeTime = 1.0f, float shakeIntensity = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByRotation");
        StartCoroutine("ShakeByRotation");
    }

    IEnumerator ShakeByRotation()
    {

        Vector3 startRotation = transform.eulerAngles;
        //Vector3 startPosition = transform.position;

        float power = 10f;
        while (shakeTime > 0.0f)
        {
            float x = 0;
            float y = UnityEngine.Random.Range(-1f, 1f);
            float z = 0;
            transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);
            //transform.position += new Vector3(x, y, z) * shakeIntensity * power;

            shakeTime -= Time.deltaTime;

            yield return null;
        }
        //transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);
        transform.rotation = Quaternion.Euler(startRotation.x, startRotation.y, startRotation.z);
    }

    public float upSpeed = 2;
    IEnumerator CamUp()
    {
        float y = transform.position.y;

        while (y < 39.4f)
        {
            y += Time.deltaTime * upSpeed;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            yield return null;
            //transform.position = Vector3.Lerp(transform.position, upPos, Time.deltaTime);
        }
        transform.position = new Vector3(transform.position.x, 39.4f, transform.position.z);
    }
}