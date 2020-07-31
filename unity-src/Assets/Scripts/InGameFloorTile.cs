    using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;

public class InGameFloorTile : MonoBehaviour
{
    // 바닥 UI 타일
    public static GameObject leftFloorTile;
    public static GameObject centerFloorTile;
    public static GameObject rightFloorTile;
    public static GameObject resumeTile;
    public static GameObject newGameTile;
    public static GameObject toMenuTile;

    // 발위치 원 변수
    public GameObject leftFootPrint;
    public GameObject rightFootPrint;

    // 발 위치, 걸음, 점프, 펀치 기준 y 좌표
    public const float footPrintSize = 1;
    public const float footPrintStartSize = 0.7f;
    public const float foorPrintScaleY = 0.005f;

    public const float stepCountY = 2;
    public const float jumpConditionY = 2;
    public const float punchDistance = 3;

    // 유저 상태 변수
    public static bool isJumping;
    public static bool isPunching;
    public static bool stepSide;
    public static float stepRecordTime;
    public static List<float> steps;

    // 일시정지 변수
    public static bool pauseHandler;

    // 바닥 타일 너비
    public const float floorTileScaleX = 9;


    public const float pushTime = 1;
    // 버튼 타이머 변수 - 0: newgame, 1: menu
    public float[] uiTimer;

    // Start is called before the first frame update
    void Start()
    {
        InitialObjects();
        InitialValues();
    }

    // 게임오브젝트 불러오기
    void InitialObjects()
    {
        leftFloorTile = GameObject.Find("FloorTile-left");
        centerFloorTile = GameObject.Find("FloorTile-center");
        rightFloorTile = GameObject.Find("FloorTile-right");

        resumeTile = GameObject.Find("ResumeTile");
        resumeTile.transform.gameObject.SetActive(false);
        newGameTile = GameObject.Find("NewGameTile");
        newGameTile.transform.gameObject.SetActive(false);
        toMenuTile = GameObject.Find("ToMenuTile");
        toMenuTile.transform.gameObject.SetActive(false);
    }

    // 변수 초기화
    void InitialValues()
    {
        // 상태변수 초기화
        isJumping = false;
        isPunching = false;
        stepSide = false;
        pauseHandler = false;

        // 걸음 관련 변수 초기화
        stepRecordTime = 0;
        InitialStepRecords();

        // UI 타이머 배열 초기화
        uiTimer = new float[2] { 0, 0 };
    }

    // 걸음 시간 리스트 초기화 (0)
    public static void InitialStepRecords()
    {
        steps = Enumerable.Repeat<float>(0, 10).ToList<float>();

    }

    // 걸음 시간 측정 (+ fixedDeltaTime)
    private void FixedUpdate()
    {
        stepRecordTime += Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        // 위치 업데이트
        HandleFootPrint();

        // 아바타와 바닥 UI 상호작용
        HandleFloorTiles();
        HandleJump();
        HandleSteps();
        HandleAvatarPunch();

        // 일시정지 설정
        HandlePause();
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
        if (Player.highlight != null)
        {
            HandleFloorTile(Player.highlight, leftFloorTile, Tile.left);
            HandleFloorTile(Player.highlight, centerFloorTile, Tile.center);
            HandleFloorTile(Player.highlight, rightFloorTile, Tile.right);
        }

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

    // 두 발이 모두 타일 안에 있는 경우 하이라이트 위치 변경
    void HandleFloorTile(GameObject highlight, GameObject floorTile, float positionX)
    {
        if (Avatar.OnTile(floorTile))
            highlight.transform.position = new Vector3(positionX, highlight.transform.position.y, highlight.transform.position.z);
    }

    // 결음 기록 조건 만족 시 함수 호출
    void HandleSteps()
    {
        if ((stepSide == true && Avatar.userPositionLeftFoot.y > stepCountY && Avatar.userPositionRightFoot.y < stepCountY)
            || (stepSide == false && Avatar.userPositionLeftFoot.y < stepCountY && Avatar.userPositionRightFoot.y > stepCountY))
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
        isJumping = Avatar.userPositionLeftFoot.y > jumpConditionY && Avatar.userPositionRightFoot.y > jumpConditionY;
    }



    void HandleAvatarPunch()
    {
        if ((-Avatar.userPositionLeftHand.z > -Avatar.userPositionHead.z + punchDistance)
            || (-Avatar.userPositionRightHand.z > -Avatar.userPositionHead.z + punchDistance))
            isPunching = true;
        else
            isPunching = false;
    }

    // 일시정지와 다시시작 조건
    void HandlePause()
    {
        if (pauseHandler)
        {
            UIinGame.instance.bePause = true;
            resumeTile.transform.gameObject.SetActive(true);
            newGameTile.transform.gameObject.SetActive(true);
            toMenuTile.transform.gameObject.SetActive(true);

            HandleUITimer();
        }
        else
        {
            if ((Avatar.userPositionLeftHand.y > Avatar.userPositionHead.y && Avatar.userPositionRightHand.y > Avatar.userPositionHead.y)
                && (Avatar.OnCircleTile(resumeTile)))
            {
                UIinGame.instance.bePause = false;
                resumeTile.transform.gameObject.SetActive(false);
            }
        }
    }

    void HandleUITimer() {

        if (Avatar.OneFootOnCircleTile(newGameTile))
            HandleNewGameTile();
        else
            uiTimer[0] = 0;

        if (Avatar.OneFootOnCircleTile(toMenuTile))
            HandleToMenuTile();
        else
            uiTimer[1] = 0;

    }

    void HandleNewGameTile() {
        uiTimer[0] += Time.deltaTime;
        if (uiTimer[0] > pushTime) {
            UIinGame.instance.bePause = false;
            SceneManager.LoadScene("Game");
        }
    }

    void HandleToMenuTile() {
        uiTimer[1] += Time.deltaTime;
        if (uiTimer[1] > pushTime) {
            UIinGame.instance.bePause = false;
            SceneManager.LoadScene("Main");
        }
    }

}
