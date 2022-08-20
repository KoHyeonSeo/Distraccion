using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerTest : MonoBehaviour
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
    //MovingGround archB;
    public TwistBlock twist;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        playerInput = GetComponent<PlayerInput>();
        if (scene.name == "Stage2")
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        if (scene.name == "Stage3")
        {
            //archB = GetComponent<MovingGround>();
            //archB.enabled = false;
        }
    }

    void Update()
    {
        RayCastDown();
        // ����� ��� Ŭ����(Fire2) �ʱ�ȭ
        if (playerInput.MoveKey)
        {
            print("11111111");
            //  A* �˰������� ��� �̵�
            if (!isCheck && playerInput.PointBlock && playerInput.PointBlock.layer == LayerMask.NameToLayer("Node"))
            {
                isCheck = true;
            }
            if (isCheck)
            {
                print("2222222222");
                // ��帮��Ʈ �θ�
                for (int i = 0; i < openNode.Count; i++)
                {
                    openNode[i].parent = null;
                }
                for (int i = 0; i < closeNode.Count; i++)
                {
                    closeNode[i].parent = null;
                }
                // ���� ����Ʈ,���� ����
                openNode.Clear();
                closeNode.Clear();
                findPath.Clear();
                findPathPos.Clear();
                idx = 0;
                ratio = 0;
                // Raycast�� ����� ��߳�� ã��
                Ray ray = new Ray(transform.position, -transform.up);
                LayerMask layer = 1 << LayerMask.NameToLayer("Node");
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1, layer))
                {
                    startNode = hit.transform.GetComponent<Node>();
                    //startNode.walkAble = true;
                }
                // Raycast�� ����ڰ� �������ϴ� Ÿ�ٳ�� ã��
                if (playerInput.PointBlock.layer == LayerMask.NameToLayer("Node"))
                {
                    targetNode = playerInput.PointBlock.GetComponent<Node>();
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
                //        openNode.Add(startNode);
                //    }
                //}
                #endregion

                // ��ã��
                FindPath();
                isCheck = false;
                isVisited = false;
            }
        }
        // PlayerMove
        if (scene.name == "Stage3")
        {
            SimpleMove_Stage3();
        }
        else
        {
            SimpleMove();
        }
        // �����̴� ��� �� Player Hierarchy ��ġ �̵�
        OnMovingBlock();
        noWay = false;
    }


    int idx = 0;
    float ratio = 0;
    public bool matChange;
    public Material mat;
    public Material mat_button;
    Vector3 playerDir;
    public float playerMoveSpeed = 3;


    void SimpleMove()
    {
        if (findPath.Count - 1 > idx)
        {
            ratio += 3 * Time.deltaTime;
            // ��� �Ÿ��� ������ �ð����� �̵��ϵ��� ����
            transform.position = Vector3.Lerp(findPathPos[idx], findPathPos[idx + 1], ratio);
            Vector3 playerDir = findPathPos[idx + 1] - findPathPos[idx];
            // ������ ��츸 ȸ��
            if (findPath[idx].gameObject.layer == LayerMask.NameToLayer("Node") && !findPath[idx].gameObject.name.Contains("trick"))
            {
                playerDir.y = 0;
                transform.forward = playerDir;
                // Twist ������ player up ���� ����
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -transform.up, out hit, 1) && hit.collider.CompareTag("Twist"))
                {
                    //hitCount++;
                    Debug.DrawRay(hit.point, hit.normal, Color.green, 200);
                    transform.position = hit.transform.position + hit.transform.forward;// + hit.normal * 0.1f;
                    //transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal;
                    transform.up = hit.transform.forward;
                }
                //transform.up = findPath[idx].gameObject;
            }
            if (ratio >= 1)
            {
                idx++;
                ratio = 0;
            }
        }

        //print(hitCount);

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

    void SimpleMove_Stage3()
    {
        if (findPath.Count - 1 > idx)
        {
            //if (archB.enabled == true)
            //{
            //    return;
            //}
            //else
            if (findPath[idx].CompareTag("Twist") && findPath[idx + 1].CompareTag("Twist"))
            {
                //if (archB.enabled) print("11111");
                transform.position = findPathPos[idx + 1] + new Vector3(0, 0.5f, 0);
                idx++;
            }
            else
            {
                ////if (archB.enabled) print("22222222");
                //// Lerp �̵�
                //if (findPath[idx + 1].CompareTag("Arch"))
                //{
                //    //archB.enabled = true;
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

            // ȸ�� && !archB.enabled
            if (findPath[idx].gameObject.layer == LayerMask.NameToLayer("Node") && !findPath[idx].gameObject.name.Contains("trick") )
            {
                //if (archB.enabled) print("3333333333");
                playerDir = (idx == 0) ? findPathPos[idx + 1] - findPathPos[idx] : findPathPos[idx] - findPathPos[idx - 1];
                playerDir.y = 0;
                transform.forward = playerDir;
                //archB.enabled = false;

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
        // �߽ɳ�� & ������� ã��
        FindNear();
        //print($"2 : {openNode.Count}, {openNode[0]}");//,{openNode[1]}");  // ��߳�� + �̿����
        // �߽�Node => ClosedList
        openNode.Remove(currNode);
        closeNode.Add(currNode);
        //print($"3 : {openNode.Count}");//, {openNode[0]}");  // �̿���� �� fCost�� ���� ���� ��� (��߳�� => close)

        // �� �� �ִ� ���� ���� ���
        if (openNode.Count <= 0)
        {
            noWay = true;
        }
        else
        {
            // ��ã�� Loop ( �� �� �ִ� ���� �ְ� targetNode�� ã�� ������)
            if (openNode.Count > 0 && openNode[0] != targetNode)
            {
                FindPath();
            }
            // targetNode�� ã�Ҵٸ� path �����
            else if (openNode[0] == targetNode)
            {
                findPath.Clear();
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
                    // �Ųٷ� ��� ��ġ �ֱ�
                    // ��Ȳ�� ���� �÷��̾ �̵��ؾ��� position �ٸ��� �Է�
                    if (Node.gameObject.name.Contains("Stair"))
                    {
                        findPathPos.Insert(0, Node.transform.position + new Vector3(0.2f, 0.7f, 0));
                    }
                    else if (Node.gameObject.name.Contains("Button"))
                    {
                        findPathPos.Insert(0, Node.transform.position + new Vector3(0, 0.3f, 0));
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


    bool isVisited = false;
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
        // Stage3�� ��� ��ġ����尡 �־� �߰� ��� �˻� ����
        if (scene.name == "Stage3")
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

        // openNode ���� by fCost
        openNode.Sort(SortByfCost);
    }

    public float rayLength = 1;
    // currNode���� ray�̿��� �ش� ���� ������� ã��
    void AddNearOpen(Vector3 dir)
    {
        Ray ray = new Ray(currNode.transform.position, dir);
        RaycastHit hit;
        //Debug.DrawRay(currNode.transform.position, dir, Color.blue, 30, false);
        int layer = 1 << LayerMask.NameToLayer("Node");
        if (Physics.Raycast(ray, out hit, rayLength, layer))
        {
            Debug.DrawLine(currNode.transform.position, hit.point, Color.red, 200, false);  // �浹�� ���������� Ray�� 
            Node Node = hit.transform.GetComponent<Node>();
            Node.SetCost(startNode.transform.position, targetNode.transform.position);
            if (!openNode.Contains(Node) && !closeNode.Contains(Node))
            {
                Node.parent = currNode;
                openNode.Add(Node);
            }
        }
    }

    bool isOnce = false;
    // ���� �÷��̾ ��� �ִ� ��� ã�� �Լ�
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
                // ���� currentNode�� checkNode�� ��� player�� Layer Top���� �������ֱ�
                if (currentNode == checkNode)
                {
                    ChangeLayersRecursively(transform, "Top");
                    isOnce = true;
                }
            }
        }
    }

    // ���� �ڽ� ������Ʈ ���̾� �ѹ��� �����ϴ� �Լ�
    public static void ChangeLayersRecursively(Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, name);
        }
    }

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
