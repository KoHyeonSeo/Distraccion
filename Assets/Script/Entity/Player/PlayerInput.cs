using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float mouseMaxDistance = 950f;


    public const string XMouseName = "Mouse X";
    public const string YMouseName = "Mouse Y";
    public const string CancelName = "Cancel";
    public const string ExplainName = "Explain";
    public const string MoveName = "Move";
    public const string InteractName = "Fire1";
    public const string UseItemName = "UseItem";

    private RaycastHit hit;
    
    /// <summary>
    /// 마우스 커서의 x좌표의 변화값 반환
    /// </summary>
    public float XMouseOut { get; private set; }

    /// <summary>
    /// 마우스 커서의 y좌표의 변화값 반환
    /// </summary>
    public float YMouseOut { get; private set; }

    /// <summary>
    /// 마우스 위치를 반환
    /// </summary>
    public Vector3 MousePosition { get; private set; }

    /// <summary>
    /// Esc키를 누르면 true 반환
    /// </summary>
    public bool EscButton { get; private set; }

    /// <summary>
    /// left alt 누르면 true 반환
    /// </summary>
    public bool ExplainButton { get; private set; }

    /// <summary>
    /// 움직일 때 사용하는 키, 우클릭 누를 시 true 반환
    /// </summary>
    public bool MoveKey { get; private set; }

    /// <summary>
    /// 블럭과 상호작용하는 키, 좌클릭 누르고 있을 때 true 반환
    /// </summary>
    public bool InteractKey { get; private set; }

    /// <summary>
    /// 아이템을 사용하는 키. 스페이스 바를 누를 시 true 반환
    /// </summary>
    public bool UseItemButton { get; private set; }
    /// <summary>
    /// 마우스 포인터가 가리키는 블록의 정보를 반환
    /// </summary>
    public GameObject PointBlock { get; private set; }

    /// <summary>
    /// 마우스의 방향을 반환
    /// </summary>
    public Vector3 MouseDir { get; private set; }

    void Update()
    {
        XMouseOut = Input.GetAxis(XMouseName);
        YMouseOut = Input.GetAxis(YMouseName);
        MousePosition = Input.mousePosition;
        ExplainButton = Input.GetButton(ExplainName);
        EscButton = Input.GetButtonDown(CancelName);
        MoveKey = Input.GetButtonDown(MoveName);
        InteractKey = Input.GetButton(InteractName);
        UseItemButton = Input.GetButtonDown(UseItemName);
        if(UseItemButton)
            Debug.Log("UseItemButton true");

        Vector3 mousepos = MousePosition;
        mousepos.z = mouseMaxDistance;
        Vector3 v = Camera.main.ScreenToWorldPoint(mousepos);
        Vector3 screen = Camera.main.ScreenToWorldPoint(MousePosition);
        Vector3 screenMid = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));
        Vector3 mouseDir = (screen - screenMid).normalized;
        mouseDir.x = 0;
        MouseDir = mouseDir;
        Vector3 dir = v - screen;
        //Debug.Log($"MouseDir = {MouseDir}");
        Debug.DrawRay(screen, dir.normalized * mouseMaxDistance, Color.red);
        LayerMask layer = (1 << LayerMask.NameToLayer("Top")) + (1<<LayerMask.NameToLayer("Item"));
        if (Physics.Raycast(screen, dir.normalized * mouseMaxDistance, out hit, mouseMaxDistance, ~layer))
        {
            //Debug.Log($"hit = {hit.collider.gameObject}");
            PointBlock = hit.collider.gameObject;
        }
        else
        {
            PointBlock = null;
        }

    }
}
