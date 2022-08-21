using UnityEngine;
using UnityEngine.UI;


public class Cursor : MonoBehaviour
{
    //[SerializeField] private float offsetDistance = 1f;
    //private Camera cam;
    private Animator anim;
    private PlayerInput player;
    private RectTransform rect;

    private void Start()
    {
        player = GameManager.Instance.playerGameobject.GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        rect = GetComponent<RectTransform>();
    }

    public void CursorClick()
    {
        rect.transform.position = player.MousePosition;
        anim.SetTrigger("Click");
        print("Click");
    }
}

