# Distraccion

### ⛔<b>깃허브 push, pull, merge 주의점</b>
- <b>먼저 작업 전 pull하기!!</b> 
- <b>merge할 때는 유니티 끄고 merge진행!!!</b>
- <b>서로의 스크립트 수정 금지</b>
- <b>create pull request 필수!!</b> 
-------------------
## 개요
- 타겟팅 게임: Monument Vally, one hand clapping, 스쿠비두 게임
- 3인칭 맵 중심 카메라
- 등장인물
    - 주인공
    - NPC
    - 길 안내자
- 스토리: 딸이 나가고 싶어했지만, 위험해 엄마가 반대. 어느날, 딸이 배를 타고 나갔다가 안들어와서 엄마가 걱정되서 딸을 찾으러 가는 스토리
- 결말: 추후결정
        1. 밖에 나갔더니 어떤 사람을 만나, 그 사람이 어떤 아이가 쓰러져있었다고 말함. 
        2. 엄마가 꽃을 들고 다닐건데(애가 타고간 배에 꽃이 있었음), 결말이 그 꽃과 연관되었음 좋겠다. 
        3. 퀘스트나 스테이지를 깬 뒤 문양같은걸 모으는데, 그거와 관련있으면 좋겠다.
- 구현하고자 하는 것: 맵 중심(머리 좀 많이 써야함), 퍼즐요소
- 플레이어는 클릭 하면 알아서 찾아감
- 스테이지와 스테이지 사이에 문지기 같은 것이 있고, 음성 퀘스트를 깨야 다음 스테이지로 이동 가능
- 희망이나, 좌절감 게이지를(슬라이더 말고 다른 모양으로) 만들어 넣고 다 소진시 게임 오버
- 게임 오버하면 좌절하는 모습이 나오고, 다시 도전할건지 포기할건지 물어본다.
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
        뭐, 까마귀가 도망가거나 쓰러진다. 그러면 그 길은 통과가 가능
    - 까마귀에 길을 막아야함 (공을 굴리던)
- item
- Camera
- Sound
    - 유니티 음성인식
- PhysicsUtility.cs 
    - 계산할 것이 있으면, 
    - 계산 공식을 넣어두자
- GameManager.cs
- Object 
    - block -> 회전 블럭이 주겠지? 
- UI
- Effect
- Start / Ending
- 스토리 라인 짜기
    - 기능 짜고 스토리 라인 짜기
- 블럭 돌아갈때 소리 나오는거
- 소리도 중요할 듯
- 맵은 상황에 맞춰 제작 (몇개 제작할 것인지 아직 생각 X)
## <b>프로토타입까지 해야할 것</b>
- 기간이 생각보다 길어... 더 많이 할 수 있을듯...? 아니였고,,;;;
- 기능 중심
- 강수현
    - Player  
        - 해야할 것 / 내일
            - 착시 부분이동 V
            - nullException 처리 V
            - 예외처리부분 (버그 테스팅 후 체크)
            - 아이템 습득 V
    - 맵 배치 V
    - 이동 블럭 / 월요일
- 고현서
    - 회전 블럭 V 
    - TwistBlock 구현 V
    - Enemy Structure and Test V  
    - 버튼 밟으면 회전 V
    - 블럭 돌아갈때 소리 / 내일 V
    - Enemy V
        - 각 Enemy마다 다른 Mission, MissionComplete V
    - 맵 배치 
        - 내일 / Copy 맵 선정 V
        - 일요일 / 맵 배치 시작 V
        - 월요일 / 플레이어 놓고 테스트 V
## <b>알파까지 해야할 것</b>
- Enemy Quest 더 만들기 - HS
- 퀘스트 소리 미리 만들어 놓기(절대 공개 안해.) -> 이게 주가 되어야할듯...? - HS
    - 소리를 받아서 문양을 만들기 (모바일에서 손으로 직접 그렸던 것) 베지어 곡선 사용 -> T를 건드리면 될듯? - SH + HS
- 스토리 요소 - SH + HS
    - 스타트때 1인칭 시점으로 주인공의사랑하는 생물(강아지)이.. 나가고 싶다며 엄마를 바라봄. 엄마가 걱정하는 말을 하며 멀리나가지 말라 말함.
    근데 멀리 나갔다가 물에 빠져 사망, 엄마가 기다리다가 꽃을 태운 배가 다가옴.. 죽음을 인지하고 울다가. 너의 꿈을 이뤄주마라는 말을 하며 스타트
    - 결말: 엄마가 꽃을 심어주고, 액자를 내려놓음. 그 액자의 사진에는 강아지와 엄마가 함께 행복하게 찍힌 사진
- 이 스토리에 맞는 애니메이션 미리 체킹해놓기
- UI
    - Menu - SH
    - Start / End - SH 
    - GameOver - SH
    - Item 상호작용 UI - SH
    - Item 창 - HS
- 사운드 - SH + HS
    - 배경음악
    - 블럭 돌아갈때 소리 -> 돌이 돌아가는 소리
    - 이외
- 에셋  - SH + HS
( 디자인 -> 에셋 )
    - 아이템
    - 플레이어
    - Enemy
    - 폰트    
- 추가 블록
    - 휘어져있는 블록 - HS
    - twist block -> Player가 갈 수 있도록 - SH

## <b> 베타까지 해야할 것</b>
- 인트로
- 씨네머신
- 이펙트
- 버그 테스팅
