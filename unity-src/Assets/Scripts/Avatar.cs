using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    // 유저 위치 변수
    public static Vector3 userPosition;
    public static Vector3 userPositionLeftFoot;
    public static Vector3 userPositionRightFoot;
    public static Vector3 userPositionLeftHand;
    public static Vector3 userPositionRightHand;
    public static Vector3 userPositionHead;

    public static float buttonPushLimitY = 2;

    // Start is called before the first frame update
    void Start()
    {
        InitialUserSpineVector();
    }

    // 유저 벡터 초기화
    void InitialUserSpineVector() {
        // 유저 위치 초기화
        userPosition = new Vector3(0, 0, 0);
        userPositionLeftFoot = new Vector3(0, 0, 0);
        userPositionRightFoot = new Vector3(0, 0, 0);
        userPositionLeftHand = new Vector3(0, 0, 0);
        userPositionRightHand = new Vector3(0, 0, 0);
        userPositionHead = new Vector3(0, 0, 0);
    }
    
    void Update()
    {

    }

    // 키넥트 좌표를 게임 상의 좌표로 변환
    public static Vector3 HandleKinectPosition(Vector3 kinectPosition)
    {
        return new Vector3(kinectPosition.x * 10, kinectPosition.y * 10, (kinectPosition.z - 1.45f) * -10);
    }

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
        return (IsInsideCircle(tile, userPositionLeftFoot) && userPositionLeftFoot.y < buttonPushLimitY) 
            && (IsInsideCircle(tile, userPositionRightFoot) && userPositionRightFoot.y < buttonPushLimitY);
    }

    // 한 발이 원 타일 안에 있는지 확인 (+ y좌표 확인)
    public static bool OneFootOnCircleTile(GameObject tile) {
        return (IsInsideCircle(tile, userPositionLeftFoot) && userPositionLeftFoot.y < buttonPushLimitY)
            || (IsInsideCircle(tile, userPositionRightFoot) && userPositionRightFoot.y < buttonPushLimitY);
    }

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
