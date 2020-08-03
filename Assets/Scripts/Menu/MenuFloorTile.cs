using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFloorTile : MonoBehaviour
{
    // 바닥 UI 타일
    GameObject upArrowTile;
    GameObject downArrowTile;
    GameObject confirmTile;
    GameObject cancelTile;

    // 발위치 원 변수
    public GameObject leftFootPrint;
    public GameObject rightFootPrint;

    // 버튼 타이머 변수 - 0: up, 1: down, 2: confirm, 3: cancel
    private float[] uiTimer;

    void Start()
    {
        GameManager.instance.Menu();
        upArrowTile = GameObject.Find("UpArrowTile");
        downArrowTile = GameObject.Find("DownArrowTile");
        confirmTile = GameObject.Find("ConfirmTile");
        cancelTile = GameObject.Find("CancelTile");

        uiTimer = new float[4] { 0, 0, 0, 0 }; 
    }


    // Update is called once per frame
    void Update()
    {
        HandleTileActive();
        HandleFootPrint();
        HandleMenuTiles();    
    }

    // 바닥 UI 타일 보여주기, 감추기
    void HandleTileActive() {
        if (GameManager.instance.GetGameState() == GameState.menu)
        {
            upArrowTile.SetActive(true);
            downArrowTile.SetActive(true);
            confirmTile.SetActive(true);
            cancelTile.SetActive(false);
        }
        else if (GameManager.instance.GetGameState() == GameState.ranking) {
            upArrowTile.SetActive(false);
            downArrowTile.SetActive(false);
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
        if (Avatar.OneFootOnCircleTile(upArrowTile))
            HandleUpArrowTile();
        else
            uiTimer[0] = 0;

        if (Avatar.OneFootOnCircleTile(downArrowTile))
            HandleDownArrowTile();
        else
            uiTimer[1] = 0;

        if (Avatar.OneFootOnCircleTile(confirmTile))
            HandleConfirmTile();
        else
            uiTimer[2] = 0;

        if (Avatar.OneFootOnCircleTile(cancelTile))
            HandleCancelTile();
        else
            uiTimer[3] = 0;
    }



    void HandleUpArrowTile() {
        uiTimer[0] += Time.deltaTime;
        if (uiTimer[0] > ConstInfo.pushTime)
            HandleUpArrow();
    }

    void HandleDownArrowTile() {
        uiTimer[1] += Time.deltaTime;
        if(uiTimer[1] > ConstInfo.pushTime)
            HandleDownArrow();
    }

    void HandleConfirmTile() {
        uiTimer[2] += Time.deltaTime;
        if (uiTimer[2] > ConstInfo.pushTime)
            HandleConfirm();
    }

    void HandleCancelTile() {
        uiTimer[3] += Time.deltaTime;
        if (uiTimer[3] > ConstInfo.pushTime)
            HandleCancel();
    }

    void HandleUpArrow() {
        MenuUI.instance.HandleUp();
        uiTimer[0] = 0;
    }

    void HandleDownArrow() {
        MenuUI.instance.HandleDown();
        uiTimer[1] = 0;
    }

    void HandleConfirm() {
        MenuUI.instance.HandleConfirm();
        uiTimer[2] = 0;
    }

    void HandleCancel() {
        RankingUI.instance.HandleCancel();
        uiTimer[3] = 0;
    }


}
