using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFloorTile : MonoBehaviour
{
    // 바닥 UI 타일
    GameObject upArrowTile;
    GameObject downArrowTile;
    GameObject leftArrowTile;
    GameObject rightArrowTile;
    GameObject confirmTile;
    GameObject cancelTile;

    // 발위치 원 변수
    public GameObject leftFootPrint;
    public GameObject rightFootPrint;

    // 버튼 타이머 변수 - 0: up, 1: down, 2: left, 3: right, 4: confirm, 5: cancel
    public float[] uiTimer;

    void Start()
    {
        GameManager.instance.SetGameState(GameState.menu);
        InitialObjects();
        uiTimer = new float[6] { 0, 0, 0, 0, 0, 0 }; 
    }

    void InitialObjects() {
        upArrowTile = GameObject.Find("UpArrowTile");
        downArrowTile = GameObject.Find("DownArrowTile");
        leftArrowTile = GameObject.Find("LeftArrowTile");
        rightArrowTile = GameObject.Find("RightArrowTile");

        confirmTile = GameObject.Find("ConfirmTile");
        cancelTile = GameObject.Find("CancelTile");

        leftFootPrint = GameObject.Find("Footprint-left");
        rightFootPrint = GameObject.Find("Footprint-right");
    }


    // Update is called once per frame
    void Update()
    {
        HandleTileActive();
        HandleFootPrint();
        HandleMenuTiles();
        HandleKeyBoard();
    }

    // 바닥 UI 타일 보여주기, 감추기
    void HandleTileActive() {
        if (GameManager.instance.GetGameState() == GameState.menu)
        {
            upArrowTile.SetActive(true);
            downArrowTile.SetActive(true);
            leftArrowTile.SetActive(false);
            rightArrowTile.SetActive(false);
            confirmTile.SetActive(true);
            cancelTile.SetActive(false);
        }
        else if (GameManager.instance.GetGameState() == GameState.ranking) {
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

    void HandleMenuTiles() {
        if (Avatar.OneFootOnCircleTile(upArrowTile) && GameManager.instance.GetGameState() == GameState.menu)
            HandleUpArrowTile();
        else
            uiTimer[0] = 0;

        if (Avatar.OneFootOnCircleTile(downArrowTile) && GameManager.instance.GetGameState() == GameState.menu)
            HandleDownArrowTile();
        else
            uiTimer[1] = 0;

        if (Avatar.OneFootOnCircleTile(leftArrowTile) && GameManager.instance.GetGameState() == GameState.setting)
            HandleLeftArrowTile();
        else
            uiTimer[2] = 0;

        if (Avatar.OneFootOnCircleTile(rightArrowTile) && GameManager.instance.GetGameState() == GameState.setting)
            HandleRightArrowTile();
        else
            uiTimer[3] = 0;

        if (Avatar.OneFootOnCircleTile(confirmTile) && GameManager.instance.GetGameState() == GameState.menu)
            HandleConfirmTile();
        else
            uiTimer[4] = 0;

        if (Avatar.OneFootOnCircleTile(cancelTile) && GameManager.instance.GetGameState() == GameState.ranking)
            HandleCancelTile();
        else
            uiTimer[5] = 0;
    }



    void HandleUpArrowTile() {
        uiTimer[0] += Time.deltaTime;
        if (uiTimer[0] > ConstInfo.pushTime)
        {
            HandleUpArrow();
            uiTimer[0] = 0;
        }
    }

    void HandleDownArrowTile() {
        uiTimer[1] += Time.deltaTime;
        if (uiTimer[1] > ConstInfo.pushTime)
        {
            HandleDownArrow();
            uiTimer[1] = 0;
        }
    }

    void HandleLeftArrowTile()
    {
        uiTimer[2] += Time.deltaTime;
        if (uiTimer[2] > ConstInfo.pushTime)
        {
            // handle left
            uiTimer[2] = 0;
        }
    }

    void HandleRightArrowTile()
    {
        uiTimer[3] += Time.deltaTime;
        if (uiTimer[3] > ConstInfo.pushTime)
        {
            // handle right
            uiTimer[3] = 0;
        }
    }

    void HandleConfirmTile() {
        uiTimer[4] += Time.deltaTime;
        if (uiTimer[4] > ConstInfo.pushTime)
        {
            MenuUI.instance.HandleConfirm();
            uiTimer[4] = 0;
        }
    }

    void HandleCancelTile() {
        uiTimer[5] += Time.deltaTime;
        if (uiTimer[5] > ConstInfo.pushTime)
        {
            
            uiTimer[5] = 0;
        }
    }

    void HandleUpArrow() {
        if (GameManager.instance.GetGameState() == GameState.menu)
            MenuUI.instance.HandleUp();
        else if (GameManager.instance.GetGameState() == GameState.setting)
            SettingUI.instance.HandleUp();
    }


    void HandleDownArrow()
    {
        if (GameManager.instance.GetGameState() == GameState.menu)
            MenuUI.instance.HandleDown();
        else if (GameManager.instance.GetGameState() == GameState.setting)
            SettingUI.instance.HandleDown();
    }

    void HandleCancel() {
        if (GameManager.instance.GetGameState() == GameState.ranking)
            RankingUI.instance.HandleCancel();
        else if (GameManager.instance.GetGameState() == GameState.setting)
            SettingUI.instance.HandleCancel();
    }


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
