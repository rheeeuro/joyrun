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

    // 오브젝트가 타일 위에 있는지 판별
    public static bool onTile(GameObject tile) {
        return IsInside(tile, userPositionLeftFoot) && IsInside(tile, userPositionRightFoot);
    }

    // 두 발이 원 타일 안에 있는지 확인
    public static bool OnCircleTile (GameObject tile)
    {
        bool leftFootOnCircleTile =  (((userPositionLeftFoot.x - tile.transform.position.x) * (userPositionLeftFoot.x - tile.transform.position.x))
            + ((userPositionLeftFoot.z - tile.transform.position.z) * (userPositionLeftFoot.z - tile.transform.position.z)))
        <= (tile.transform.localScale.x * tile.transform.localScale.x);
        bool rightFootOnCircleTile = (((userPositionRightFoot.x - tile.transform.position.x) * (userPositionRightFoot.x - tile.transform.position.x))
            + ((userPositionRightFoot.z - tile.transform.position.z) * (userPositionRightFoot.z - tile.transform.position.z)))
        <= (tile.transform.localScale.x * tile.transform.localScale.x);
        return leftFootOnCircleTile && rightFootOnCircleTile;
    }

    // 한 발이 원 타일 안에 있는지 확인
    public static bool OneFootOnCircleTile(GameObject tile) {
        bool leftFootOnCircleTile = (((userPositionLeftFoot.x - tile.transform.position.x) * (userPositionLeftFoot.x - tile.transform.position.x))
            + ((userPositionLeftFoot.z - tile.transform.position.z) * (userPositionLeftFoot.z - tile.transform.position.z)))
        <= (tile.transform.localScale.x * tile.transform.localScale.x);
        bool rightFootOnCircleTile = (((userPositionRightFoot.x - tile.transform.position.x) * (userPositionRightFoot.x - tile.transform.position.x))
            + ((userPositionRightFoot.z - tile.transform.position.z) * (userPositionRightFoot.z - tile.transform.position.z)))
        <= (tile.transform.localScale.x * tile.transform.localScale.x);
        return leftFootOnCircleTile || rightFootOnCircleTile;
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
