using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MenuFloorTile : MonoBehaviour
{

    // 바닥 UI 타일
    GameObject centerTile;
    GameObject upArrowTile;
    GameObject downArrowTile;
    GameObject leftArrowTile;
    GameObject rightArrowTile;
    GameObject confirmTile;
    GameObject cancelTile;

    // 발위치 원 변수
    public GameObject leftFootPrint;
    public GameObject rightFootPrint;
    public float footPrintLerpT;

    // 버튼 타이머 변수 - 0: up, 1: down, 2: left, 3: right, 4: confirm, 5: cancel
    public float[] uiTimer;
    public float buttonDelayTimer;

    void Start()
    {
        GameManager.instance.SetGameState(GameState.menu);
        InitialObjects();
        uiTimer = new float[6] { 0, 0, 0, 0, 0, 0 };
        buttonDelayTimer = 0;
        footPrintLerpT = 0;
    }

    // 메뉴 바닥 타일 오브젝트 설정
    void InitialObjects() {
        centerTile = GameObject.Find("CenterTile");

        upArrowTile = GameObject.Find("UpArrowTile");
        downArrowTile = GameObject.Find("DownArrowTile");
        leftArrowTile = GameObject.Find("LeftArrowTile");
        rightArrowTile = GameObject.Find("RightArrowTile");

        confirmTile = GameObject.Find("ConfirmTile");
        cancelTile = GameObject.Find("CancelTile");

        leftFootPrint = GameObject.Find("Footprint-left");
        rightFootPrint = GameObject.Find("Footprint-right");
    }

    void Update()
    {
        FloorTexture.MoveAllChildTexture(upArrowTile, true);
        HandleTileActive();
        if (GameManager.instance.GetKinectState()) {
            HandleFootPrint();
            HandleMenuTiles();
        }
        else
        {
            leftFootPrint.transform.localScale = Vector3.zero;
            rightFootPrint.transform.localScale = Vector3.zero;
        }
        HandleKeyBoard();
    }

    // 바닥 UI 타일 보여주기, 감추기
    void HandleTileActive()
    {
        if (GameManager.instance.GetGameState() == GameState.menu)
        {
            upArrowTile.SetActive(true);
            downArrowTile.SetActive(true);
            leftArrowTile.SetActive(false);
            rightArrowTile.SetActive(false);
            confirmTile.SetActive(true);
            cancelTile.SetActive(false);
        }
        else if (GameManager.instance.GetGameState() == GameState.ranking)
        {
            upArrowTile.SetActive(false);
            downArrowTile.SetActive(false);
            leftArrowTile.SetActive(false);
            rightArrowTile.SetActive(false);
            confirmTile.SetActive(false);
            cancelTile.SetActive(true);
        }
        else if (GameManager.instance.GetGameState() == GameState.setting)
        {
            upArrowTile.SetActive(true);
            downArrowTile.SetActive(true);
            leftArrowTile.SetActive(true);
            rightArrowTile.SetActive(true);
            confirmTile.SetActive(false);
            cancelTile.SetActive(true);
        }
    }

    // 발 위치 원 설정
    void HandleFootPrint()
    {
        HandleFootPrintPosition();
        HandleFootPrintSize();
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


    
    // 메뉴 바닥타일과 유저 간의 상호작용
    void HandleMenuTiles() {
        HandleCenterTile();

        if (buttonDelayTimer == 0)
        {
            if (Avatar.OneFootOverlaps(leftFootPrint, rightFootPrint, upArrowTile))
                HandleUpArrowTile();
            else
            {
                FloorTexture.setButtonTexture(upArrowTile, FloorTexture.UpArrowButton);
                uiTimer[0] = 0;
                FloorTexture.ProgressDelayTexture(upArrowTile, 0);
                FloorTexture.MoveAllChildTexture(upArrowTile, false);
            }

            if (Avatar.OneFootOverlaps(leftFootPrint, rightFootPrint, downArrowTile))
                HandleDownArrowTile();
            else
            {
                FloorTexture.setButtonTexture(downArrowTile, FloorTexture.DownArrowButton);
                uiTimer[1] = 0;
                FloorTexture.ProgressDelayTexture(downArrowTile, 0);
                FloorTexture.MoveAllChildTexture(downArrowTile, false);
            }
            if (Avatar.OneFootOverlaps(leftFootPrint, rightFootPrint, leftArrowTile))
                HandleLeftArrowTile();
            else
            {
                FloorTexture.setButtonTexture(leftArrowTile, FloorTexture.LeftArrowButton);
                uiTimer[2] = 0;
                FloorTexture.ProgressDelayTexture(leftArrowTile, 0);
                FloorTexture.MoveAllChildTexture(leftArrowTile, false);
            }
            if (Avatar.OneFootOverlaps(leftFootPrint, rightFootPrint, rightArrowTile))
                HandleRightArrowTile();
            else
            {
                FloorTexture.setButtonTexture(rightArrowTile, FloorTexture.RightArrowButton);
                uiTimer[3] = 0;
                FloorTexture.ProgressDelayTexture(rightArrowTile, 0);
                FloorTexture.MoveAllChildTexture(rightArrowTile, false);
            }
            if (Avatar.OneFootOverlaps(leftFootPrint, rightFootPrint, confirmTile))
                HandleConfirmTile();
            else
            {
                FloorTexture.setButtonTexture(confirmTile, FloorTexture.RightButton);
                uiTimer[4] = 0;
                FloorTexture.ProgressDelayTexture(confirmTile, 0);
                FloorTexture.MoveAllChildTexture(confirmTile, false);
            }
            if (Avatar.OneFootOverlaps(leftFootPrint, rightFootPrint, cancelTile))
                HandleCancelTile();
            else
            {
                FloorTexture.setButtonTexture(cancelTile, FloorTexture.LeftButton);
                uiTimer[5] = 0;
                FloorTexture.ProgressDelayTexture(cancelTile, 0);
                FloorTexture.MoveAllChildTexture(cancelTile, false);
            }
        }
        else {
            buttonDelayTimer = buttonDelayTimer - Time.deltaTime > 0 ? buttonDelayTimer - Time.deltaTime : 0;
        }
    }

    void HandleCenterTile() {
        if (Avatar.GetUserValid())
            if (Avatar.VectorInside(Avatar.userPositionLeftFoot, centerTile) && Avatar.VectorInside(Avatar.userPositionRightFoot, centerTile))
                FloorTexture.setButtonTexture(centerTile, FloorTexture.PositionButtonBlue);
            else
                FloorTexture.setButtonTexture(centerTile, FloorTexture.PositionButton);
        else
            FloorTexture.setButtonTexture(centerTile, FloorTexture.PositionDisabled);
    }



    // 위 방향을 누른 경우
    void HandleUpArrowTile() {
        FloorTexture.setButtonTexture(upArrowTile, FloorTexture.UpArrowButtonPress);
        uiTimer[0] += Time.deltaTime;
        FloorTexture.MoveAllChildTexture(upArrowTile, true);
        FloorTexture.ProgressDelayTexture(upArrowTile, uiTimer[0] / ConstInfo.buttonPushTime);
        if (uiTimer[0] > ConstInfo.buttonPushTime)
        {
            HandleUpArrow();
            uiTimer[0] = 0;
            buttonDelayTimer = ConstInfo.buttonDelayTime;
            FloorTexture.ProgressDelayTexture(upArrowTile, 0);
            FloorTexture.MoveAllChildTexture(upArrowTile, false);
        }
    }

    // 아래 방향을 누른 경우
    void HandleDownArrowTile() {
        FloorTexture.setButtonTexture(downArrowTile, FloorTexture.DownArrowButtonPress);
        uiTimer[1] += Time.deltaTime;
        FloorTexture.MoveAllChildTexture(downArrowTile, true);
        FloorTexture.ProgressDelayTexture(downArrowTile, uiTimer[1] / ConstInfo.buttonPushTime);
        if (uiTimer[1] > ConstInfo.buttonPushTime)
        {
            HandleDownArrow();
            uiTimer[1] = 0;
            buttonDelayTimer = ConstInfo.buttonDelayTime;
            FloorTexture.ProgressDelayTexture(downArrowTile, 0);
            FloorTexture.MoveAllChildTexture(downArrowTile, false);
        }
    }

    // 왼쪽 방향을 누른 경우
    void HandleLeftArrowTile()
    {
        FloorTexture.setButtonTexture(leftArrowTile, FloorTexture.LeftArrowButtonPress);
        uiTimer[2] += Time.deltaTime;
        FloorTexture.MoveAllChildTexture(leftArrowTile, true);
        FloorTexture.ProgressDelayTexture(leftArrowTile, uiTimer[2] / ConstInfo.buttonPushTime);
        if (uiTimer[2] > ConstInfo.buttonPushTime)
        {
            SettingUI.instance.HandleLeft();
            uiTimer[2] = 0;
            buttonDelayTimer = ConstInfo.buttonDelayTime;
            FloorTexture.ProgressDelayTexture(leftArrowTile, 0);
            FloorTexture.MoveAllChildTexture(leftArrowTile, false);
        }
    }

    // 오른쪽 방향을 누른 경우
    void HandleRightArrowTile()
    {
        FloorTexture.setButtonTexture(rightArrowTile, FloorTexture.RightArrowButtonPress);
        uiTimer[3] += Time.deltaTime;
        FloorTexture.MoveAllChildTexture(rightArrowTile, true);
        FloorTexture.ProgressDelayTexture(rightArrowTile, uiTimer[3] / ConstInfo.buttonPushTime);
        if (uiTimer[3] > ConstInfo.buttonPushTime)
        {
            SettingUI.instance.HandleRight();
            uiTimer[3] = 0;
            buttonDelayTimer = ConstInfo.buttonDelayTime;
            FloorTexture.ProgressDelayTexture(rightArrowTile, 0);
            FloorTexture.MoveAllChildTexture(rightArrowTile, false);
        }
    }

    // 확인을 누른 경우
    void HandleConfirmTile() {
        FloorTexture.setButtonTexture(confirmTile, FloorTexture.RightButtonPress);
        uiTimer[4] += Time.deltaTime;
        FloorTexture.MoveAllChildTexture(confirmTile, true);
        FloorTexture.ProgressDelayTexture(confirmTile, uiTimer[4] / ConstInfo.buttonPushTime);
        if (uiTimer[4] > ConstInfo.buttonPushTime)
        {
            MenuUI.instance.HandleConfirm();
            uiTimer[4] = 0;
            buttonDelayTimer = ConstInfo.buttonDelayTime;
            FloorTexture.ProgressDelayTexture(confirmTile, 0);
            FloorTexture.MoveAllChildTexture(confirmTile, false);
        }
    }

    // 취소를 누른 경우
    void HandleCancelTile() {
        FloorTexture.setButtonTexture(cancelTile, FloorTexture.LeftButtonPress);
        uiTimer[5] += Time.deltaTime;
        FloorTexture.MoveAllChildTexture(cancelTile, true);
        FloorTexture.ProgressDelayTexture(cancelTile, uiTimer[5] / ConstInfo.buttonPushTime);
        if (uiTimer[5] > ConstInfo.buttonPushTime)
        {
            HandleCancel();
            uiTimer[5] = 0;
            buttonDelayTimer = ConstInfo.buttonDelayTime;
            FloorTexture.ProgressDelayTexture(cancelTile, 0);
            FloorTexture.MoveAllChildTexture(cancelTile, false);
        }
    }



    // 게임 상태에 따른 위 방향키 동작구분
    void HandleUpArrow() {
        if (GameManager.instance.GetGameState() == GameState.menu)
            MenuUI.instance.HandleUp();
        else if (GameManager.instance.GetGameState() == GameState.setting)
            SettingUI.instance.HandleUp();
    }

    // 게임 상태에 따른 아래 방향키 동작구분
    void HandleDownArrow()
    {
        if (GameManager.instance.GetGameState() == GameState.menu)
            MenuUI.instance.HandleDown();
        else if (GameManager.instance.GetGameState() == GameState.setting)
            SettingUI.instance.HandleDown();
    }

    // 게임 상태에 따른 취소 동작구분
    void HandleCancel() {
        if (GameManager.instance.GetGameState() == GameState.ranking)
            RankingUI.instance.HandleCancel();
        else if (GameManager.instance.GetGameState() == GameState.setting)
            SettingUI.instance.HandleCancel();
    }



    // 키보드 조작
    void HandleKeyBoard() {
        if (Input.GetKeyDown(KeyCode.UpArrow) && (GameManager.instance.GetGameState() == GameState.menu || GameManager.instance.GetGameState() == GameState.setting))
            HandleUpArrow();
        if (Input.GetKeyDown(KeyCode.DownArrow) && (GameManager.instance.GetGameState() == GameState.menu || GameManager.instance.GetGameState() == GameState.setting))
            HandleDownArrow();
        if (Input.GetKeyDown(KeyCode.LeftArrow) && GameManager.instance.GetGameState() == GameState.setting)
            SettingUI.instance.HandleLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow) && GameManager.instance.GetGameState() == GameState.setting)
            SettingUI.instance.HandleRight();
        if (Input.GetKeyDown(KeyCode.Return) && GameManager.instance.GetGameState() == GameState.menu)
            MenuUI.instance.HandleConfirm();
        if (Input.GetKeyDown(KeyCode.Backspace) && (GameManager.instance.GetGameState() == GameState.ranking || GameManager.instance.GetGameState() == GameState.setting))
            HandleCancel();
    }
}
