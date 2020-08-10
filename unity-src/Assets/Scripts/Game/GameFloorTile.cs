﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;

public class GameFloorTile : MonoBehaviour
{
    // 바닥 UI 타일
    public GameObject leftFloorTile;
    public GameObject centerFloorTile;
    public GameObject rightFloorTile;
    
    public static GameObject centerTile;
    public GameObject newGameTilePause;
    public GameObject newGameTileResult;
    public GameObject toMenuTilePause;
    public GameObject toMenuTileResult;
    public GameObject nextPageTile;

    // 발위치 원 변수
    public GameObject leftFootPrint;
    public GameObject rightFootPrint;

    // 유저 상태 변수
    public static bool isJumping;
    public static bool isPunching;
    public static bool stepSide;
    public static float stepRecordTime;
    public static List<float> steps;

    public static List<float> left;
    public static List<float> right;

    // 버튼 타이머 변수 - 0: newgame pause, 1: to menu pause, 2: next page, 3: new game result, 4: to menu result
    public float[] uiTimer;

    void Start()
    {
        GameManager.instance.SetGameState(GameState.game);
        InitialObjects();
        InitialValues();
    }

    // 게임오브젝트 불러오기
    void InitialObjects()
    {
        leftFloorTile = GameObject.Find("FloorTile-left");
        centerFloorTile = GameObject.Find("FloorTile-center");
        rightFloorTile = GameObject.Find("FloorTile-right");
        centerTile = GameObject.Find("CenterTile");
        newGameTilePause = GameObject.Find("NewGameTilePause");
        newGameTileResult = GameObject.Find("NewGameTileResult");
        toMenuTilePause = GameObject.Find("ToMenuTilePause");
        toMenuTileResult = GameObject.Find("ToMenuTileResult");
        nextPageTile = GameObject.Find("NextPageTile");

        leftFootPrint = GameObject.Find("Footprint-left");
        rightFootPrint = GameObject.Find("Footprint-right");
    }

    // 변수 초기화
    void InitialValues()
    {
        // 상태변수 초기화
        isJumping = false;
        isPunching = false;
        stepSide = false;

        // 걸음 관련 변수 초기화
        stepRecordTime = 0;
        InitialStepRecords();

        // UI 타이머 배열 초기화
        uiTimer = new float[5] { 0, 0, 0, 0, 0 };
    }

    // 걸음 시간 리스트 초기화 (0)
    public static void InitialStepRecords()
    {
        steps = Enumerable.Repeat<float>(0, 5).ToList<float>();
        left = Enumerable.Repeat<float>(5, 5).ToList<float>();
        right = Enumerable.Repeat<float>(5, 5).ToList<float>();
    }

    // 걸음 시간 측정 (+ fixedDeltaTime)
    private void FixedUpdate()
    {
        stepRecordTime += Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        HandleTileActive();

        // 위치 업데이트
        HandleFootPrint();

        // 아바타와 바닥 UI 상호작용
        if (GameManager.instance.GetKinectState() && GameManager.instance.GetGameState() == GameState.game)
            HandleFloorTiles();
        HandleJump();
        HandleSteps();
        HandleAvatarPunch();

        HandleGameTiles();
        HandleKeyboard();
        // 일시정지 설정 (키넥트가 있는 경우만 실행할 것)
        if (GameManager.instance.GetKinectState()) 
            HandlePause();
    }

    // 바닥 UI 타일 보여주기, 감추기
    void HandleTileActive()
    {
        if (GameManager.instance.GetGameState() == GameState.game)
        {
            leftFloorTile.SetActive(true);
            centerFloorTile.SetActive(true);
            rightFloorTile.SetActive(true);
            
            centerTile.SetActive(false);

            newGameTilePause.SetActive(false);
            toMenuTilePause.SetActive(false);
            nextPageTile.SetActive(false);
            newGameTileResult.SetActive(false);
            toMenuTileResult.SetActive(false);
        }
        else if (GameManager.instance.GetGameState() == GameState.pause)
        {
            leftFloorTile.SetActive(false);
            centerFloorTile.SetActive(false);
            rightFloorTile.SetActive(false);
            
            centerTile.SetActive(true);

            newGameTilePause.SetActive(true);
            toMenuTilePause.SetActive(true);
            nextPageTile.SetActive(false);
            newGameTileResult.SetActive(false);
            toMenuTileResult.SetActive(false);
        }
        else if (GameManager.instance.GetGameState() == GameState.result)
        {
            leftFloorTile.SetActive(false);
            centerFloorTile.SetActive(false);
            rightFloorTile.SetActive(false);
            
            centerTile.SetActive(true);

            newGameTilePause.SetActive(false);
            toMenuTilePause.SetActive(false);
            nextPageTile.SetActive(true);
            newGameTileResult.SetActive(false);
            toMenuTileResult.SetActive(false);
        }
        else if (GameManager.instance.GetGameState() == GameState.myRank)
        {
            leftFloorTile.SetActive(false);
            centerFloorTile.SetActive(false);
            rightFloorTile.SetActive(false);
            
            centerTile.SetActive(true);

            newGameTilePause.SetActive(false);
            toMenuTilePause.SetActive(false);
            nextPageTile.SetActive(false);
            newGameTileResult.SetActive(true);
            toMenuTileResult.SetActive(true);
        }
    }

