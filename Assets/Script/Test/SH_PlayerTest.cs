using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SH_PlayerTest : MonoBehaviour
{
    public Node startNode;
    public Node currNode;  // openNode�� ���Ե� ��� ��  fCost�� ���� ���� ���
    public Node targetNode;

    public Transform currentNode;
    public Transform checkNode;  // Player Layer 'Top'���� �����ϴ� ���

    [Serializable]
    public struct trickNode
    {
        public Node trick1;
        public Node trick2;
    }

    public List<trickNode> trick = new List<trickNode>();
    public List<Node> openNode = new List<Node>();  // ���� ���ϱ� ���� ��� ����Ʈ
    public List<Node> closeNode = new List<Node>();  // ���� ������ ��� ����Ʈ
    public List<Node> findPath = new List<Node>();
    public List<Vector3> findPathPos = new List<Vector3>();

    private PlayerInput playerInput;
    private bool isCheck = false;
    private bool noWay = false;

    Scene scene;
    SH_MovingGround archBezier;
    public TwistBlock twist;
    Animator anim;
    public Cursor cursor;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        anim = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();
        if (scene.name == "Stage2")
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        if (scene.name == "(Legacy)Stage3")
        {
            archBezier = GetComponent<SH_MovingGround>();
            archBezier.enabled = false;
        }
    }

    void Update()
    {
        // Player�� �� �ִ� ���(�ǽð�) = currentNode
        RayCastDown();


        // ����� Ŭ��(Fire2)
        if (playerInput.MoveKey)
        {
           //  ��� ������ ���� (1��)
            if (!isCheck && playerInput.PointBlock && playerInput.PointBlock.layer == LayerMask.NameToLayer("Node"))
            {
                // ��� ���� üũ ����
                isCheck = true;
                cursor.CursorClick();

                // �ʱ�ȭ
                for (int i = 0; i < openNode.Count; i++)
                {
                    openNode[i].parent = null;
                }
                for (int i = 0; i < closeNode.Count; i++)
                {
                    closeNode[i].parent = null;
                }
                openNode.Clear();
                closeNode.Clear();
                findPath.Clear();
                findPathPos.Clear();
            }

            // ���õǾ��ٸ�
            if (isCheck)
            {
                #region ���� �ʱ�ȭ ��ġ
                //print("2222222222");
                //// ��帮��Ʈ �θ�
                //for (int i = 0; i < openNode.Count; i++)
                //{
                //    openNode[i].parent = null;
                //}
                //for (int i = 0; i < closeNode.Count; i++)
                //{
                //    closeNode[i].parent = null;
                //}
                //// ���� ����Ʈ,���� ����
                //openNode.Clear();
                //closeNode.Clear();
                //findPath.Clear();
                //findPathPos.Clear();
                #endregion

                // Move �ε���, �ӵ�
                idx = 0;
                ratio = 0;

                // ��߳�� = Player �Ʒ� ���
                Ray ray = new Ray(transform.position, -transform.up);
                LayerMask layer = 1 << LayerMask.NameToLayer("Node");
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1, layer))
                {
                    startNode = hit.transform.GetComponent<Node>();
                }

                // Ÿ�� ��� = ������ ���
                if (playerInput.PointBlock.layer == LayerMask.NameToLayer("Node"))
                {
                    targetNode = playerInput.PointBlock.GetComponent<Node>();
                    // ��� ��� openNode�� �־��ֱ�
                    openNode.Add(startNode);  
                }

                #region Regarcy
                //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //if (Physics.Raycast(ray, out hit))
                //{
                //    if (hit.transform.gameObject.tag == "Node")
                //    {
                //        targetNode = hit.transform.GetComponent<Node>();
                //        //targetNode.walkAble = true;
                //        openNode.Add(startNode);d
                //    }
                //}
                #endregion

                // ��ã��
                FindPath();
                // Trick ���� ���� ���� ����
                isVisited = false;
                // ��� ���� üũ ����
                isCheck = false;
            }
        }
        // PlayerMove
        SimpleMove();
        // �����̴� ��� �� Player Hierarchy ��ġ �̵�
        OnMovingBlock();
        noWay = false;
    }


    int idx = 0;  // parent ����Ʈ ��ȯ �ε���
    float ratio = 0;  // Lerp ����
    public bool matChange;
    public Material mat;
    public Material mat_button;
    Vector3 playerDir;
    public float playerMoveSpeed = 3;
    Vector3 target; //  Twist ��� ������
    void SimpleMove()
    {
        if (isCheck)
        {
            anim.SetTrigger("Move");
        }
        
        if (findPath.Count - 1 > idx)
        {
            ratio += 2 * Time.deltaTime;
            playerDir = findPathPos[idx + 1] - findPathPos[idx];

            // Twist ��� ������
            target = findPathPos[idx + 1] + findPath[idx + 1].transform.right;

            // �̵�
            //1. ������ ���� twist�� ���� �ִ� ���� twist�� ���
            if (findPath[idx].CompareTag("Twist") && findPath[idx + 1].CompareTag("Twist"))
            {
                transform.position = findPathPos[idx + 1] + findPath[idx + 1].transform.right;
                //Debug.Log($"==========================================");
                //Debug.Log($"1. ������ ��: {findPathPos[idx + 1] + findPath[idx + 1].transform.forward}");
                //Debug.Log($"1. ������ ��ü ��ġ: {findPathPos[idx + 1]}");
                //Debug.Log($"1. ������ ��ü: {findPath[idx + 1]}");
                //Debug.Log($"==========================================");

                ratio = 0;
                idx++;
            }
            //2. ������ ���� twist�� �ƴϰ�, ���� �ִ� ���� twist�� ���
            else if (findPath[idx].CompareTag("Twist") && !findPath[idx + 1].CompareTag("Twist"))
            {
                transform.position = findPathPos[idx + 1];
                //Debug.Log($"==========================================");
                //Debug.Log($"2. ������ ��: {findPathPos[idx + 1]}");
                //Debug.Log($"2. ������ ��ü ��ġ: {findPathPos[idx + 1]}");
                //Debug.Log($"2��. ������ ��ü: {findPath[idx + 1]}");
                //Debug.Log($"==========================================");
                if (ratio >= 1)
                {
                    idx++;
                    ratio = 0;
                }
            }
            //3. ������ ���� twist�� ���� �ִ� ���� twist�� �ƴ� ���
            else if (!findPath[idx].CompareTag("Twist") && findPath[idx + 1].CompareTag("Twist"))
            {
                transform.position = findPathPos[idx + 1] + findPath[idx + 1].transform.right;
                //Debug.Log($"==========================================");
                //Debug.Log($"3. ������ ��: {findPathPos[idx + 1] + findPath[idx + 1].transform.forward}");
                //Debug.Log($"3. ������ ��ü ��ġ: {findPathPos[idx + 1]}");
                //Debug.Log($"3. ������ ��ü: {findPath[idx + 1]}");
                //Debug.Log($"==========================================");
                ratio = 0;
                idx++;
            }
            // ������ ���� arch�� ��� 
            else if (findPath[idx+1].CompareTag("Arch"))
            {
                transform.position = findPathPos[idx] + new Vector3(-0.6f, 0, 0);
                archBezier.enabled = true;

                ratio = 0;
                idx += 2;
            }
            //4. ������ ��, ���� �ִ� �� ��� twist�� �ƴ� ���
            else
            {
                transform.position = Vector3.Lerp(findPathPos[idx], findPathPos[idx + 1], ratio);
                if (Vector3.Distance(transform.position, findPathPos[idx + 1]) < 0.01f)
                {
                    transform.position = findPathPos[idx + 1];
                }
                if (ratio >= 1)
                {
                    idx++;
                    ratio = 0;
                }
            }

            // ȸ�� : ���� �ִ� ���� ����, Trick�� �ƴ� ���
            if (findPath[idx].gameObject.layer == LayerMask.NameToLayer("Node") && !findPath[idx].gameObject.name.Contains("trick"))
            {
                // Twist ������ player back ���� ����
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -transform.forward, out hit, 1) && hit.collider.CompareTag("Twist"))
                {
                    Debug.DrawRay(hit.point, hit.normal, Color.green, 200);
                    transform.position = hit.transform.position + hit.normal;// + hit.normal * 0.1f;
                    //transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal
                    Vector3 n = target;
                    //Debug.Log(playerDir.normalized.y);
                    n.z = 0;
                    if (playerDir.normalized.y > -0.1)
                        n.x *= -1;
                    else
                    {
                        n.y *= -1;
                    }
                    transform.forward = n;
                }
                // Twist ������ player foward ���� ����
                else if (Physics.Raycast(transform.position, transform.forward, out hit, 1) && hit.collider.CompareTag("Twist"))
                {
                    //hitCount++;
                    Debug.DrawRay(hit.point, hit.normal, Color.green, 200);
                    transform.position = hit.transform.position + hit.normal;// + hit.normal * 0.1f;
                    //transform.position = hit.transform.position + hit.transform.forward;// + hit.normal * 0.1f;
                    //transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal;
                    Vector3 n = target;
                    //Debug.Log(playerDir.normalized.y);
                    n.z = 0;
                    if (playerDir.normalized.y > -0.1)
                        n.x *= -1;
                    else
                    {
                        n.y *= -1;
                    }
                    transform.forward = n;
                }
                // Twist ������ player down ���� ����
                else if (Physics.Raycast(transform.position, -transform.up, out hit, 1) && hit.collider.CompareTag("Twist"))
                {
                    Debug.DrawRay(hit.point, hit.normal, Color.green, 200);
                    transform.position = hit.transform.position + hit.normal;// + hit.normal * 0.1f;
                    //transform.position = hit.transform.position + hit.transform.forward;// + hit.normal * 0.1f;
                    //transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal;
                    Vector3 n = target;
                    //Debug.Log(playerDir.normalized.y);
                    if (playerDir.normalized.y > -0.1)
                        n.x *= -1;
                    else
                    {
                        n.y *= -1;
                    }
                    n.z = 0;
                    transform.forward = n;
                }
                // Twist ������ player left ���� ����
                else if (Physics.Raycast(transform.position, -transform.right, out hit, 1) && hit.collider.CompareTag("Twist"))
                {
                    //hitCount++;
                    Debug.DrawRay(hit.point, hit.normal, Color.green, 200);
                    transform.position = hit.transform.position + hit.normal;// + hit.normal * 0.1f;
                    //transform.position = hit.transform.position + hit.transform.forward;// + hit.normal * 0.1f;
                    //transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal;
                    Vector3 n = target;
                    //Debug.Log(playerDir.normalized.y);
                    n.z = 0;
                    if (playerDir.normalized.y > -0.1)
                        n.x *= -1;
                    else
                    {
                        n.y *= -1;
                    }
                    transform.forward = n;
                }
                // Twist ������ player right ���� ����
                else if (Physics.Raycast(transform.position, transform.right, out hit, 1) && hit.collider.CompareTag("Twist"))
                {
                    //hitCount++;
                    Debug.DrawRay(hit.point, hit.normal, Color.green, 200);
                    transform.position = hit.transform.position + hit.normal;// + hit.normal * 0.1f;
                    //transform.position = hit.transform.position + hit.transform.forward;// + hit.normal * 0.1f;
                    //transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal;
                    //Vector3 normal = target;
                    //if (normal != Vector3.zero)
                    //    transform.LookAt(normal);
                    //Debug.Log(normal);
                    Vector3 n = target;
                    //Debug.Log(playerDir.normalized.y);
                    n.z = 0;
                    if (playerDir.normalized.y > -0.5)
                        n.x *= -1;
                    else
                    {
                        n.y *= -1;
                    }
                    transform.forward = n;
                }
                // arch�� ���
                else if (Physics.Raycast(transform.position, transform.right, out hit, 1) && hit.collider.CompareTag("Arch"))
                {
                    Debug.DrawRay(hit.point, hit.normal, Color.cyan, 200);
                    transform.up = hit.normal;
                }
                // twist Block�� �ƴѰ��
                else
                {
                    transform.position = hit.transform.position + hit.normal;
                    playerDir.y = 0;
                    if (playerDir != Vector3.zero)
                        transform.forward = playerDir;
                }
            }
        }
        // �̵��� ��� �� �ʱ�ȭ
        if (matChange)
        {
            for (int i = 0; i < findPath.Count; i++)
            {
                if (findPath[idx].name.Contains("Button"))
                {
                    findPath[idx].GetComponent<MeshRenderer>().material = mat_button;
                }
                else
                {
                    findPath[idx].GetComponent<MeshRenderer>().material = mat;
                }
            }
        }
    }

    Vector3 twistForward;  // Twist ��� forward
    void SimpleMove2()
    {
        if (isCheck)
        {
            anim.SetTrigger("Move");
        }

        if (findPath.Count - 1 > idx)
        {
            ratio += 2 * Time.deltaTime;
            playerDir = findPathPos[idx + 1] - findPathPos[idx];

            // �̵�
            // ������ ���� ��ġ�� ���
            if (findPath[idx + 1].CompareTag("Arch"))
            {
                transform.position = findPathPos[idx] + new Vector3(-0.6f, 0, 0);
                archBezier.enabled = true;
                // twistForward�� ����
                findPath[idx + 1].transform.up = twistForward;

                ratio = 0;
                idx += 2;
            }
            else
            {
                transform.position = Vector3.Lerp(findPathPos[idx], findPathPos[idx + 1], ratio);
                if (Vector3.Distance(transform.position, findPathPos[idx + 1]) < 0.01f)
                {
                    transform.position = findPathPos[idx + 1];
                }
                if (ratio >= 1)
                {
                    idx++;
                    ratio = 0;
                }
            }


            // ȸ�� : ���� �ִ� ���� ����, Trick�� �ƴ� ���
            if (findPath[idx].gameObject.layer == LayerMask.NameToLayer("Node") && !findPath[idx].gameObject.name.Contains("trick"))
            {
                RaycastHit hit;
                // arch�� ���
                if (Physics.Raycast(transform.position, transform.right, out hit, 1) && hit.collider.CompareTag("Arch"))
                {
                }
                else if (Physics.Raycast(transform.position, transform.right, out hit, 1) && hit.collider.CompareTag("Twist"))
                {
                    transform.forward = twistForward;
                }
                else
                {
                    transform.position = hit.transform.position + hit.normal;
                    playerDir.y = 0;
                    if (playerDir != Vector3.zero)
                        transform.forward = playerDir;
                }

                // Twist�� ��� : ��ġ���� ������ forward�� �÷��̾��� forward�� ����
            }
        }
    }
    void SimpleMove_Stage3()
        {
            if (findPath.Count - 1 > idx)
            {
                //if (archBezier.enabled == true)
                //{
                //    return;
                //}
                //else
                if (findPath[idx].CompareTag("Twist") && findPath[idx + 1].CompareTag("Twist"))
                {
                    //if (archBezier.enabled) print("11111");
                    transform.position = findPathPos[idx + 1] + new Vector3(0, 0.5f, 0);
                    idx++;
                }
                else
                {
                    ////if (archBezier.enabled) print("22222222");
                    //// Lerp �̵�
                    //if (findPath[idx + 1].CompareTag("Arch"))
                    //{
                    //    //archBezier.enabled = true;
                    //    findPath.RemoveAt(idx + 1);
                    //}
                    //else
                    //{
                    ratio += playerMoveSpeed * Time.deltaTime;
                    transform.position = Vector3.Lerp(findPathPos[idx], findPathPos[idx + 1], ratio);
                    if (ratio >= 1)
                    {
                        idx++;
                        ratio = 0;
                    }
                    //}
                }

                // ȸ�� && !archBezier.enabled
                if (findPath[idx].gameObject.layer == LayerMask.NameToLayer("Node") && !findPath[idx].gameObject.name.Contains("trick"))
                {
                    //if (archBezier.enabled) print("3333333333");
                    playerDir = (idx == 0) ? findPathPos[idx + 1] - findPathPos[idx] : findPathPos[idx] - findPathPos[idx - 1];
                    playerDir.y = 0;
                    transform.forward = playerDir;

                    //archBezier.enabled = false;

                    // Arch Bezier �̵�
                    //RaycastHit hit;
                    //if (Physics.Raycast(transform.position, -transform.up, out hit, 1) && hit.collider.CompareTag("Arch"))
                }

                // Twist �� �� ���
                //if(!twist.isTwist)
                //{
                //    transform.position += transform.right;

                //}
                //else
                //{

                //}
            }
            #region ���� �ڵ�
            //// ã�� �� �ε����� ���� ��ȸ
            //if (findPath.Count - 2 > idx)
            //{
            //   // Twist �̵� ���
            //    if (findPath[idx].CompareTag("Twist") && findPath[idx +1].CompareTag("Twist"))
            //    {
            //        //if (idx >= findPath.Count - 2)
            //        //    goto CONTINUE;
            //        transform.position = findPathPos[idx + 1];
            //        idx++;
            //    }
            //    // Node �̵� ���
            //    else
            //    {
            //        ratio += 3 * Time.deltaTime;
            //        transform.position = Vector3.Lerp(findPathPos[idx], findPathPos[idx + 1], ratio);
            //        if (ratio >= 1)
            //        {
            //            idx++;
            //            ratio = 0;
            //        }
            //    }
            //    // ȸ��(trick��� ����)
            //    if (findPath[idx].gameObject.layer == LayerMask.NameToLayer("Node") && !findPath[idx].gameObject.name.Contains("trick"))
            //    {
            //        playerDir = (idx == 0) ? findPathPos[idx + 1] - findPathPos[idx] : findPathPos[idx] - findPathPos[idx - 1];
            //        playerDir.y = 0;
            //        transform.forward = playerDir;  // �÷��̾��� �չ��� : ���� ã�� ��� ��ġ -> ���� ã�� ��� ��ġ

            //        // �÷��̾� �Ʒ��������� ray����� �� Twist / Arch ����� ���
            //        RaycastHit hit;
            //        if (Physics.Raycast(transform.position, -transform.up, out hit, 5) && hit.collider.CompareTag("Arch"))
            //        {

            //            //Debug.DrawRay(hit.point, hit.normal, Color.green, 200);
            //            //transform.position = hit.transform.position + transform.up;
            //            //transform.up = hit.normal;
            //            //StartCoroutine(Arch());
            //        }
            //        //if (Physics.Raycast(transform.position, -transform.up, out hit, 1) && hit.collider.CompareTag("Twist"))
            //        //{
            //        //    Debug.DrawRay(hit.point, hit.normal, Color.green, 200);
            //        //    transform.position = hit.transform.position + hit.transform.forward;
            //        //    transform.up = hit.transform.forward;
            //        //}
            //    }
            //}
            /* else
             {

                 transform.position = findPathPos[idx] + findPath[idx].transform.forward;
                 transform.up = findPath[idx].transform.forward;
             }*/

            //CONTINUE:
            #endregion

            // �̵��� ��� �� �ʱ�ȭ
            if (matChange)
            {
                for (int i = 0; i < findPath.Count; i++)
                {
                    if (findPath[idx].name.Contains("Button"))
                    {
                        findPath[idx].GetComponent<MeshRenderer>().material = mat_button;
                    }
                    else
                    {
                        findPath[idx].GetComponent<MeshRenderer>().material = mat;
                    }
                }
            }
        }



    // ��ã��
    void FindPath()
    {
        //print($"1: {openNode.Count}, {openNode[0]}");  // ��߳��

        // 1. �߽ɳ�� & ������� ã��
        FindNear();

        //print($"2 : {openNode.Count}, {openNode[0]}");//,{openNode[1]}");  // ��߳�� + �̿����

        // 2. currNode(��� �ּ�) => ClosedList
        openNode.Remove(currNode);
        closeNode.Add(currNode);
        //print($"3 : {openNode.Count}");//, {openNode[0]}");  // �̿���� �� fCost�� ���� ���� ��� (��߳�� => close)


        // 3-1. ���� ���� ���
        if (openNode.Count <= 0)
        {
            noWay = true;
        }
        // 3-2. ���� �ִ� ���
        else
            {
                // 1. ��ã�� Loop ( �� �� �ִ� ���� �ְ� targetNode�� ã�� ������)
                if (openNode.Count > 0 && openNode[0] != targetNode)
                {
                    FindPath();
                }
                // 2. targetNode�� ã�Ҵٸ� path �����
                else if (openNode[0] == targetNode)
                {
                    findPath.Clear();
                    // Ÿ�ٳ����� �Ųٷ� ã��
                    Node Node = targetNode;
                    // �θ��尡 ���� startNode���� �Ž��� �ö󰡸鼭 path �����
                    while (Node.parent != null)
                    {
                        if (matChange)
                        {
                            // ������ ��� �� �Ķ������� ǥ��
                            Node.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                        }

                        // �Ųٷ� ��� �ֱ�
                        findPath.Insert(0, Node);
                        // ��Ȳ�� ���� �÷��̾ �̵��ؾ��� position �ٸ��� �Է�
                        if (Node.gameObject.name.Contains("Stair"))
                        {
                            findPathPos.Insert(0, Node.transform.position + new Vector3(0.2f, 0.7f, 0));
                        }
                        else if (Node.gameObject.name.Contains("Button"))
                        {
                            findPathPos.Insert(0, Node.transform.position + new Vector3(0, 0.3f, 0));
                        }
                        else if (Node.gameObject.CompareTag("Twist"))
                        {
                            findPathPos.Insert(0, Node.transform.position);
                        }
                        else
                        {
                            findPathPos.Insert(0, Node.transform.position + Vector3.up);
                        }
                        // �θ� ��� �Ž��� �ö󰡱�
                        Node = Node.parent;
                    }
                    findPath.Insert(0, startNode);
                    findPathPos.Insert(0, transform.position);
                }
            }
    }


    // Trick ���� ���� ���� ����
    bool isVisited = false;
    // �̿� ��� ã�� openNode ����
    void FindNear()
    {
        currNode = openNode[0];  // ����� ���� ���� ���

        // ��
        AddNearOpen(transform.forward);
        // ��
        AddNearOpen(-transform.forward);
        // ������
        AddNearOpen(transform.right);
        // ����
        AddNearOpen(-transform.right);
        // ���� 
        AddNearOpen(Vector3.up + transform.forward);
        // ����
        AddNearOpen(Vector3.up - transform.forward);
        // ����
        AddNearOpen(Vector3.up - transform.right);
        // ����
        AddNearOpen(Vector3.up + transform.right);
        // �Ʒ���
        AddNearOpen(Vector3.down + transform.forward);
        // �Ʒ���
        AddNearOpen(Vector3.down - transform.forward);
        // �Ʒ���
        AddNearOpen(Vector3.down - transform.right);
        // �Ʒ���
        AddNearOpen(Vector3.down + transform.forward);
        // Stage3 ��ġ��� �߰� �˻� 
        if (scene.name == "(Legacy)Stage3")
            {
                AddNearOpen(transform.right + new Vector3(0, 0.5f, 0));
                AddNearOpen(-transform.right + new Vector3(0, 0.5f, 0));
            }

        // trick�κ� ����
        if (isVisited == false && currNode.gameObject.CompareTag("Trick"))
        {
            for (int i = 0; i < trick.Count; i++)
            {
                if (trick[i].trick1.name == currNode.gameObject.name)
                {
                    Node next = trick[i].trick2;
                    next.parent = currNode;
                    openNode.Add(next);
                    isVisited = true;
                    break;
                }
                else if (trick[i].trick2.name == currNode.gameObject.name)
                {
                    Node next = trick[i].trick1;
                    next.parent = currNode;
                    openNode.Add(next);
                    isVisited = true;
                    break;
                }
            }
        }
        // openNode ���� 
        openNode.Sort(SortByfCost);
    }
    // �̿� ��� ã�� ray ����
    public float rayLength = 1;
    // currNode�� �̿���� ã��
    void AddNearOpen(Vector3 dir)
    {
        Ray ray = new Ray(currNode.transform.position, dir);
        RaycastHit hit;
        //Debug.DrawRay(currNode.transform.position, dir, Color.blue, 200, false);  // ���� ��� ã�� ��
        int layer = 1 << LayerMask.NameToLayer("Node");
        if (Physics.Raycast(ray, out hit, rayLength, layer))
        {
            Debug.DrawLine(currNode.transform.position, hit.point, Color.red, 30, false);  // �浹�� ���������� Ray�� 
            Node Node = hit.transform.GetComponent<Node>();
            Node.SetCost(startNode.transform.position, targetNode.transform.position);
            if (!openNode.Contains(Node) && !closeNode.Contains(Node))
            {
                Node.parent = currNode;
                openNode.Add(Node);
            }
        }
    }

    // currentNode  �ѹ��� ã��
    bool isOnce = false;
    void RayCastDown()
    {
        // ���� ����, ������ �Ʒ�
        Ray playerRay = new Ray(transform.position, -transform.up);
        RaycastHit playerHit;

        // ���� �߻�
        if (Physics.Raycast(playerRay, out playerHit))
        {
            // ��带 ��� �ִٸ� 
            if (playerHit.collider.gameObject.layer == LayerMask.NameToLayer("Node") && !isOnce)
            {
                currentNode = playerHit.transform;
                // ���� currentNode�� checkNode�� ��� player�� ���� �ڽĵ� ��� Layer Top���� �������ֱ�
                if (currentNode == checkNode)
                {
                    ChangeLayersRecursively(transform, "Top");
                    isOnce = true;
                }
            }
        }
    }

    // ���� �ڽ� ������Ʈ ���̾� �ѹ��� ����
    public static void ChangeLayersRecursively(Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, name);
        }
    }

    // Moving Block �� Hierarchy ��ġ ����
    void OnMovingBlock()
    {
        // ���� ��� �ִ� ��尡 �����̴� ���
        if (currentNode.tag == "Move")
        {
            // �÷��̾ �� �ڽ����� �ִ´�.
            transform.parent = currentNode.parent;
        }
        else
        {
            transform.parent = null;
        }
    }

    // ��� ���
    int SortByfCost(Node c1, Node c2)
    {
        if (c1.fCost < c2.fCost) return -1;
        if (c1.fCost > c2.fCost) return 1;
        return 0;
    }

    // ������ �浹
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            for (int i = 0; i < GameManager.Instance.ItemProp.Count; i++)
            {
                if (GameManager.Instance.ItemProp[i].Item.name.Contains(other.gameObject.name))
                {
                    GameManager.Instance.SetHaveItem = i;
                    Destroy(other.gameObject);
                    return;
                }
            }
            Destroy(other.gameObject);
        }
    }
}
