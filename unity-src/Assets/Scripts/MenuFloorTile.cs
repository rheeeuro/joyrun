using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFloorTile : MonoBehaviour
{
    // 바닥 UI 타일
    GameObject leftArrowTile;
    GameObject rightArrowTile;
    GameObject confirmTile;
    GameObject cancelTile;

    // 발위치 원 변수
    public GameObject leftFootPrint;
    public GameObject rightFootPrint;

    public const float pushTime = 0.5f;
    public const float footPrintStartSize = 0.7f;
    public const float foorPrintScaleY = 0.005f;

    // 버튼 타이머 변수 - 0: leftArrow, 1: rightArrow, 2: confirm, 3: cancel
    public float[] menuTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        leftArrowTile = GameObject.Find("leftArrowTile");
        rightArrowTile = GameObject.Find("rightArrowTile");
        confirmTile = GameObject.Find("confirmTile");
        cancelTile = GameObject.Find("cancelTile");

        menuTimer = new float[4] { 0, 0, 0, 0 }; 
        
    }


    // Update is called once per frame
    void Update()
    {
        HandleFootPrint();
        HandleMenuTiles();    
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
        if (Avatar.OneFootOnCircleTile(leftArrowTile))
            HandleLeftArrowTile();
        else
            menuTimer[0] = 0;

        if (Avatar.OneFootOnCircleTile(rightArrowTile))
            HandleRightArrowTile();
        else
            menuTimer[1] = 0;

        if (Avatar.OneFootOnCircleTile(confirmTile))
            HandleConfirmTile();
        else
            menuTimer[2] = 0;

        if (Avatar.OneFootOnCircleTile(cancelTile))
            HandleCancelTile();
        else
            menuTimer[3] = 0;
    }



    void HandleLeftArrowTile() {
        menuTimer[0] += Time.deltaTime;
        if (menuTimer[0] > pushTime)
            HandleLeftArrow();
    }

    void HandleRightArrowTile() {
        menuTimer[1] += Time.deltaTime;
        if(menuTimer[1] > pushTime)
            HandleRightArrow();
    }

    void HandleConfirmTile() {
        menuTimer[2] += Time.deltaTime;
        if (menuTimer[2] > pushTime)
            HandleConfirm();
    }

    void HandleCancelTile() {
        menuTimer[3] += Time.deltaTime;
        if (menuTimer[3] > pushTime)
            HandleCancel();
    }

    void HandleLeftArrow() {
        Debug.Log("Left Arrow !");
        menuTimer[0] = 0;
    }

    void HandleRightArrow() {
        Debug.Log("Right Arrow !");
        menuTimer[1] = 0;
    }

    void HandleConfirm() {
        Debug.Log("Confirm !");
        menuTimer[2] = 0;
    }

    void HandleCancel() {
        Debug.Log("Cancel !");
        menuTimer[3] = 0;
    }


}
