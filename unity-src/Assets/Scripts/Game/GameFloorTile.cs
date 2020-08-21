﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;

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
    public float footPrintLerpT;

    // 유저 상태 변수
    public static bool isJumping;
    public static bool isPunching;
    public static bool stepSide;
    public static float stepRecordTime;
    public static float decreaseSpeedTimer;
    public static List<float> steps;

    public static Vector3 lastPositionLeftFoot;
    public static Vector3 lastPositionRightFoot;
    public static Vector3 lastPositionHead;

    // 버튼 타이머 변수 - 0: newgame pause, 1: to menu pause, 2: next page, 3: new game result, 4: to menu result
    public float[] uiTimer;
    public Text timerText;
    public GameObject timerBox;

    public UIBarScript barHp;
    public GameObject barHpBox;

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

        timerBox = GameObject.Find("TimerBox");
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
        decreaseSpeedTimer = 0;
        InitialStepRecords();

        // UI 타이머 배열 초기화
        uiTimer = new float[5] { 0, 0, 0, 0, 0 };

        footPrintLerpT = 0;

        lastPositionLeftFoot = Vector3.zero;
        lastPositionRightFoot = Vector3.zero;
        lastPositionHead = Vector3.zero;
    }

    // 걸음 시간 리스트 초기화 (0)
    public static void InitialStepRecords()
    {
        steps = Enumerable.Repeat<float>(0, 3).ToList<float>();
    }

    // 걸음 시간 측정 (+ fixedDeltaTime)
    private void FixedUpdate()
    {
        stepRecordTime += Time.fixedDeltaTime;
        decreaseSpeedTimer += Time.fixedDeltaTime;
    }
    
    void Update()
    {
        if(GameManager.instance.GetGameState() == GameState.game)
            HandleFloorUI();
        HandleTileActive();
        HandleKeyboard();
        if (GameManager.instance.GetKinectState())
            HandleKinect();
    }
    void HandleFloorUI(){
        if (GameUI.instance.timerText && timerBox.activeSelf)
            timerText.text = GameUI.instance.timerText.text.ToString();
        if (GameUI.instance.barHp && barHpBox.activeSelf)
            barHp.UpdateValue(Player.instance.hp, ConstInfo.maxHp);
    }

    void HandleKinect() {
        HandleFootPrint();
        HandleUserAction();
        HandleGameTiles();
        HandlePause();
        if (GameManager.instance.GetGameState() == GameState.game)
            HandleHighlight();

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

            timerBox.SetActive(true);
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

            timerBox.SetActive(true);
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

            timerBox.SetActive(false);
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

            timerBox.SetActive(false);
        }
    }



    // 발 위치 원 설정
    void HandleFootPrint()
    {
        HandleFootPrintPosition();
        HandleFootPrintSize();
    }

    // 바닥 스크린들의 밟은 판정   
    void HandleHighlight()
    {
        HandleHighlightPosition(Player.instance.highlight, leftFloorTile, ConstInfo.left);
        HandleHighlightPosition(Player.instance.highlight, centerFloorTile, ConstInfo.center);
        HandleHighlightPosition(Player.instance.highlight, rightFloorTile, ConstInfo.right);
    }

    // 두 발이 모두 타일 안에 있는 경우 하이라이트 위치 변경
    void HandleHighlightPosition(GameObject highlight, GameObject floorTile, float positionX)
    {
        if (Avatar.VectorInside(Avatar.userPosition, floorTile))
            highlight.transform.position = new Vector3(positionX, highlight.transform.position.y, highlight.transform.position.z);
    }

    // 발 위치 원 좌표 변경
    void HandleFootPrintPosition()
    {
        leftFootPrint.transform.localPosition = 
            Vector3.Lerp(leftFootPrint.transform.localPosition, new Vector3(Avatar.userPositionLeftFoot.x, Avatar.userPositionLeftFoot.z, 0), footPrintLerpT);
        rightFootPrint.transform.localPosition = 
            Vector3.Lerp(rightFootPrint.transform.localPosition, new Vector3(Avatar.userPositionRightFoot.x, Avatar.userPositionRightFoot.z, 0), footPrintLerpT);
        footPrintLerpT += ConstInfo.footPrintSpeed * Time.deltaTime;
        if (footPrintLerpT > 1.0f)
            footPrintLerpT = 0.0f;
    }

    // 발 위치 원 크기 변경
    void HandleFootPrintSize()
    {
        float newLeftFootPrintSize = Avatar.HandleFootprintSize(Avatar.userPositionLeftFoot.y);
        float newRightFootPrintSize = Avatar.HandleFootprintSize(Avatar.userPositionRightFoot.y);
        leftFootPrint.transform.localScale = new Vector3(newLeftFootPrintSize, newLeftFootPrintSize, newLeftFootPrintSize);
        rightFootPrint.transform.localScale = new Vector3(newRightFootPrintSize, newRightFootPrintSize, newRightFootPrintSize);
    }



    // 점프, 걸음, 펀치 판정
    void HandleUserAction() {
        HandleJump(); // 점프 판정 (버전 확인)
        HandleSteps();
        HandleAvatarPunch();
    }

    // 결음 기록 조건 만족 시 함수 호출
    void HandleSteps()
    {
        if (decreaseSpeedTimer >= 2 && !Player.instance.isJumping) {
            decreaseSpeedTimer = 0;
            steps.Add(0);
            steps.RemoveAt(0);
        }
            
        if (((stepSide == true && Avatar.userPositionLeftFoot.y > ConstInfo.stepHeight  && Avatar.userPositionRightFoot.y < ConstInfo.stepHeight)
            || (stepSide == false && Avatar.userPositionRightFoot.y > ConstInfo.stepHeight && Avatar.userPositionLeftFoot.y < ConstInfo.stepHeight))
            && stepRecordTime != 0)
            HandleStep();

        Tile.extraSpeed = steps.Average();
    }

    // 걸음시간 기록 및 초기화, 결음 방향 변경
    void HandleStep()
    {
        stepSide = !stepSide;
        steps.Add(10 / stepRecordTime);
        steps.RemoveAt(0);
        stepRecordTime = 0;
        decreaseSpeedTimer = 0;
    }

    // 점프 조건 (이전 프레임보다 양 발 모두 jumpHeight 이상 증가 + 발높이 차가 0.3 이하 + 양발의 x변화량이 5 미만)
    void HandleJump() {
        isJumping = lastPositionLeftFoot.y + ConstInfo.jumpHeight < Avatar.userPositionLeftFoot.y
            && lastPositionRightFoot.y + ConstInfo.jumpHeight < Avatar.userPositionRightFoot.y
            && lastPositionHead.y + ConstInfo.jumpHeight < Avatar.userPositionHead.y
            && lastPositionLeftFoot.y != 0 && lastPositionRightFoot.y != 0
            && Mathf.Abs(lastPositionLeftFoot.y - lastPositionRightFoot.y) < ConstInfo.jumpYLimitBetweenFoots
            && Mathf.Abs(lastPositionLeftFoot.x - Avatar.userPositionLeftFoot.x) < ConstInfo.jumpXChangeLimit
            && Mathf.Abs(lastPositionRightFoot.x - Avatar.userPositionRightFoot.x) < ConstInfo.jumpXChangeLimit;
        lastPositionLeftFoot = Avatar.userPositionLeftFoot;
        lastPositionRightFoot = Avatar.userPositionRightFoot;
    }

    // 펀치 조건
    void HandleAvatarPunch()
    {
        if ((Avatar.userPositionLeftHand.z > Avatar.userPositionHead.z + Avatar.DistanceBetweenHandAndElbow)
            || (Avatar.userPositionRightHand.z > Avatar.userPositionHead.z + Avatar.DistanceBetweenHandAndElbow))
            isPunching = true;
        else
            isPunching = false;
    }



    // 게임 바닥타일과 유저의 상호작용
    void HandleGameTiles() {
        HandleCenterTile();

        if (Avatar.OneFootOverlaps(leftFootPrint, rightFootPrint, newGameTilePause))
            HandleNewGameTilePause();
        else
        {
            FloorTexture.setButtonTexture(newGameTilePause, FloorTexture.HomeButton);
            uiTimer[0] = 0;
        }

        if (Avatar.OneFootOverlaps(leftFootPrint, rightFootPrint, toMenuTilePause))
            HandleToMenuTilePause();
        else
        {
            FloorTexture.setButtonTexture(toMenuTilePause, FloorTexture.BackButton);
            uiTimer[1] = 0;
        }

        if (Avatar.OneFootOverlaps(leftFootPrint, rightFootPrint, nextPageTile))
            HandleNextPageTile();
        else
        {
            FloorTexture.setButtonTexture(nextPageTile, FloorTexture.RemeasurementButton);
            uiTimer[2] = 0;
        }

        if (Avatar.OneFootOverlaps(leftFootPrint, rightFootPrint, newGameTileResult))
            HandleNewGameTileResult();
        else
        {
            FloorTexture.setButtonTexture(newGameTileResult, FloorTexture.RightButton);
            uiTimer[3] = 0;
        }

        if (Avatar.OneFootOverlaps(leftFootPrint, rightFootPrint, toMenuTileResult))
            HandleToMenuTileResult();
        else
        {
            FloorTexture.setButtonTexture(toMenuTileResult, FloorTexture.LeftButton);
            uiTimer[4] = 0;
        }
    }

    void HandleCenterTile()
    {
        if (Avatar.GetUserValid())
            if (Avatar.TwoFootOverlaps(leftFootPrint, rightFootPrint, centerTile))
                FloorTexture.setButtonTexture(centerTile, FloorTexture.PositionButtonBlue);
            else
                FloorTexture.setButtonTexture(centerTile, FloorTexture.PositionButton);
        else
            FloorTexture.setButtonTexture(centerTile, FloorTexture.PositionDisabled);
    }



    // 일시정지의 새 게임 버튼을 누른 경우
    void HandleNewGameTilePause() {
        FloorTexture.setButtonTexture(newGameTilePause, FloorTexture.HomeButtonPress);
        uiTimer[0] += Time.deltaTime;
        if (uiTimer[0] > ConstInfo.buttonPushTime) {
            uiTimer[0] = 0;
            GameUI.instance.HandleNewGame();
        }
    }

    // 게임결과의 다시하기 버튼을 누른 경우 (내 점수 화면)
    void HandleNewGameTileResult()
    {
        FloorTexture.setButtonTexture(newGameTileResult, FloorTexture.RightButtonPress);
        uiTimer[3] += Time.deltaTime;
        if (uiTimer[3] > ConstInfo.buttonPushTime)
        {
            uiTimer[3] = 0;
            MyRankUI.instance.HandleRetry();
        }
    }

    // 일시정지의 메뉴 버튼을 누른 경우
    void HandleToMenuTilePause() {
        FloorTexture.setButtonTexture(toMenuTilePause, FloorTexture.BackButtonPress);
        uiTimer[1] += Time.deltaTime;
        if (uiTimer[1] > ConstInfo.buttonPushTime) {
            uiTimer[1] = 0;
            GameUI.instance.HandleToMenu();
        }
    }

    // 게임결과의 메뉴버튼을 누른경우 (내 점수 화면)
    void HandleToMenuTileResult()
    {
        FloorTexture.setButtonTexture(toMenuTileResult, FloorTexture.LeftButtonPress);
        uiTimer[4] += Time.deltaTime;
        if (uiTimer[4] > ConstInfo.buttonPushTime)
        {
            uiTimer[4] = 0;
            MyRankUI.instance.HandleToMenu();
        }
    }

    // 다음 페이지 버튼을 누른 경우 (결과 화면)
    void HandleNextPageTile() {
        FloorTexture.setButtonTexture(nextPageTile, FloorTexture.RemeasurementButtonPress);
        uiTimer[2] += Time.deltaTime;
        if (uiTimer[2] > ConstInfo.buttonPushTime)
        {
            uiTimer[2] = 0;
            ResultUI.instance.HandleNextPage();
        }
    }



    // 일시정지와 다시시작 조건
    void HandlePause()
    {

        if (Avatar.GetUserValid())
        {
            if ((Avatar.userPositionLeftHand.y > Avatar.userPositionHead.y
                && Avatar.userPositionRightHand.y > Avatar.userPositionHead.y)
                && Avatar.TwoFootOverlaps(leftFootPrint, rightFootPrint, centerTile) && GameManager.instance.GetGameState() == GameState.pause)
                GameUI.instance.Pause();
        }
        else
        {
            if (GameManager.instance.GetGameState() == GameState.game)
                GameUI.instance.Pause();
        }

    }



    // 키보드 입력
    void HandleKeyboard()
    {
        if (GameManager.instance.GetGameState() == GameState.game)
            HandleKeyboardGame();
        if (GameManager.instance.GetGameState() == GameState.pause)
            HandleKeyboardPause();
        if (GameManager.instance.GetGameState() == GameState.result)
            HandleKeyboardResult();
        if (GameManager.instance.GetGameState() == GameState.myRank)
            HandleKeyboardMyRank();

        if (Input.GetKeyDown(KeyCode.Backspace) 
            && (GameManager.instance.GetGameState() == GameState.pause || GameManager.instance.GetGameState() == GameState.game))
            GameUI.instance.Pause();
    }

    // 게임 화면의 키보드 상호작용
    void HandleKeyboardGame() {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Player.instance.player.transform.position = new Vector3(ConstInfo.left, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
            Player.instance.highlight.transform.position = new Vector3(ConstInfo.left, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Player.instance.player.transform.position = new Vector3(ConstInfo.center, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
            Player.instance.highlight.transform.position = new Vector3(ConstInfo.center, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Player.instance.player.transform.position = new Vector3(ConstInfo.right, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
            Player.instance.highlight.transform.position = new Vector3(ConstInfo.right, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
        }

        if (Input.GetKey(KeyCode.LeftAlt))
            isJumping = true;
        else if (!GameManager.instance.GetKinectState())
            isJumping = false;

        if (Input.GetKey(KeyCode.LeftControl))
            Tile.extraSpeed += ConstInfo.extraSpeedIncrease;
        if (Input.GetKey(KeyCode.LeftShift))
            isPunching = true;
    }

    // 일시정지 상태의 키보드 상호작용
    void HandleKeyboardPause() {
        if (Input.GetKey(KeyCode.Alpha1))
            GameUI.instance.HandleToMenu();
        if (Input.GetKey(KeyCode.Alpha2))
            GameUI.instance.HandleNewGame();
    }

    // 결과 상태의 키보드 상호작용
    void HandleKeyboardResult()
    {
        if (Input.GetKey(KeyCode.Return) && GameManager.instance.GetGameState() == GameState.result)
            ResultUI.instance.HandleNextPage();
    }

    // 내 점수 상태의 키보드 상호작용
    void HandleKeyboardMyRank()
    {
        if (Input.GetKey(KeyCode.Alpha1) && GameManager.instance.GetGameState() == GameState.myRank)
            MyRankUI.instance.HandleToMenu();
        if (Input.GetKey(KeyCode.Alpha2) && GameManager.instance.GetGameState() == GameState.myRank)
            MyRankUI.instance.HandleRetry();
    }
}
