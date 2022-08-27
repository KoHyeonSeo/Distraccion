using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMove : MonoBehaviour
{
    public Node startNode;
    public Node currNode;  // openNode에 포함된 노드 중  fCost가 가장 작은 노드
    public Node targetNode;

    public Transform currentNode;
    public Transform checkNode;  // Player Layer 'Top'으로 변경하는 블록
    public Vector3 clickedPos;  // 클릭한 노드 스크린 좌표

    [Serializable]
    public struct trickNode
    {
        public Node trick1;
        public Node trick2;
    }

    public List<trickNode> trick = new List<trickNode>();
    public List<Node> openNode = new List<Node>();  // 값을 정하기 전의 노드 리스트
    public List<Node> closeNode = new List<Node>();  // 값이 정해진 노드 리스트
    public List<Node> findPath = new List<Node>();
    public List<Vector3> findPathPos = new List<Vector3>();

    private PlayerInput playerInput;
    public bool isCheck = false;  // 노드 한 번 선택
    private bool noWay = false;

    Scene scene;
    CameraControl cam;
    public Animator anim;
    public TwistBlock twist;
    public Cursor cursor;
    public List<bool> isTrickVisited;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        anim = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();
        if (scene.name == "Stage2")
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }


    bool onFallingBlock = false;
    void LateUpdate()
    {
        RayCastDown();
        //print(playerInput.PointBlock);
        // 사용자 노드 클릭시(Fire2) 초기화
        if (playerInput.MoveKey)
        {
            isCheck = false;
            // Raycast로 사용자 출발노드 찾기
            Ray ray = new Ray(transform.position, -transform.up);
            LayerMask layer = 1 << LayerMask.NameToLayer("Node");
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1, layer))
            {
                startNode = hit.transform.GetComponent<Node>();
            }
            else
            {
                startNode = null;
            }
            //  A* 알고리즘으로 노드 이동
            if (!isCheck && playerInput.PointBlock && playerInput.PointBlock.layer == LayerMask.NameToLayer("Node") && startNode != null)
            {
                isTrickVisited = new List<bool>();
                //trick 방문처리 초기화
                for (int i = 0; i < trick.Count; i++)
                {
                    isTrickVisited.Add(false);
                }
                isCheck = true;
                cursor.CursorClick();
                isComplete = false;
                completeFindPath = false;
                noWay = false;
            }
            if (isCheck)
            {
                // 노드리스트 부모
                for (int i = 0; i < openNode.Count; i++)
                {
                    openNode[i].parent = null;
                }
                for (int i = 0; i < closeNode.Count; i++)
                {
                    closeNode[i].parent = null;
                }
                // 관련 리스트,변수 비우기
                openNode.Clear();
                closeNode.Clear();
                findPath.Clear();
                findPathPos.Clear();
                idx = 0;
                ratio = 0;
                //// Raycast로 사용자 출발노드 찾기
                //Ray ray = new Ray(transform.position, -transform.up);
                //LayerMask layer = 1 << LayerMask.NameToLayer("Node");
                //RaycastHit hit;
                //if (Physics.Raycast(ray, out hit, 1, layer))
                //{
                //    startNode = hit.transform.GetComponent<Node>();
                //}
                // Raycast로 사용자가 가고자하는 타겟노드 찾기
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

                // 길찾기
                FindPath();
            }
        }
        else if (!isCheck && isComplete)
        {
            anim.SetTrigger("Idle");
            print("Idle1");
        }
        if (completeFindPath && !noWay)
        {
            print("completeFindPath");
            anim.SetTrigger("Move");
            //if (!onFallingBlock)
            //{
            //    anim.SetTrigger("Move");
            //    print("Move");
            //}
            //anim.SetTrigger("Move");
            SimpleMove();
        }
        // 움직이는 블록 위 Player Hierarchy 위치 이동
        OnMovingBlock();
    }


    int idx = 0;
    float ratio = 0;
    public bool matChange;
    public Material mat;
    public Material mat_button;
    Vector3 playerDir;
    public float playerMoveSpeed = 3;
    private bool isComplete = false;

    void SimpleMove()
    {
        if (!onFallingBlock)
        {
            if (currentNode.name == "FallingBlock")
            {
                anim.SetTrigger("Idle");
                print("Idle2");
                onFallingBlock = true;
            }
        }


        //if (playerInput.MoveKey)
        //{
            //anim.SetTrigger("Move");
        //}
        if (findPath.Count - 1 > idx)
        {
            playerDir = findPathPos[idx + 1] - findPathPos[idx];
            // 평지인 경우만 회전
            if (findPath[idx].gameObject.layer == LayerMask.NameToLayer("Node") && !findPath[idx].gameObject.name.Contains("trick"))
            {
                playerDir.y = 0;
                transform.forward = playerDir;


                // Twist 블럭에서 player up 방향 설정
                //RaycastHit hit;
                //if (Physics.Raycast(transform.position, -transform.up, out hit, 1) && hit.collider.CompareTag("Twist"))
                //{
                //    Debug.DrawRay(hit.point, hit.normal, Color.green, 200);
                //    transform.position = hit.transform.position + hit.transform.forward;
                //    transform.up = hit.transform.forward;
                //}
            }
            ratio += 3 * Time.deltaTime;
            // 모든 거리를 일정한 시간으로 이동하도록 설정
            transform.position = Vector3.Lerp(findPathPos[idx], findPathPos[idx + 1], ratio);

            if (ratio >= 1)
            {
                transform.position = findPathPos[idx + 1];
                idx++;
                ratio = 0;
                if (currentNode.gameObject == targetNode.gameObject)
                {
                    isCheck = false;
                    isComplete = true;
                    completeFindPath = false;
                }
            }
        }

        // 이동한 노드 색 초기화
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

    private bool completeFindPath = false;
    // 길찾기
    void FindPath()
    {
        //print($"1: {openNode.Count}, {openNode[0]}");  // 출발노드
        // 중심노드 & 근접노드 찾기
        FindNear();
        //print($"2 : {openNode.Count}, {openNode[0]}");//,{openNode[1]}");  // 출발노드 + 이웃노드
        // 중심Node => ClosedList
        openNode.Remove(currNode);
        closeNode.Add(currNode);
        //print($"3 : {openNode.Count}, {openNode[0]}");  // 이웃노드 중 fCost가 가장 작은 노드 (출발노드 => close)

        // 갈 수 있는 길이 없는 경우
        if (openNode.Count <= 0)
        {
            noWay = true;
            print("noWay");
        }
        else
        {
            // 길찾기 Loop ( 갈 수 있는 길이 있고 targetNode를 찾을 때까지)
            if (openNode.Count > 0 && openNode[0] != targetNode)
            {
                FindPath();
            }
            // targetNode를 찾았다면 path 만들기
            else if (openNode[0] == targetNode)
            {
                findPath.Clear();
                Node Node = targetNode;
                // 부모노드가 없는 startNode까지 거슬러 올라가면서 path 만들기
                while (Node.parent != null)
                {
                    if (matChange)
                    {
                        // 지나갈 노드 색 파란색으로 표시
                        Node.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                    }

                    // 거꾸로 노드 넣기
                    findPath.Insert(0, Node);
                    // 거꾸로 노드 위치 넣기
                    // 상황에 따라 플레이어가 이동해야할 position 다르게 입력
                    if (Node.gameObject.name.Contains("Stair"))
                    {
                        findPathPos.Insert(0, Node.transform.position + new Vector3(0.2f, 0.7f, 0));
                    }
                    else if (Node.gameObject.name.Contains("Button"))
                    {
                        findPathPos.Insert(0, Node.transform.position + new Vector3(0, 0.3f, 0));
                    }
                    else if (Node.gameObject.name.Contains("Final"))
                    {
                        print("Final Position");
                        findPathPos.Insert(0, Node.transform.position + new Vector3(0, 0.463f, 0));
                    }
                    else
                    {
                        findPathPos.Insert(0, Node.transform.position + Vector3.up);
                    }
                    // 부모 노드 거슬러 올라가기
                    Node = Node.parent;
                }
                findPath.Insert(0, startNode);
                findPathPos.Insert(0, transform.position);
            }
        }
        completeFindPath = true;
    }


    void FindNear()
    {
        currNode = openNode[0];  // 비용이 가장 작은 노드

        // 앞
        AddNearOpen(transform.forward);
        // 뒤
        AddNearOpen(-transform.forward);
        // 오른쪽
        AddNearOpen(transform.right);
        // 왼쪽
        AddNearOpen(-transform.right);
        // 위앞 
        AddNearOpen(Vector3.up + transform.forward);
        // 위뒤
        AddNearOpen(Vector3.up - transform.forward);
        // 위좌
        AddNearOpen(Vector3.up - transform.right);
        // 위우
        AddNearOpen(Vector3.up + transform.right);
        // 아래앞
        AddNearOpen(Vector3.down + transform.forward);
        // 아래뒤
        AddNearOpen(Vector3.down - transform.forward);
        // 아래좌
        AddNearOpen(Vector3.down - transform.right);
        // 아래우
        AddNearOpen(Vector3.down + transform.forward);
        // Stage3만 추가 노드 검사
        if (scene.name == "Stage3")
        {
            AddNearOpen(Vector3.up);
            AddNearOpen(transform.right + new Vector3(0, 0.5f, 0));
            AddNearOpen(-transform.right + new Vector3(0, 0.5f, 0));
        }
        

        // trick 부분 연결
        if (currNode.gameObject.CompareTag("Trick"))
        {
            for (int i = 0; i < trick.Count; i++)
            {
                if (!isTrickVisited[i])
                {
                    if (trick[i].trick1.name == currNode.gameObject.name)
                    {
                        Node next = trick[i].trick2;
                        next.parent = currNode;
                        openNode.Add(next);
                        isTrickVisited[i] = true;
                        break;
                    }
                    else if (trick[i].trick2.name == currNode.gameObject.name)
                    {
                        Node next = trick[i].trick1;
                        next.parent = currNode;
                        openNode.Add(next);
                        isTrickVisited[i] = true;
                        break;
                    }
                }
            }
        }

        // openNode 정렬 by fCost
        openNode.Sort(SortByfCost);
    }

    public float rayLength = 0.7f;
    // currNode에서 ray이용해 해당 방향 근접노드 찾기
    void AddNearOpen(Vector3 dir)
    {
        Ray ray = new Ray(currNode.transform.position, dir);
        RaycastHit hit;
        //Debug.DrawRay(currNode.transform.position, dir, Color.blue, 30, false);
        int layer = 1 << LayerMask.NameToLayer("Node");
        if (Physics.Raycast(ray, out hit, rayLength, layer))
        {
            Debug.DrawRay(currNode.transform.position, dir, Color.blue, 30, false);
            Debug.DrawLine(currNode.transform.position, hit.point, Color.red, 200, false);  // 충돌한 지점까지의 Ray선 
            Node Node = hit.transform.GetComponent<Node>();
            Node.SetCost(startNode.transform.position, targetNode.transform.position);
            if (!openNode.Contains(Node) && !closeNode.Contains(Node))
            {
                Node.parent = currNode;
                openNode.Add(Node);
            }
        }
    }

    
    // 현재 플레이어가 밟고 있는 노드 찾는 함수
    void RayCastDown()
    {
        // 레이 생성, 방향은 아래
        Ray playerRay = new Ray(transform.position, -transform.up);
        RaycastHit playerHit;

        // 레이 발사
        if (Physics.Raycast(playerRay, out playerHit))
        {
            Debug.DrawRay(playerHit.point, playerHit.normal, Color.green, 200);
            // 노드를 밟고 있다면 
            if (playerHit.collider.gameObject.layer == LayerMask.NameToLayer("Node"))
            {
                currentNode = playerHit.transform;
                // 만약 currentNode가 checkNode일 경우 player의 Layer Top으로 변경해주기
                if (currentNode == checkNode)
                {
                    ChangeLayersRecursively(transform, "Top");
                }

                
            }
        }
    }

    // 하위 자식 오브젝트 레이어 한번에 변경하는 함수
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
        // 현재 밟고 있는 노드가 움직이는 경우
        if (currentNode.tag == "Move")
        {
            // 플레이어를 그 자식으로 넣는다.
            transform.parent = currentNode;
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

    // 아이템 충돌
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