using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    public Node startNode;
    public Node currNode;  // openNode에 포함된 노드 중  fCost가 가장 작은 노드
    public Node targetNode;
    public Transform currentNode;
    public Node trick1;
    public Node trick2;

    public List<Node> openNode = new List<Node>();  // 값을 정하기 전의 노드 리스트
    public List<Node> closeNode = new List<Node>();  // 값이 정해진 노드 리스트
    public List<Node> findPath = new List<Node>();
    public List<Vector3> findPathPos = new List<Vector3>();

    private PlayerInput playerInput;
    private bool isCheck = false;
    

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    void Update()
    {
        RayCastDown();
        // 사용자 노드 클릭시(Fire2) 초기화
        if (playerInput.MoveKey)
        {
            // 노드인 PointBlock을 단 한번만 체크
            if (!isCheck && playerInput.PointBlock && playerInput.PointBlock.layer == LayerMask.NameToLayer("Node"))
            {
                isCheck = true;
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
                // Raycast로 사용자 출발노드 찾기
                Ray ray = new Ray(transform.position, -transform.up);
                LayerMask layer = 1 << LayerMask.NameToLayer("Node");
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1, layer))
                {
                    startNode = hit.transform.GetComponent<Node>();
                    //startNode.walkAble = true;
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
                //        openNode.Add(startNode);
                //    }
                //}
                #endregion


                // TrickBlock에 있을 때 위치 이동 
                //OnTrickBlock();

                // 길찾기
                FindPath();
                isCheck = false;
                isVisited = false;
            }
        }
        // 플레이어가 노드가 아닌 다른 곳을 선택할 경우(예외처리)
        //else
        //{

        //}
        // 플레이어 이동
        SimpleMove();
        // 움직이는 블록 위 Player Hierarchy 위치 이동
        OnMovingBlock();
    }

    int idx = 0;
    float ratio = 0;
    public Material mat;
    public Material mat_button;
    void SimpleMove()
    {
        if (findPath.Count - 1 > idx)
        {
            ratio += 3 * Time.deltaTime;
            // 모든 거리를 일정한 시간으로 이동하도록 설정
            transform.position = Vector3.Lerp(findPathPos[idx], findPathPos[idx + 1], ratio);
            Vector3 playerDir = findPathPos[idx + 1] - findPathPos[idx];
            // 평지인 경우만 회전
            if (findPath[idx].gameObject.layer == LayerMask.NameToLayer("Node"))
            {
                playerDir.y = 0;
                transform.forward = -playerDir;
                //transform.rotation = Quaternion.LookRotation(-playerDir);
            }
            if (ratio >= 1)
            {
                idx++;
                ratio = 0;
            }
        }

        // 이동한 노드 색 초기화
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

    // 길찾기
    void FindPath()
    {
        //print($"Before1: {openNode.Count}, {openNode[0]}");  // 출발노드
        // 중심노드 & 근접노드 찾기
        FindNear();
        //print($"Before2 : {openNode.Count}, {openNode[0]},{openNode[1]}");  // 출발노드 + 이웃노드
        // 중심Node => ClosedList
        openNode.Remove(currNode);
        closeNode.Add(currNode);
        //print($"After : {openNode.Count}, {openNode[0]}");  // 이웃노드 중 fCost가 가장 작은 노드 (출발노드 => close)
        // 길찾기 Loop ( 갈 수 있는 길이 있고 targetNode를 찾을 때까지)
        if (openNode.Count > 0 && openNode[0] != targetNode)
        {
            FindPath();
        }
        // 길이 없는 경우
        else if (openNode == null)
        {
            print("No Way");
        }
        // targetNode를 찾았다면 path 만들기
        else if (openNode[0] == targetNode)
        {
            findPath.Clear();
            Node Node = targetNode;
            // 부모노드가 없는 startNode까지 거슬러 올라가면서 path 만들기
            while (Node.parent != null)
            {
                // 지나갈 노드 색 파란색으로 표시
                Node.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                // 거꾸로 노드 넣기
                findPath.Insert(0, Node);
                // 거꾸로 노드 위치 넣기
                // 계단의 경우 findPathPos 다르게 입력해주기
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
                // 부모 노드 거슬러 올라가기
                Node = Node.parent;
            }
            findPath.Insert(0, startNode);
            findPathPos.Insert(0, transform.position);

            print("Path Completed!");
        }
        // 갈 수 있는 길이 없는 경우
        else
        {
                Debug.Log("갈 수 있는 길이 없습니다.");
                return;
        }
    }

    bool isVisited = false;
    void FindNear()
    {
        //try
        //{
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

        // 만약 trick1 노드가 이웃노드를 찾는 중이라면 trick2를 openNode에 넣어주기
        if (isVisited == false && currNode.gameObject.name == "trick1")
        {
            trick2.parent = currNode;
            openNode.Add(trick2);
            isVisited = true;
        }
        // 만약 trick2 노드가 이웃노드를 찾는 중이라면 trick1를 openNode에 넣어주기
        if (isVisited == false && currNode.gameObject.name == "trick2")
        {
            trick1.parent = currNode;
            openNode.Add(trick1);
            isVisited = true;
        }
        // openNode 정렬 by fCost
        openNode.Sort(SortByfCost);
        //}
        //catch
        //{
        //    Debug.Lo
        //    Debug.Log("선택영역이 노드가 아닙니다.");
        //}

    }

    // 해당 방향 근접노드 찾기
    void AddNearOpen(Vector3 dir)
    {
        Ray ray = new Ray(currNode.transform.position, dir);
        RaycastHit hit;
        Debug.DrawRay(currNode.transform.position, dir, Color.blue, 200, false);
        int layer = 1 << LayerMask.NameToLayer("Node");
        if (Physics.Raycast(ray, out hit, 1, layer))
        {
            Debug.DrawLine(currNode.transform.position, hit.point, Color.red, 200, false);  // 충돌한 지점까지의 Ray선 
            Node Node = hit.transform.GetComponent<Node>();
            //Node.walkAble = true;
            Node.SetCost(startNode.transform.position, targetNode.transform.position);
            if (!openNode.Contains(Node) && !closeNode.Contains(Node)) //&& Node.walkAble)
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
            // 노드를 밟고 있다면 
            if (playerHit.collider.gameObject.layer == LayerMask.NameToLayer("Node"))
            {
                currentNode = playerHit.transform;
                // 밟고 있는 노드가 trick1일 경우
                //if (currentNode.gameObject.name == "trick1")
                //{
                //    trick1 = currentNode.GetComponent<Node>();
                //}
            }
        }
    }

    void OnMovingBlock()
    {
        // 현재 밟고 있는 노드가 움직이는 경우
        if (currentNode.tag == "move")
        {
            // 플레이어를 그 자식으로 넣는다.
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            for (int i=0; i < GameManager.Instance.ItemProp.Count; i++)
            {
                if (GameManager.Instance.ItemProp[i].Item == other.gameObject)
                {
                    //GameManager.Instance.ItemProp[i].isHaveItem = true;
                    return;
                }
            }
            Destroy(other.gameObject);
        }
    }
}