    // 발 위치 원 설정
    void HandleFootPrint()
    {
        HandleFootPrintPosition();
        HandleFootPrintSize();
    }

    // 바닥 스크린들의 밟은 판정   
    void HandleFloorTiles()
    {
        HandleFloorTile(Player.highlight, leftFloorTile, ConstInfo.left);
        HandleFloorTile(Player.highlight, centerFloorTile, ConstInfo.center);
        HandleFloorTile(Player.highlight, rightFloorTile, ConstInfo.right);
    }

    // 발 위치 원 좌표 변경
    void HandleFootPrintPosition()
    {
        leftFootPrint.transform.position = new Vector3(Avatar.userPositionLeftFoot.x, 0, Avatar.userPositionLeftFoot.z);
        rightFootPrint.transform.position = new Vector3(Avatar.userPositionRightFoot.x, 0, Avatar.userPositionRightFoot.z);
    }

    // 발 위치 원 크기 변경
    void HandleFootPrintSize()
    {
        float newLeftFootPrintSize = Avatar.HandleFootprintSize(Avatar.userPositionLeftFoot.y);
        float newRightFootPrintSize = Avatar.HandleFootprintSize(Avatar.userPositionRightFoot.y);
        leftFootPrint.transform.localScale = new Vector3(newLeftFootPrintSize, ConstInfo.foorPrintScaleY, newLeftFootPrintSize);
        rightFootPrint.transform.localScale = new Vector3(newRightFootPrintSize, ConstInfo.foorPrintScaleY, newRightFootPrintSize);
    }

    // 두 발이 모두 타일 안에 있는 경우 하이라이트 위치 변경
    void HandleFloorTile(GameObject highlight, GameObject floorTile, float positionX)
    {
        if (Avatar.OnTile(floorTile))
            highlight.transform.position = new Vector3(positionX, highlight.transform.position.y, highlight.transform.position.z);
    }

    // 결음 기록 조건 만족 시 함수 호출
    void HandleSteps()
    {
        if (((stepSide == true && Avatar.userPositionLeftFoot.y > ConstInfo.stepCountY && Avatar.userPositionRightFoot.y < ConstInfo.stepCountY)
            || (stepSide == false && Avatar.userPositionLeftFoot.y < ConstInfo.stepCountY && Avatar.userPositionRightFoot.y > ConstInfo.stepCountY))
            && GameManager.instance.GetKinectState())
            HandleStep();
    }

    // 걸음시간 기록 및 초기화, 결음 방향 변경
    void HandleStep()
    {
        stepSide = !stepSide;
        steps.RemoveAt(0);
        steps.Add(10 / stepRecordTime);
        Tile.extraSpeed = steps.Average();
        stepRecordTime = 0;
    }

    // 점프 조건
    void HandleJump()
    {
        bool jump = true;
        for (int i = 0; i < left.Count - 1 ; i++) {
            if (left[i] >= left[i + 1] || right[i] >= right[i + 1])
                jump = false;
        }
        isJumping = jump;

        left.Add(Avatar.userPositionLeftFoot.y);
        right.Add(Avatar.userPositionRightFoot.y);
        left.RemoveAt(0);
        right.RemoveAt(0);
        Debug.Log("left: " + left);
        Debug.Log("right: " + right);

        // isJumping = Avatar.userPositionLeftFoot.y > ConstInfo.jumpConditionY && Avatar.userPositionRightFoot.y > ConstInfo.jumpConditionY;
        // CountJumpDelay();
    }

    void CountJumpDelay() {
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        if (isJumping == false)
        {
            if (Avatar.userPositionLeftFoot.y > 1.6 && Avatar.userPositionRightFoot.y > 1.6)
            {
                watch.Start();
            }
            else
                watch.Reset();
        }


        isJumping = Avatar.userPositionLeftFoot.y > ConstInfo.jumpConditionY && Avatar.userPositionRightFoot.y > ConstInfo.jumpConditionY;
        watch.Stop();
        Debug.Log("Jump delay time : " + watch.ElapsedMilliseconds + " ms");
        watch.Reset();
    }

    // 펀치 조건
    void HandleAvatarPunch()
    {
        if ((Avatar.userPositionLeftHand.z > Avatar.userPositionHead.z + ConstInfo.punchDistance)
            || (Avatar.userPositionRightHand.z > Avatar.userPositionHead.z + ConstInfo.punchDistance))
            isPunching = true;
        else
            isPunching = false;
    }

