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
    /// ���콺 Ŀ���� x��ǥ�� ��ȭ�� ��ȯ
    /// </summary>
    public float XMouseOut { get; private set; }

    /// <summary>
    /// ���콺 Ŀ���� y��ǥ�� ��ȭ�� ��ȯ
    /// </summary>
    public float YMouseOut { get; private set; }

    /// <summary>
    /// ���콺 ��ġ�� ��ȯ
    /// </summary>
    public Vector3 MousePosition { get; private set; }

    /// <summary>
    /// EscŰ�� ������ true ��ȯ
    /// </summary>
    public bool EscButton { get; private set; }

    /// <summary>
    /// left alt ������ true ��ȯ
    /// </summary>
    public bool ExplainButton { get; private set; }

    /// <summary>
    /// ������ �� ����ϴ� Ű, ��Ŭ�� ���� �� true ��ȯ
    /// </summary>
    public bool MoveKey { get; private set; }

    /// <summary>
    /// ���� ��ȣ�ۿ��ϴ� Ű, ��Ŭ�� ������ ���� �� true ��ȯ
    /// </summary>
    public bool InteractKey { get; private set; }

    /// <summary>
    /// �������� ����ϴ� Ű. �����̽� �ٸ� ���� �� true ��ȯ
    /// </summary>
    public bool UseItemButton { get; private set; }
    /// <summary>
    /// ���콺 �����Ͱ� ����Ű�� ����� ������ ��ȯ
    /// </summary>
    public GameObject PointBlock { get; private set; }

    /// <summary>
    /// ���콺�� ������ ��ȯ
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
