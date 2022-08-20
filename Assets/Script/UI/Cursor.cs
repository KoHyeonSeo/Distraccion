using UnityEngine;


public class Cursor : MonoBehaviour
{
    //[SerializeField] private float offsetDistance = 1f;
    //private Camera cam;
    private Animator anim;
    private PlayerMove player;
    private RectTransform rect;


    private void Start()
    {
        player = GameManager.Instance.playerGameobject.GetComponent<PlayerMove>();
        rect = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        print("0000000");
        print($"1 : { player.isCheck}");
        if (player.isCheck)
        {
            print("111111");
            rect.anchoredPosition = Input.mousePosition;
            print("22222");
            anim.SetTrigger("Click");
            print("333333333");
        }
    }
    


//private void Start()
//    {
//        if (cam == null)
//        {
//            cam = Camera.main;
//        }
//        animController = GetComponent<Animator>();
//        player = GameManager.Instance.playerGameobject.GetComponent<PlayerMove>();
//    }

//    // 카메라 항상 바라보게 하기
//    void LateUpdate()
//    {
//        if (cam != null)
//        {
//            Vector3 cameraForward = cam.transform.rotation * Vector3.forward;
//            Vector3 cameraUp = cam.transform.rotation * Vector3.up;

//            transform.LookAt(transform.position + cameraForward, cameraUp);
//        }
//    }

//    // 클릭한 곳 커서 나타내기
//    public void ShowCursor(Vector3 position)
//    {
//        if (cam != null && animController != null)
//        {
//            Vector3 cameraForwardOffset = cam.transform.rotation * new Vector3(0f, 0f, offsetDistance);
//            transform.position = position - cameraForwardOffset;
//            if (player.isCheck)
//            {
//                animController.SetTrigger("ClickTrigger");
//            }
//        }
//    }
}

