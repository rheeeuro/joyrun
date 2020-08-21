using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{
    // 인스턴스 변수 선언
    public static Player instance;

    // 오브젝트 변수 선언
    public GameObject player;
    public GameObject highlight;

    // 애니매이터 변수 선언
    Animator animator;

    // 애니메이터 컨트롤러 변수 선언
    RuntimeAnimatorController animIdle;
    RuntimeAnimatorController animRun;
    RuntimeAnimatorController animWalk;
    RuntimeAnimatorController animSprint;
    RuntimeAnimatorController animJump;

    // 점프 관련 번수 선언
    public bool isJumping;
    public float jumpTimer;

    //점수 변수 선언
    public int point;

    // 콤보, 체력 관련 변수, 상수 선언
    public int maxCombo;
    public int combo;
    public int hp;

    // 인스턴스 설정
    private void Awake() { instance = this; }

    void Start()
    {
        InitialObjects();
        InitialValues();
        LoadPrefabs();
    }

    // 오브젝트 변수 초기화
    void InitialObjects() {
        player = gameObject;
        highlight = GameObject.Find("highlight");
    }

    // 변수 초기화
    void InitialValues()
    {
        isJumping = false;
        jumpTimer = 0;
        point = 0;
        combo = 0;
        maxCombo = 0;
        hp = ConstInfo.startHp;
    }

    // 프리팹 불러오기
    void LoadPrefabs()
    {
        animator = GetComponent<Animator>();
        player.GetComponent<Animation>().wrapMode = WrapMode.Loop;
        animIdle = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Idle") as RuntimeAnimatorController;
        animRun = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Run") as RuntimeAnimatorController;
        animWalk = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Walk") as RuntimeAnimatorController;
        animSprint = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Sprint") as RuntimeAnimatorController;
        animJump = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Jump") as RuntimeAnimatorController;
    }



    void Update()
    {
        if (GameManager.instance.GetGameState() == GameState.game)
            HandleGame(GameUI.instance.timer);
        else
            animator.runtimeAnimatorController = Setting.GetCurrentAnimationState() == AnimationState.animation ? animIdle as RuntimeAnimatorController : null;
    }

    // 시간, 체력에 따른 게임 동작 설정
    void HandleGame(float timer) {
        if ((timer == 0 && Setting.GetCurrentTimeState() == TimeState.normal) || hp == 0)
            GameEnd();
        else
            HandlePlayer();
    }



    // 플레이어 점프, 이동 설정 알고리즘
    void HandlePlayer()
    {
        HandlePlayerState();
        HandlePlayerAction();
    }

    // 플레이어 상태 업데이트 (체력, 위치)
    void HandlePlayerState() {
        if (Setting.GetCurrentHpState() == HpState.immortal)
            hp = 100;
        if (GameManager.instance.GetKinectState())
            HandlePlayerPosition();
    }

    // 플레이어 동작 업데이트
    void HandlePlayerAction()
    {
        if (isJumping)
            HandlePlayerJumping();
        else
            HandlePlayerMoving(Setting.GetCurrentAnimationState());
    }

    // 플레이어 점프 애니메이션, 점프 타이머 설정
    void HandlePlayerJumping()
    {
        animator.runtimeAnimatorController = animJump as RuntimeAnimatorController;
        jumpTimer += Time.deltaTime;
        if (jumpTimer >= ConstInfo.jumpTime)
            InitialJumpState();
    }

    // 점프 상태 초기화
    public void InitialJumpState() {
        if(Setting.GetCurrentAnimationState() == AnimationState.kinect)
            animator.runtimeAnimatorController = null;
        isJumping = false;
        jumpTimer = 0;
    }

    // 플레이어 동작 상태 설정
    void HandlePlayerMoving(AnimationState state)
    {
        if(state == AnimationState.kinect)
            animator.runtimeAnimatorController = null;
        else if (state == AnimationState.animation)
            HandlePlayerRuntimeAnimatorController(Tile.actualSpeed);
        isJumping = GameFloorTile.isJumping;
    }

    // 플레이어 달리기 애니메이션 설정
    void HandlePlayerRuntimeAnimatorController(float speed)
    {
        if (speed <= 40)
            animator.runtimeAnimatorController = animWalk as RuntimeAnimatorController;
        else if (speed > 40 && speed <= 60)
            animator.runtimeAnimatorController = animRun as RuntimeAnimatorController;
        else
            animator.runtimeAnimatorController = animSprint as RuntimeAnimatorController;
    }

    // 아바타 위치로 플레이어 위치 고정
    public void HandlePlayerPosition()
    {
        player.transform.position = new Vector3(Avatar.userPosition.x * (ConstInfo.trackWidth / ConstInfo.floorUICanvasWidth) + ConstInfo.center, 
            ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
    }



    // 하트에 충돌 시
    public void HeartCollision()
    {
        combo += ConstInfo.heartTileComboIncrease;
        GameUI.instance.ChangeCombo(combo);
        hp = hp + ConstInfo.heartTileHpIncrease > ConstInfo.maxHp ? ConstInfo.maxHp : hp + ConstInfo.heartTileHpIncrease;
    }

    // 풍선에 닿을 시
    public void BalloonCollision()
    {
        combo += ConstInfo.balloonComboIncrease;
        GameUI.instance.ChangeCombo(combo);
    }

    // 장애물에 층돌 시
    public void ObstacleCollision()
    {
        Tile.extraSpeed = 0;
        combo = 0;
        Damaged();
    }

    // 빈 칸을 지날 시
    public void EmptyCollision()
    {
        combo += ConstInfo.emptyTileComboIncrease;
        GameUI.instance.ChangeCombo(combo);
    }



    // 데미지 판정 알고리즘
    public void Damaged()
    {
        int hpDecrease = (hp / 2) > 10 ? (hp / 2) : 10;
        if (Setting.GetCurrentHpState() == HpState.normal)
            hp = hp - hpDecrease > 0 ? hp - hpDecrease : 0;
        GameUI.instance.damageEffectTimer = ConstInfo.damageShowTime;
    }

    // 점수 계산 알고리즘
    void CaculatePoint()
    {
        point = combo + (int)(60 - GameUI.instance.timer);
        ResultUI.instance.maxCombo.text = maxCombo.ToString() + " 회";
        ResultUI.instance.playTime.text =(Mathf.Round((60 - GameUI.instance.timer) * 100) / 100) + " 초";
        ResultUI.instance.point.text = point.ToString();
        MyRankUI.instance.point.text = point.ToString();
    }

    // 게임 종료 알고리즘
    public void GameEnd()
    {
        animator.runtimeAnimatorController = null;
        CaculatePoint();
        GameUI.instance.InsertRank(point);
        GameUI.instance.HandleGameEnd();
    }
}
