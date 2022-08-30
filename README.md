# Distraccion

## 개요
- 타겟팅 게임: Monument Vally, one hand clapping, 스쿠비두 게임
- 3인칭 맵 중심 카메라
- 등장인물
    - 주인공
    - NPC
    - 길 안내자

- 스토리:
    - 스타트때 강아지의 1인칭 시점으로 주인공의사랑하는 생물(강아지)이.. 나가고 싶다며 엄마를 바라봄. 주인공이 걱정하는 말을 하며 멀리나가지 말라 말함.
    - 근데 멀리 나갔다가 물(또는 구덩이)에 빠져 사망, 주인공이 기다리다가 꽃을 태운 배가 다가옴.. 죽음을 인지하고 울다가. 너의 꿈을 이뤄주마라는 말을 하며 스타트
    -  결말: 주인공이 꽃을 심어주고, 액자를 내려놓음. 그 액자의 사진에는 강아지와 주인공이 함께 행복하게 찍힌 사진 
- 구현하고자 하는 것: 맵 중심, 퍼즐요소
- 플레이어는 클릭 하면 알아서 찾아감
- 스테이지와 스테이지 사이에 문지기가 있고, 음성 퀘스트를 깨야 다음 스테이지로 이동 가능
- 희망이나, 좌절감 게이지를(슬라이더X other shape) 만들어 넣고 다 소진시 게임 오버
- 게임 오버하면 무서워 도망가는 모습이 나오고, 다시 도전할건지 포기할건지 물어본다.
## 기획 
- 업무분담
    - 구현 위기 부분: 휘어진 블럭, 돌리면 꽈배기 같이 꼬여지는 블록
        - 이 부분은 여러 블럭들을 계단형식으로 여러개 쌓아서 곡선으로 보이게끔 설치해야함

    - 강수현
        - Object
            - 이동하는 블럭
            - 밟을 거 같을 때  떨어지는 블럭
        - Stage 1
        - Stage 2
        - 각자 맵에 맞는 함정 만들기
        - Player
    - 고현서
        - Object
            - 손으로 회전하는 블럭
            - 블럭 돌리면 맵 중 몇 개가 회전하는 블럭
            - 버튼 밟으면 회전하는 블럭(플레이어가 위에 있으면 같이 돌아감)
        - Stage 3
        - Stage 4
        - 각자 맵에 맞는 함정 만들기
        - 데이터 전달 관련 스크립트 구축
## Project Structure
1. 블럭이 판단 -> (플레이어가 블럭을 밟으면) Ray 쏴서 판단
2. 그 정보를 밟은 플레이어에게 전달 어디에 길이 있는지. 
---
1. 블럭을 클릭 

## 넣어야할 요소들
- Player
    - PlayerInput.cs 
    - PlayerMove.cs -> 2. 그걸 받아서 그거에 맞는 움직임
    - 
- Enemy
    - 각 길에 까마귀가 있고, 아이템을 습득 뒤 적절히 상황에 맞는 아이템을 해당 까마귀에게 적용하면 
        까마귀가 도망가거나 쓰러진다. 그러면 그 길은 통과가 가능
    - 까마귀가 길을 막아야함
- item
- Camera
- Sound
    - 유니티 음성인식
- PhysicsUtility.cs 
    - 계산할 것이 있으면, 
    - 계산 공식을 넣어두자
- GameManager.cs
- Object 
    - block -> 회전 블럭
- UI
- Effect
- Start / Ending
- 스토리 라인 짜기
    - 기능 짜고 스토리 라인 짜기
- 블럭 돌아갈때 소리 나오는거
- 소리도 중요할 듯
- 맵은 상황에 맞춰 제작 (몇개 제작할 것인지 아직 생각 X)
## <b>프로토타입까지 해야할 것</b>
- 기능 중심
- <b>강수현</b>
    - Player  
        - 해야할 것 / 내일
            - 착시 부분이동
            - nullException 처리 
            - 예외처리부분 (버그 테스팅 후 체크)
            - 아이템 습득 
    - 맵 배치 
    - 이동 블럭 / 월요일
- <b>고현서</b>
    - 회전 블럭 
    - TwistBlock 구현
    - Enemy Structure and Test 
    - 버튼 밟으면 회전
    - 블럭 돌아갈때 소리
    - Enemy
        - 각 Enemy마다 다른 Mission, MissionComplete 
    - 맵 배치 
        - 내일 / Copy 맵 선정
        - 일요일 / 맵 배치 시작 
        - 월요일 / 플레이어 놓고 테스트 
## <b>알파까지 해야할 것</b>
- <b>고현서</b>
    - Enemy Quest 더 만들기 
    - 소리 퀘스트 Base 구축 
    - 스토리 요소
        - 이 스토리에 맞는 애니메이션 미리 체킹해놓기 
    - Entity
        - Player Scared 제작 
    - UI
        - GameOver 
        - Item 상호작용 UI 
        - Item 창 
    - 사운드 
        - 배경음악 
        - 블럭 돌아갈때 소리 -> 돌이 돌아가는 소리 
        - 이외 
    - 에셋 
        - 아이템 
        - 플레이어 
        - Enemy 
        - 폰트 
    - 추가 블록
        - twist block 
    - Map 
    - Bug Testing 
    - Stage Testing (Complete List)
        - Stage 0 
        - Stage 1 
    - Build Testing 
- <b>강수현</b>
    - 스토리 요소
        - 이 스토리에 맞는 애니메이션 미리 체킹해놓기 
    - UI
        - Menu 
        - Start / End 
    - 사운드 
        - 배경음악 
        - 이외 
    - 에셋
        - 아이템 
        - 플레이어 
        - Enemy 
        - 폰트 
    - Map 
    - Bug Testing 
    - Stage Testing (Complete List)
        - Stage 2 
        - Stage 3 
    - Build Testing 
## <b> 베타까지 해야할 것</b>
- <b>알파 feedback</b>
    - 플레이어 버그 
    - 배경이 하얀색이라서 플레이어가 안보인다.
- <b>고현서</b>
    - Middle Secret Stage 완성(Stage 2개) 
    - 씨네머신
        - Stage 0 이전의 Starting Story 씨네머신 
    - 각자 Stage에 문양 넣는거 구현
        - Stage 0에서 노래 들리면서 위로 올라가며 문양이 그려짐 (우리 로고같은거 넣으면 좋을 것 같다.) 
            - 그려지면서 나오는 이쁜거 그거 구현 
                - 카메라를 단순히 위로 올려서 문양 보여줌)
    - 이펙트 (각자 넣고 싶은 곳에 넣기)
    - 버그 테스팅
- <b>강수현</b>
    - Stage 3 대체 맵 제작
        - Drag 블록 사용할 맵으로 선정
        - Drag Block 구현
    - 씨네머신 
        - Stage Complete 후 Ending Story 씨네머신
            - 초원
    - 각자 Stage에 문양 넣는거 구현
    - 이펙트 (각자 넣고 싶은 곳에 넣기)
    - 버그 테스팅

## <b> Script </b>
### Hyeonseo Ko (고현서)
- Entity
    - Player
        - PlayerInput.cs
        - 
