using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    // 아바타 변수
    public static GameObject avatar;

    // 아바타 내부
    public GameObject leftFoot;
    public GameObject rightFoot;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject head;

    // 바닥 UI 타일
    public static GameObject leftFloorTile;
    public static GameObject centerFloorTile;
    public static GameObject rightFloorTile;
    public static GameObject resumeTile;

    // 바닥 타일 너비
    public const float floorTileScaleX = 0.8f;

    // 발위치 변수
    public GameObject leftFootPrint;
    public GameObject rightFootPrint;

    // 발 위치, 걸음 기준 y 좌표
    public const float footPrintY = 0.01f;
    public const float stepCountY = 0.1f;

    // 아바타 상태 변수
    public static bool isJumping;
    public static bool stepSide;
    public static float stepRecordTime;
    public static List<float> steps;

    // 유저 위치 변수
    public static float userSpineX;
    public static float userSpineY;
    public static float userSpineZ;

    // 일시정지 변수
    public static bool pauseHandler;

    // Start is called before the first frame update
    void Start()
    {
        InitialObjects();
        InitialValues();

    }

    // 게임오브젝트 불러오기
    void InitialObjects() {
        avatar = gameObject;

        leftFloorTile = GameObject.Find("floorTile-left");
        centerFloorTile = GameObject.Find("floorTile-center");
        rightFloorTile = GameObject.Find("floorTile-right");

        leftFootPrint = GameObject.Find("footprint-left");
        rightFootPrint = GameObject.Find("footprint-right");

        resumeTile.transform.gameObject.SetActive(false);
    }

    // 변수 초기화
    void InitialValues() {
        // 상태변수 초기화
        isJumping = false;
        stepSide = false;
        pauseHandler = false;

        stepRecordTime = 0;

        // 유저 위치 초기화
        userSpineX = 0;
        userSpineY = 0;
        userSpineZ = 0;

        InitialStepRecords();
    }

    // 걸음 시간 리스트 초기화 (0)
    public static void InitialStepRecords() {
        steps = Enumerable.Repeat<float>(0, 10).ToList<float>();

    }

    // 걸음 시간 측정 (+ fixedDeltaTime)
    private void FixedUpdate()
    {
        stepRecordTime += Time.fixedDeltaTime;
    }

    void Update()
    {
        Debug.Log(userSpineX + "/" + userSpineY + "/" + userSpineZ);

        // 아바타 업데이트
        HandleAvatarPosition();
        HandleFootPrints();

        // 아바타와 바닥 UI 상호작용
        HandleFloorTiles();
        HandleJump();
        HandleSteps();

        HandlePause();
    }

    // 아바타 위치를 유저 위치로 변경
    void HandleAvatarPosition() {
        avatar.transform.position = new Vector3(userSpineX, userSpineY, userSpineZ);
    }

    // 발 위치 원 이동
    void HandleFootPrints()
    {
        leftFootPrint.transform.position = new Vector3(leftFoot.transform.position.x, footPrintY, leftFoot.transform.position.z - 10);
        rightFootPrint.transform.position = new Vector3(rightFoot.transform.position.x, footPrintY, rightFoot.transform.position.z - 10);
    }

    // 바닥 스크린들의 밟은 판정
    void HandleFloorTiles() {
        HandleFloorTile(Player.highlight, leftFloorTile, Tile.left);
        HandleFloorTile(Player.highlight, centerFloorTile, Tile.center);
        HandleFloorTile(Player.highlight, rightFloorTile, Tile.right);
    }

    // 두 발이 모두 타일 안에 있는 경우 하이라이트 위치 변경
    void HandleFloorTile(GameObject highlight, GameObject floorTile, float positionX) {
        if (onTile(floorTile))
            highlight.transform.position = new Vector3(positionX, highlight.transform.position.y, highlight.transform.position.z);
    }

    // 결음 기록 조건 만족 시 함수 호출
    void HandleSteps() {
        if ((stepSide == true && leftFoot.transform.position.y > stepCountY && rightFoot.transform.position.y < stepCountY)
            || (stepSide == false && leftFoot.transform.position.y < stepCountY && rightFoot.transform.position.y > stepCountY))
            HandleStep();
    }

    // 걸음시간 기록 및 초기화, 결음 방향 변경
    void HandleStep() {
        stepSide = !stepSide;
        steps.RemoveAt(0);
        steps.Add(10 / stepRecordTime);
        Tile.extraSpeed = steps.Average();
        stepRecordTime = 0;
    }

    // 오브젝트가 타일 위에 있는지 판별
    bool onTile(GameObject tile) {
        return IsInside(tile, leftFootPrint) && IsInside(tile, rightFootPrint);
    }

    // 오브젝트가 타일 안에 있으면 true 반환
    bool IsInside (GameObject tile, GameObject obj) {
        float objX = obj.transform.position.x;
        float objZ = obj.transform.position.z;

        bool horizontal = (objX > tile.transform.position.x - (tile.transform.localScale.x / 2))
            && (objX < tile.transform.position.x + (tile.transform.localScale.x / 2));
        bool vertical = (objZ > tile.transform.position.z - (tile.transform.localScale.z / 2))
            && (objZ < tile.transform.position.z + (tile.transform.localScale.z / 2));

        return horizontal && vertical;
    }

    // 점프 조건
    void HandleJump() {
        isJumping =  leftFoot.transform.position.y > 0.2 && rightFoot.transform.position.y > 0.2;
    }

    // 일시정지와 다시시작 조건
    void HandlePause() {
        if (pauseHandler)
        {
            UIinGame.instance.bePause = true;
            resumeTile.transform.gameObject.SetActive(true);
        }
        else {
            if ((leftHand.transform.position.y > head.transform.position.y && rightHand.transform.position.y > head.transform.position.y)
                && (onTile(resumeTile))) {
                UIinGame.instance.bePause = false;
                resumeTile.transform.gameObject.SetActive(false);
            }
        }

    }
}