    // 게임 바닥타일과 유저의 상호작용
    void HandleGameTiles() {
        if (Avatar.OneFootOnCircleTile(newGameTilePause) && GameManager.instance.GetGameState() == GameState.pause)
            HandleNewGameTilePause();
        else
            uiTimer[0] = 0;

        if (Avatar.OneFootOnCircleTile(toMenuTilePause) && GameManager.instance.GetGameState() == GameState.pause)
            HandleToMenuTilePause();
        else
            uiTimer[1] = 0;

        if (Avatar.OneFootOnCircleTile(nextPageTile)&& GameManager.instance.GetGameState() == GameState.result)
            HandleNextPageTile();
        else
            uiTimer[2] = 0;

        if (Avatar.OneFootOnCircleTile(newGameTileResult) && GameManager.instance.GetGameState() == GameState.myRank)
            HandleNewGameTileResult();
        else
            uiTimer[3] = 0;

        if (Avatar.OneFootOnCircleTile(toMenuTileResult) && GameManager.instance.GetGameState() == GameState.myRank)
            HandleToMenuTileResult();
        else
            uiTimer[4] = 0;
    }

    // 일시정지와 다시시작 조건
    void HandlePause()
    {

        if (Avatar.GetUserValid())
        {
            if ((Avatar.userPositionLeftHand.y > Avatar.userPositionHead.y 
                && Avatar.userPositionRightHand.y > Avatar.userPositionHead.y)
                && Avatar.OnCircleTile(centerTile) && GameManager.instance.GetGameState() == GameState.pause)
                GameUI.instance.Pause();
        }
        else
        {
            if (GameManager.instance.GetGameState() == GameState.game)
                GameUI.instance.Pause();
        }

    }

    // 새 게임 버튼을 누른 경우
    void HandleNewGameTilePause() {
        uiTimer[0] += Time.deltaTime;
        if (uiTimer[0] > ConstInfo.pushTime) {
            uiTimer[0] = 0;
            GameUI.instance.HandleNewGame();
        }
    }

    void HandleNewGameTileResult()
    {
        uiTimer[3] += Time.deltaTime;
        if (uiTimer[3] > ConstInfo.pushTime)
        {
            uiTimer[3] = 0;
            MyRankUI.instance.HandleRetry();
        }
    }

    // 메뉴로 버튼을 누른 경우
    void HandleToMenuTilePause() {
        uiTimer[1] += Time.deltaTime;
        if (uiTimer[1] > ConstInfo.pushTime) {
            uiTimer[1] = 0;
            GameUI.instance.HandleToMenu();
        }
    }

    void HandleToMenuTileResult()
    {
        uiTimer[4] += Time.deltaTime;
        if (uiTimer[4] > ConstInfo.pushTime)
        {
            uiTimer[4] = 0;
            MyRankUI.instance.HandleToMenu();
        }
    }

    // 다음 페이지 버튼을 누른 경우
    void HandleNextPageTile() {
        uiTimer[2] += Time.deltaTime;
        if (uiTimer[2] > ConstInfo.pushTime)
        {
            uiTimer[2] = 0;
            ResultUI.instance.HandleNextPage();
        }
    }

    // 키보드 입력
    void HandleKeyboard()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && GameManager.instance.GetGameState() == GameState.game)
        {
            Player.player.transform.position = new Vector3(ConstInfo.left, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
            Player.highlight.transform.position = new Vector3(ConstInfo.left, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
        }
        if (Input.GetKey(KeyCode.DownArrow) && GameManager.instance.GetGameState() == GameState.game)
        {
            Player.player.transform.position = new Vector3(ConstInfo.center, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
            Player.highlight.transform.position = new Vector3(ConstInfo.center, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
        }
        if (Input.GetKey(KeyCode.RightArrow) && GameManager.instance.GetGameState() == GameState.game)
        {
            Player.player.transform.position = new Vector3(ConstInfo.right, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
            Player.highlight.transform.position = new Vector3(ConstInfo.right, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
        }
        if (Input.GetKey(KeyCode.LeftAlt))
            isJumping = true;
        if (Input.GetKey(KeyCode.LeftControl))
            Tile.extraSpeed += 0.1f;
        if (Input.GetKey(KeyCode.LeftShift))
            isPunching = true;


        if (Input.GetKeyDown(KeyCode.Backspace) && (GameManager.instance.GetGameState() == GameState.pause || GameManager.instance.GetGameState() == GameState.game))
            GameUI.instance.Pause();

            if (Input.GetKey(KeyCode.Alpha1) && GameManager.instance.GetGameState() == GameState.pause)
            GameUI.instance.HandleToMenu();
        if (Input.GetKey(KeyCode.Alpha2) && GameManager.instance.GetGameState() == GameState.pause)
            GameUI.instance.HandleNewGame();

        if (Input.GetKey(KeyCode.Return) && GameManager.instance.GetGameState() == GameState.result)
            ResultUI.instance.HandleNextPage();

        if (Input.GetKey(KeyCode.Alpha1) && GameManager.instance.GetGameState() == GameState.myRank)
            MyRankUI.instance.HandleToMenu();
        if (Input.GetKey(KeyCode.Alpha2) && GameManager.instance.GetGameState() == GameState.myRank)
            MyRankUI.instance.HandleRetry();


    }
}
