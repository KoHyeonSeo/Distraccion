using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerTest : MonoBehaviour
{
    public Node startNode;
    public Node currNode;  // openNode에 포함된 노드 중  fCost가 가장 작은 노드
    public Node targetNode;

    public Transform currentNode;
    public Transform checkNode;  // Player Layer 'Top'으로 변경하는 블록

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
    private bool isCheck = false;
    private bool noWay = false;

    Scene scene;
    //MovingGround archB;
    public TwistBlock twist;
    Animator anim;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        anim = GetComponentInChildren<Animator>();
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
        // 사용자 노드 클릭시(Fire2) 초기화
        if (playerInput.MoveKey)
        {
            print("11111111");
            print($"isCheck = {isCheck} / playerInput.PointBlock = {playerInput.PointBlock} / playerInput.PointBlock.layer = {playerInput.PointBlock.layer}");
            //  A* 알고리즘으로 노드 이동
            if (!isCheck && playerInput.PointBlock && playerInput.PointBlock.layer == LayerMask.NameToLayer("Node"))
            {
                isCheck = true;
                print("2222222222");
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
            }
            if (isCheck)
            {
                print("33333333333");
                //print("2222222222");
                //// 노드리스트 부모
                //for (int i = 0; i < openNode.Count; i++)
                //{
                //    openNode[i].parent = null;
                //}
                //for (int i = 0; i < closeNode.Count; i++)
                //{
                //    closeNode[i].parent = null;
                //}
                //// 관련 리스트,변수 비우기
                //openNode.Clear();
                //closeNode.Clear();
                //findPath.Clear();
                //findPathPos.Clear();
                idx = 0;
                ratio = 0;
                // Raycast로 사용자 출발노드 찾기
                Ray ray = new Ray(transform.position, -transform.up);
                LayerMask layer = 1 << LayerMask.NameToLayer("Node");
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1, layer))
                {
                    startNode = hit.transform.GetComponent<Node>();
                }
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
                //        openNode.Add(startNode);d
                //    }
                //}
                #endregion

                // 길찾기
                FindPath();
                isCheck = false;
                isVisited = false;
            }
        }
        // PlayerMove
        SimpleMove();
        // 움직이는 블록 위 Player Hierarchy 위치 이동
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
        if (playerInput.MoveKey)
        {
            anim.SetTrigger("Move");
        }
        if (findPath.Count - 1 > idx)
        {
            ratio += 1 * Time.deltaTime;
            playerDir = findPathPos[idx + 1] - findPathPos[idx];
            Vector3 target = findPathPos[idx + 1] + findPath[idx + 1].transform.right;
            // 모든 거리를 일정한 시간으로 이동하도록 설정
            //1. 가려는 곳도 twist고 지금 있는 곳도 twist인 경우
            if (findPath[idx].CompareTag("Twist") && findPath[idx + 1].CompareTag("Twist"))
            {
                transform.position = findPathPos[idx + 1] + findPath[idx + 1].transform.right;
                //Debug.Log($"==========================================");
                //Debug.Log($"1. 가야할 곳: {findPathPos[idx + 1] + findPath[idx + 1].transform.forward}");
                //Debug.Log($"1. 가야할 객체 위치: {findPathPos[idx + 1]}");
                //Debug.Log($"1. 가야할 객체: {findPath[idx + 1]}");
                //Debug.Log($"==========================================");

                ratio = 0;
                idx++;
            }
            //2. 가려는 곳은 twist가 아니고, 지금 있는 곳은 twist인 경우
            else if(findPath[idx].CompareTag("Twist") && !findPath[idx + 1].CompareTag("Twist"))
            {
                transform.position = findPathPos[idx + 1];
                //Debug.Log($"==========================================");
                //Debug.Log($"2. 가야할 곳: {findPathPos[idx + 1]}");
                //Debug.Log($"2. 가야할 객체 위치: {findPathPos[idx + 1]}");
                //Debug.Log($"2ㄴ. 가야할 객체: {findPath[idx + 1]}");
                //Debug.Log($"==========================================");
                if (ratio >= 1)
                {
                    idx++;
                    ratio = 0;
                }
            }
            //3. 가려는 곳은 twist고 지금 있는 곳은 twist가 아닌 경우
            else if(!findPath[idx].CompareTag("Twist") && findPath[idx + 1].CompareTag("Twist"))
            {
                transform.position = findPathPos[idx + 1] + findPath[idx + 1].transform.right;
                //Debug.Log($"==========================================");
                //Debug.Log($"3. 가야할 곳: {findPathPos[idx + 1] + findPath[idx + 1].transform.forward}");
                //Debug.Log($"3. 가야할 객체 위치: {findPathPos[idx + 1]}");
                //Debug.Log($"3. 가야할 객체: {findPath[idx + 1]}");
                //Debug.Log($"==========================================");
                ratio = 0;
                idx++;
            }
            //4. 가려는 곳, 지금 있는 곳 모두 twist가 아닌 경우
            else
            {
                transform.position = Vector3.Lerp(findPathPos[idx], findPathPos[idx + 1], ratio); 
                if (Vector3.Distance(transform.position, findPathPos[idx + 1] ) < 0.01f)
                {
                    transform.position = findPathPos[idx + 1];
                }
                if (ratio >= 1)
                {
                    idx++;
                    ratio = 0;
                }
            }

            // 회전
            if (findPath[idx].gameObject.layer == LayerMask.NameToLayer("Node") && !findPath[idx].gameObject.name.Contains("trick"))
            {
                // Twist 블럭에서 player back 방향 설정
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -transform.forward, out hit, 1) && hit.collider.CompareTag("Twist"))
                {
                    //hitCount++;
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
                // Twist 블럭에서 player foward 방향 설정
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
                // Twist 블럭에서 player down 방향 설정
                else if (Physics.Raycast(transform.position, -transform.up, out hit, 1) && hit.collider.CompareTag("Twist"))
                {
                    //hitCount++;
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
                // Twist 블럭에서 player left 방향 설정
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
                // Twist 블럭에서 player right 방향 설정
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
                //twist Block이 아닌경우
                else
                {
                    transform.position = hit.transform.position + hit.normal;// + hit.normal * 0.1f;
                    playerDir.y = 0;
                    if (playerDir != Vector3.zero)
                        transform.forward = playerDir;
                }
                //transform.up = findPath[idx].gameObject;
            }
            else
            {
                anim.SetTrigger("Idle");
            }
        }

        //print(hitCount);

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
                //// Lerp 이동
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

            // 회전 && !archB.enabled
            if (findPath[idx].gameObject.layer == LayerMask.NameToLayer("Node") && !findPath[idx].gameObject.name.Contains("trick") )
            {
                //if (archB.enabled) print("3333333333");
                playerDir = (idx == 0) ? findPathPos[idx + 1] - findPathPos[idx] : findPathPos[idx] - findPathPos[idx - 1];
                playerDir.y = 0;
                transform.forward = playerDir;
                //archB.enabled = false;

                // Arch Bezier 이동
                //RaycastHit hit;
                //if (Physics.Raycast(transform.position, -transform.up, out hit, 1) && hit.collider.CompareTag("Arch"))
            }

            // Twist 안 된 경우
            //if(!twist.isTwist)
            //{
            //    transform.position += transform.right;

            //}
            //else
            //{

            //}

        }
        //// 찾은 길 인덱스를 통해 순회
        //if (findPath.Count - 2 > idx)
        //{
        //   // Twist 이동 방식
        //    if (findPath[idx].CompareTag("Twist") && findPath[idx +1].CompareTag("Twist"))
        //    {
        //        //if (idx >= findPath.Count - 2)
        //        //    goto CONTINUE;
        //        transform.position = findPathPos[idx + 1];
        //        idx++;
        //    }
        //    // Node 이동 방식
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
        //    // 회전(trick노드 제외)
        //    if (findPath[idx].gameObject.layer == LayerMask.NameToLayer("Node") && !findPath[idx].gameObject.name.Contains("trick"))
        //    {
        //        playerDir = (idx == 0) ? findPathPos[idx + 1] - findPathPos[idx] : findPathPos[idx] - findPathPos[idx - 1];
        //        playerDir.y = 0;
        //        transform.forward = playerDir;  // 플레이어의 앞방향 : 현재 찾은 노드 위치 -> 다음 찾은 노드 위치

        //        // 플레이어 아래방향으로 ray쏘았을 때 Twist / Arch 노드일 경우
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
        //print($"3 : {openNode.Count}");//, {openNode[0]}");  // 이웃노드 중 fCost가 가장 작은 노드 (출발노드 => close)

        // 갈 수 있는 길이 없는 경우
        if (openNode.Count <= 0)
        {
            noWay = true;
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
                    else if (Node.gameObject.CompareTag("Twist"))
                    {
                        findPathPos.Insert(0, Node.transform.position);
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
    }


    bool isVisited = false;
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
        // Stage3의 경우 아치형노드가 있어 추가 노드 검사 실행
        if (scene.name == "(Legacy)Stage3")
        {
            AddNearOpen(transform.right + new Vector3(0, 0.5f, 0));
            AddNearOpen(-transform.right + new Vector3(0, 0.5f, 0)); 
        }

        // trick부분 연결
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

        // openNode 정렬 by fCost
        openNode.Sort(SortByfCost);
    }

    public float rayLength = 1;
    // currNode에서 ray이용해 해당 방향 근접노드 찾기
    void AddNearOpen(Vector3 dir)
    {
        Ray ray = new Ray(currNode.transform.position, dir);
        RaycastHit hit;
        Debug.DrawRay(currNode.transform.position, dir, Color.blue, 200, false);
        int layer = 1 << LayerMask.NameToLayer("Node");
        if (Physics.Raycast(ray, out hit, rayLength, layer))
        {
            Debug.DrawLine(currNode.transform.position, hit.point, Color.red, 30, false);  // 충돌한 지점까지의 Ray선 
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
    // 현재 플레이어가 밟고 있는 노드 찾는 함수
    void RayCastDown()
    {
        // 레이 생성, 방향은 아래
        Ray playerRay = new Ray(transform.position, -transform.up);
        RaycastHit playerHit;

        // 레이 발사
        if (Physics.Raycast(playerRay, out playerHit))
        {
            // 노드를 밟고 있다면 
            if (playerHit.collider.gameObject.layer == LayerMask.NameToLayer("Node") && !isOnce)
            {
                currentNode = playerHit.transform;
                // 만약 currentNode가 checkNode일 경우 player의 Layer Top으로 변경해주기
                if (currentNode == checkNode)
                {
                    ChangeLayersRecursively(transform, "Top");
                    isOnce = true;
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

    // currentNode
    void OnMovingBlock()
    {
        // 현재 밟고 있는 노드가 움직이는 경우
        if (currentNode.tag == "Move")
        {
            // 플레이어를 그 자식으로 넣는다.
            transform.parent = currentNode.parent;
        }
        else
        {
            transform.parent = null;
        }
    }

    // 비용 계산
    int SortByfCost(Node c1, Node c2)
    {
        if (c1.fCost < c2.fCost) return -1;
        if (c1.fCost > c2.fCost) return 1;
        return 0;
    }

    // Item 충돌
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
