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
        player = GameManager.Instance.playerGameobject.GetComponent<PlayerMove>();
        scene = SceneManager.GetActiveScene();
    }

    void LateUpdate()
    {
        //print($"1 : {player.currentNode.name == "CameraUp"}");
        //print($"2 : {scene.name == "Stage3"}");
        //print($"3 : {upOnce}");

        // Stage3에서 플레이어가 특정 지점에 도착하면 Camera y position 조절
        if (player.currentNode.name == "CameraUp" && scene.name == "Stage3" /*&& !upOnce*/)
        {
            print("UP!!!!");
            StartCoroutine("CamUp");
            //upOnce = true;
            
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

    IEnumerator CamUp()
    {
        Vector3 upPos = new Vector3(transform.position.x, 39.4f, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, upPos, Time.deltaTime*7);

        yield return new WaitForSeconds(1);
    }
}