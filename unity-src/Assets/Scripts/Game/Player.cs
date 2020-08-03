﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public enum MovingState
{
    kinect,
    animation
}

public class Player : MonoBehaviour
{
    public static Player instance;

    // 오브젝트 변수 선언
    public static GameObject player;
    public static GameObject highlight;

    // 애니매이터 변수 선언
    Animator animator;

    // 애니메이터 컨트롤러 변수 선언
    RuntimeAnimatorController animIdle;
    RuntimeAnimatorController animRun;
    RuntimeAnimatorController animWalk;
    RuntimeAnimatorController animSprint;
    RuntimeAnimatorController animJump;
    MovingState currentMovingState;

    // 점프 관련 번수 선언
    public static bool isJumping;
    public float jumpTimer;

    //점수 변수 선언
    public static int point;

    // 콤보, 체력 관련 변수, 상수 선언
    public int maxCombo;
    public int combo;
    public int hp;

    // 인스턴스 설정
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitialValues();
    }

    void Update()
    {
        HandleGame(GameUI.timer);
    }

    // 변수 초기화
    void InitialValues()
    {
        // 오브젝트 변수 초기화
        player = gameObject;
        highlight = GameObject.Find("highlight");
        InitializePrefabs();

        isJumping = false;
        jumpTimer = 0;
        point = 0;
        combo = 0;
        maxCombo = 0;
        hp = ConstInfo.startHp;
        currentMovingState = MovingState.animation;
    }

    void HandleGame(float timer) {
        if (timer == 0)
            GameEnd();
        else
            HandlePlayer();
    }


    // 프리팹 불러오기
    void InitializePrefabs()
    {
        animator = GetComponent<Animator>();
        player.GetComponent<Animation>().wrapMode = WrapMode.Loop;
        animIdle = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Idle") as RuntimeAnimatorController;
        animRun = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Run") as RuntimeAnimatorController;
        animWalk = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Walk") as RuntimeAnimatorController;
        animSprint = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Sprint") as RuntimeAnimatorController;
        animJump = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Jump") as RuntimeAnimatorController;
        
    }

    // 플레이어 점프, 이동 설정 알고리즘
    void HandlePlayer()
    {
        HandlePlayerPosition();
        if (isJumping)    
            HandlePlayerJumping();
        else
            HandlePlayerMoving(currentMovingState);
    }

    // 플레이어 점프 애니메이션, 점프 타이머 설정
    void HandlePlayerJumping()
    {
        animator.runtimeAnimatorController = animJump as RuntimeAnimatorController;
        jumpTimer += Time.deltaTime;
        if (jumpTimer >= ConstInfo.jumpTime)
        {
            isJumping = false;
            jumpTimer = 0;
        }

    }

    void HandlePlayerMoving(MovingState state)
    {
        if(state == MovingState.kinect)
            animator.runtimeAnimatorController = null;
        else if (state == MovingState.animation)// 플레이어 달리기 애니메이션 
            HandlePlayerRuntimeAnimatorController(Tile.actualSpeed);

        if (GameFloorTile.isJumping)
            isJumping = true;     
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
    public static void HandlePlayerPosition()
    {
        if (GameManager.instance.GetGameState() == GameState.game)
            player.transform.position = 
                new Vector3(Avatar.userPosition.x * (ConstInfo.tileScaleX / ConstInfo.floorTileScaleX) + ConstInfo.center, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
        else
            player.transform.position = new Vector3(ConstInfo.center, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);

    }

    // 하트에 충돌 시
    public void MeetHeart()
    {
        GameUI.instance.ChangeCombo(combo + ConstInfo.heartTileComboIncrease);

        if (hp < ConstInfo.maxHp)
        {
            hp = hp + ConstInfo.heartTileHpIncrease;

            if (hp > ConstInfo.maxHp)
                hp = ConstInfo.maxHp;
        }
    }

    // 풍선에 닿을 시
    public void MeetBalloon()
    {
        combo += ConstInfo.balloonComboIncrease;
        GameUI.instance.ChangeCombo(combo);
    }

    // 장애물에 층돌 시
    public void MeetObstacle()
    {
        Tile.extraSpeed = 0;
        GameUI.instance.ChangeCombo(0);
        HavaDamaged();

    }

    // 빈 칸을 지날 시
    public void MeetEmpty()
    {
        combo += ConstInfo.emptyTileComboIncrease;
        GameUI.instance.ChangeCombo(combo);
    }

    // 데미지 판정 알고리즘
    public void HavaDamaged()
    {
        if ((hp / 2) > 10)
            hp = hp - (hp / 2);
        else
            hp -= 10;


        if (hp <= 0)
        {
            hp = 0;
            GameEnd();
        }
        else if (hp > 100)
            hp = 100;
    }

    // 점수 계산 알고리즘
    void CaculatePoint()
    {
        point = combo + (int)(60f - GameUI.timer);

        ResultUI.instance.maxCombo.text = "최대 콤보 횟수 : " + maxCombo.ToString() + " 회";
        ResultUI.instance.playTime.text = "진행 시간 : " + (60 - GameUI.timer) + " 초";
        ResultUI.instance.point.text = "점수 : " + point;
        MyRankUI.instance.point.text = "점수 : " + point;
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