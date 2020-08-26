﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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
    RuntimeAnimatorController animFinish;
    RuntimeAnimatorController animStumble;
    RuntimeAnimatorController animPunch;


    // 행동 관련 번수 선언
    public bool isJumping;
    public float jumpTimer;
    public float stumbleTimer;
    public bool isStumble;
    public bool isPunching;
    public float punchTimer;


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
        isStumble = false;
        stumbleTimer = 0;
        isPunching = false;
        punchTimer = 0;
        point = 0;
        combo = 0;
        maxCombo = 0;
        hp = ConstInfo.InitialHp;
    }

    // 프리팹 불러오기
    void LoadPrefabs()
    {
        animator = GetComponent<Animator>();
        player.GetComponent<Animation>().wrapMode = WrapMode.Loop;
        animIdle = Resources.Load("3DResources/AnimationControllers/JoyRunIdle") as RuntimeAnimatorController;
        animRun = Resources.Load("3DResources/AnimationControllers/JoyRunBattleRun") as RuntimeAnimatorController;
        animWalk = Resources.Load("3DResources/AnimationControllers/JoyRunWalk") as RuntimeAnimatorController;
        animSprint = Resources.Load("3DResources/AnimationControllers/JoyRunSprint") as RuntimeAnimatorController;
        animJump = Resources.Load("3DResources/AnimationControllers/JoyRunJump") as RuntimeAnimatorController;
        animFinish = Resources.Load("3DResources/AnimationControllers/JoyRunFinish") as RuntimeAnimatorController;
        animStumble = Resources.Load("3DResources/AnimationControllers/JoyRunStumble") as RuntimeAnimatorController;
        animPunch = Resources.Load("3DResources/AnimationControllers/JoyRunPunch") as RuntimeAnimatorController;
    }



    void Update()
    {
        if (GameManager.instance.GetGameState() == GameState.Game)
            HandleGame(GameUI.instance.timer);
        else if (GameManager.instance.GetGameState() == GameState.Pause)
            animator.runtimeAnimatorController = Setting.GetCurrentAnimationState() == AnimationState.animation ? animIdle as RuntimeAnimatorController : null;
    }

    // 시간, 체력에 따른 게임 동작 설정
    void HandleGame(float timer) {
        if ((timer == 0 && Setting.GetCurrentTimeState() == TimeState.normal))
        {
            animator.runtimeAnimatorController = Setting.GetCurrentAnimationState() == AnimationState.animation ? animFinish : null;
            GameEnd();
        }
        else if (hp == 0)
        {
            animator.runtimeAnimatorController = Setting.GetCurrentAnimationState() == AnimationState.animation ? animIdle : null;
            GameEnd();
        }
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

        if (isJumping || isStumble || isPunching)
        {
            if (isStumble)
                HandleStumble();
            else {
                if (isJumping)
                    HandlePlayerJumping();
                else if (isPunching)
                    HandlePlayerPunching();
            }

            if (isJumping) {
                jumpTimer += Time.deltaTime;
                if (jumpTimer >= ConstInfo.jumpingTime)
                    InitialJumpState();
            }
            if (isStumble) {
                stumbleTimer += Time.deltaTime;
                if (stumbleTimer >= ConstInfo.stumblingTime)
                    InitialStumbleState();
            }
            if (isPunching) {
                punchTimer += Time.deltaTime;
                if (punchTimer >= ConstInfo.punchingTime)
                    InitialPunchState();
            }
        }
        else
            HandlePlayerMoving(Setting.GetCurrentAnimationState());

    }

    // 플레이어 점프 애니메이션, 점프 타이머 설정
    void HandlePlayerJumping()
    {
        animator.runtimeAnimatorController = animJump as RuntimeAnimatorController;
        if(GameFloorTile.isPunching)
            isPunching = GameFloorTile.isPunching;
    }

    // 점프 상태 초기화
    public void InitialJumpState() {
        if (Setting.GetCurrentAnimationState() == AnimationState.kinect)
            animator.runtimeAnimatorController = null;
        isJumping = false;
        isPunching = false;
        jumpTimer = 0;
    }

    public void HandleStumble()
    {
        animator.runtimeAnimatorController = animStumble as RuntimeAnimatorController;
    }

    public void InitialStumbleState() {
        if (Setting.GetCurrentAnimationState() == AnimationState.kinect)
            animator.runtimeAnimatorController = null;
        isStumble = false;
        stumbleTimer = 0;

    }

    public void HandlePlayerPunching() {
        animator.runtimeAnimatorController = animPunch as RuntimeAnimatorController;
        if(GameFloorTile.isJumping)
            isJumping = GameFloorTile.isJumping;
    }

    public void InitialPunchState() {
        if (Setting.GetCurrentAnimationState() == AnimationState.kinect)
            animator.runtimeAnimatorController = null;
        isPunching = false;
        punchTimer = 0;

    }

    // 플레이어 동작 상태 설정
    void HandlePlayerMoving(AnimationState state)
    {
        if(state == AnimationState.kinect)
            animator.runtimeAnimatorController = null;
        else if (state == AnimationState.animation)
            HandlePlayerRuntimeAnimatorController(Tile.actualSpeed);
        isJumping = GameFloorTile.isJumping;
        isPunching = GameFloorTile.isPunching;
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
        player.transform.position = new Vector3(Avatar.userPosition[(int)AvatarJointType.Body].x * (ConstInfo.runningTrackWidth / ConstInfo.floorUICanvasWidth) + ConstInfo.center, 
            ConstInfo.playerInitialPositionY, ConstInfo.playerInitialPositionZ);
    }



    // 하트에 충돌 시
    public void HeartCollision(int i)
    {
        if (i == 0)
            combo += 1;
        GameUI.instance.ShowHpIncrease();
        GameUI.instance.ChangeCombo(combo);
        hp = hp + ConstInfo.heartTileHpIncrease > ConstInfo.maxHp ? ConstInfo.maxHp : hp + ConstInfo.heartTileHpIncrease;
    }

    // 풍선에 닿을 시
    public void BalloonCollision()
    {
        combo += 1;
        GameUI.instance.balloonCount++;
        GameUI.instance.timer = GameUI.instance.timer + ConstInfo.balloonTimeIncrease > 60 ? 60 : GameUI.instance.timer + ConstInfo.balloonTimeIncrease;
        GameUI.instance.ShowTimeIncrease();
        GameUI.instance.ChangeCombo(combo);
    }

    // 장애물에 층돌 시
    public void ObstacleCollision()
    {
        isStumble = true;
        Tile.extraSpeed = 0;
        int comboWeight = (combo - 1) / 10;
        point += (int) (combo * ((comboWeight / 10f) + 1));
        combo = 0;
        Damaged();
    }

    // 빈 칸을 지날 시
    public void EmptyCollision()
    {
        combo += 1;
        GameUI.instance.ChangeCombo(combo);
    }



    // 데미지 판정 알고리즘
    public void Damaged()
    {
        int hpDecrease = (hp / 2) > 10 ? (hp / 2) : 10;
        if (Setting.GetCurrentHpState() == HpState.normal)
            hp = hp - hpDecrease > 0 ? hp - hpDecrease : 0;
        GameUI.instance.damageEffectTimer = ConstInfo.damageEffectTime;
    }

    // 점수 계산 알고리즘
    void CaculatePoint()
    {
        ResultUI.instance.maxCombo.text = maxCombo.ToString() + " 회";
        ResultUI.instance.playTimeText.text =(Mathf.Round((60 - GameUI.instance.timer + (3 * GameUI.instance.balloonCount)) * 100) / 100) + " 초";
        if (Setting.GetCurrentTimeState() == TimeState.infinite)
            ResultUI.instance.playTimeText.text = "무제한";
        ResultUI.instance.pointText.text = point.ToString();
        MyRankUI.instance.pointText.text = point.ToString();
    }

    // 게임 종료 알고리즘
    public void GameEnd()
    {
        CaculatePoint();
        GameUI.instance.InsertRank(point);
        GameUI.instance.HandleGameEnd();
    }
}
