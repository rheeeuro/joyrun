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

    public float buttonTimer;
    public const float pushTime = 0.5f;
    public const float footPrintStartSize = 0.7f;
    public const float foorPrintScaleY = 0.005f;

    public bool leftArrowState;
    public bool rightArrowState;
    public bool confirmState;
    public bool cancelState;
    
    // Start is called before the first frame update
    void Start()
    {
        leftArrowTile = GameObject.Find("leftArrowTile");
        rightArrowTile = GameObject.Find("rightArrowTile");
        confirmTile = GameObject.Find("confirmTile");
        cancelTile = GameObject.Find("cancelTile");

        InitialState();
        
    }

    void InitialState()
    {
        leftArrowState = false;
        rightArrowState = false;
        confirmState = false;
        cancelState = false;
        buttonTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 위치 업데이트
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
        float newLeftFootPrintSize = footPrintStartSize / Avatar.userPositionLeftFoot.y;
        float newRightFootPrintSize = footPrintStartSize / Avatar.userPositionRightFoot.y;
        leftFootPrint.transform.localScale = new Vector3(newLeftFootPrintSize, foorPrintScaleY, newLeftFootPrintSize);
        rightFootPrint.transform.localScale = new Vector3(newRightFootPrintSize, foorPrintScaleY, newRightFootPrintSize);
    }

    void HandleMenuTiles() {
        Debug.Log("timer" + buttonTimer);

        if (Avatar.OneFootOnCircleTile(leftArrowTile))
            HandleLeftArrowTile();
        else if (Avatar.OneFootOnCircleTile(rightArrowTile))
            HandleRightArrowTile();
        else if (Avatar.OneFootOnCircleTile(confirmTile))
            HandleConfirmTile();
        else if (Avatar.OneFootOnCircleTile(cancelTile))
            HandleCancelTile();
        else
            InitialState();
    }



    void HandleLeftArrowTile() {
        Debug.Log("left pushing!");
        if (leftArrowState)
            buttonTimer += Time.deltaTime;
        else
            InitialState();
        if (buttonTimer > pushTime)
            HandleLeftArrow();
    }

    void HandleRightArrowTile() {
        Debug.Log("right pushing!");
        if (rightArrowState)
            buttonTimer += Time.deltaTime;
        else
            InitialState();
        if (buttonTimer > pushTime)
            HandleRightArrow();
    }

    void HandleConfirmTile() {
        Debug.Log("confirm pushing!");
        if (confirmState)
            buttonTimer += Time.deltaTime;
        else
            InitialState();
        if (buttonTimer > pushTime)
            HandleConfirm();
    }

    void HandleCancelTile() {
        Debug.Log("cancel pushing!");
        if (cancelState)
            buttonTimer += Time.deltaTime;
        else
            InitialState();
        if (buttonTimer > pushTime)
            HandleCancel();
    }

    void HandleLeftArrow() {
        Debug.Log("Left Arrow !");
        InitialState();
    }

    void HandleRightArrow() {
        Debug.Log("Right Arrow !");
        InitialState();
    }

    void HandleConfirm() {
        Debug.Log("Confirm !");
        InitialState();
    }

    void HandleCancel() {
        Debug.Log("Cancel !");
        InitialState();
    }


}
