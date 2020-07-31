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

    public const float pushTime = 1;
    public const float footPrintStartSize = 0.7f;
    public const float foorPrintScaleY = 0.005f;

    // 버튼 타이머 변수 - 0: up, 1: down, 2: confirm, 3: cancel
    public float[] uiTimer;
    
    // Start is called before the first frame update
    void Start()
    {
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
        if (GameManager.instance.currentGameState == GameState.menu)
        {
            upArrowTile.SetActive(true);
            downArrowTile.SetActive(true);
            confirmTile.SetActive(true);
            cancelTile.SetActive(false);
        }
        else if (GameManager.instance.currentGameState == GameState.ranking) {
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
        float newLeftFootPrintSize = Avatar.userPositionLeftFoot.y < 2 ? 1 : 0;
        float newRightFootPrintSize = Avatar.userPositionRightFoot.y < 2 ? 1 : 0;
        leftFootPrint.transform.localScale = new Vector3(newLeftFootPrintSize, foorPrintScaleY, newLeftFootPrintSize);
        rightFootPrint.transform.localScale = new Vector3(newRightFootPrintSize, foorPrintScaleY, newRightFootPrintSize);
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
        if (uiTimer[0] > pushTime)
            HandleUpArrow();
    }

    void HandleDownArrowTile() {
        uiTimer[1] += Time.deltaTime;
        if(uiTimer[1] > pushTime)
            HandleDownArrow();
    }

    void HandleConfirmTile() {
        uiTimer[2] += Time.deltaTime;
        if (uiTimer[2] > pushTime)
            HandleConfirm();
    }

    void HandleCancelTile() {
        uiTimer[3] += Time.deltaTime;
        if (uiTimer[3] > pushTime)
            HandleCancel();
    }

    void HandleUpArrow() {
        Debug.Log("Left Arrow !");
        UIgameStart.instance.HandleUp();
        uiTimer[0] = 0;
    }

    void HandleDownArrow() {
        Debug.Log("Right Arrow !");
        UIgameStart.instance.HandleDown();
        uiTimer[1] = 0;
    }

    void HandleConfirm() {
        Debug.Log("Confirm !");
        UIgameStart.instance.HandleConfirm();
        uiTimer[2] = 0;
    }

    void HandleCancel() {
        Debug.Log("Cancel !");
        UIranking.instance.HandleCancel();
        uiTimer[3] = 0;
    }


}
