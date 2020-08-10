using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    // 유저 존재 여부
    private static bool userValid;

    // 유저 위치 변수
    public static Vector3 userPosition;
    public static Vector3 userPositionLeftFoot;
    public static Vector3 userPositionRightFoot;
    public static Vector3 userPositionLeftHand;
    public static Vector3 userPositionRightHand;
    public static Vector3 userPositionHead;

    static void Start()
    {
        InitialUserPosition();
    }

    // 유저 존재 여부 getter
    public static bool GetUserValid()
    {
        return userValid;
    }

    // 유저 존재 여부 setter
    public static void SetUserValid(bool newUserValid)
    {
        userValid = newUserValid;
    }

    // 유저 벡터 초기화
    static void InitialUserPosition() {
        userPosition = Vector3.zero;
        userPositionLeftFoot = Vector3.zero;
        userPositionRightFoot = Vector3.zero;
        userPositionLeftHand = Vector3.zero;
        userPositionRightHand = Vector3.zero;
        userPositionHead = Vector3.zero;
    }

    // 키넥트 좌표를 게임 상의 좌표로 변환
    public static Vector3 HandleKinectPosition(Vector3 kinectPosition)
    {
        return new Vector3(kinectPosition.x * 10, kinectPosition.y * 10, (kinectPosition.z - 1.45f) * -10);
    }

    // 발 위치 원 크기 설정
    public static float HandleFootprintSize(float footPositionY) {
        if (footPositionY < 1.5)
            return 0.7f;
        else if (footPositionY >= 1.5 && footPositionY < 2.5)
            return 2.5f - footPositionY;
        else
            return 0;
    }

    // 한 발이 타일 위에 있는지 판별
    public static bool OneFootOnTile(GameObject tile)
    {
        return IsInside(tile, userPositionLeftFoot) || IsInside(tile, userPositionRightFoot);
    }

    // 오브젝트가 타일 위에 있는지 판별
    public static bool OnTile(GameObject tile) {
        return IsInside(tile, userPositionLeftFoot) && IsInside(tile, userPositionRightFoot);
    }

    // 두 발이 원 타일 안에 있는지 확인 (+ y좌표 확인)
    public static bool OnCircleTile (GameObject tile)
    {
        return (IsInsideCircle(tile, userPositionLeftFoot) && userPositionLeftFoot.y < ConstInfo.buttonPushLimitY) 
            && (IsInsideCircle(tile, userPositionRightFoot) && userPositionRightFoot.y < ConstInfo.buttonPushLimitY);
    }

    // 한 발이 원 타일 안에 있는지 확인 (+ y좌표 확인)
    public static bool OneFootOnCircleTile(GameObject tile) {
        return (IsInsideCircle(tile, userPositionLeftFoot) && userPositionLeftFoot.y < ConstInfo.buttonPushLimitY)
            || (IsInsideCircle(tile, userPositionRightFoot) && userPositionRightFoot.y < ConstInfo.buttonPushLimitY);
    }

    // 두 발이 원 타일 위에 있는지 확인
    public static bool IsInsideCircle(GameObject tile, Vector3 obj) {
        return (((obj.x - tile.transform.position.x) * (obj.x - tile.transform.position.x))
            + ((obj.z - tile.transform.position.z) * (obj.z - tile.transform.position.z)))
        <= (tile.transform.localScale.x/2 * tile.transform.localScale.x/2);
    }

    // 벡터3가 오브젝트가 타일 안에 있으면 true 반환
    public static bool IsInside (GameObject tile, Vector3 obj) {
        bool horizontal = (obj.x > tile.transform.position.x - (tile.transform.localScale.x / 2))
            && (obj.x < tile.transform.position.x + (tile.transform.localScale.x / 2));
        bool vertical = (obj.z > tile.transform.position.z - (tile.transform.localScale.z / 2))
            && (obj.z < tile.transform.position.z + (tile.transform.localScale.z / 2));
        return horizontal && vertical;
    }
}
