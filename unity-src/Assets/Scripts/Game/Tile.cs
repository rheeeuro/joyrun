using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    // 게임 오브젝트 변수 선언 (5가지)
    public GameObject heartTile;
    public GameObject obstacleTile;
    public GameObject emptyTile;
    public GameObject emptyTilePass;
    public GameObject trapTile;
    public GameObject balloonTile;

    // 리스트 변수 선언
    public static List<GameObject> randomTiles;
    public static List<GameObject> badTiles;
    public static List<GameObject> activatedTiles;

    // 지정 위치 상수
    public const float left = 100;
    public const float center = 114;
    public const float right = 128;

    public const float startPositionY = 1;
    public const float startPositionZ = 80;

    public const float scaleX = 14;

    public const float destroyLine = -65;
    public const float tileDistance = 60;
    public static float[] collisionPosition = { -30, -36, -42, -48 };
    public const float collisionGap = 5;

    // 마지막 줄의 하트 위치 변수
    public static float heartDirection;

    // 속도 변수, 상수
    public static float extraSpeed;
    public static float actualSpeed;
    public const float actualSpeedStart = 30;

    // 시간 변수
    public float tileDelay;
    public const float startTileDelay = 2;
    public const float tileDelayIncrease = 0.1f;
    public int createTileCount;

    // 이벤트 시작 시간 상수
    public const float obstableAnimStartTime = 20;
    public const float trapAnimStartTime = 30;
    public const float tileAnimationLength = 2;


    void Start()
    {
        InitialValues();
        GameManager.instance.Game();
    }

    // 변수 초기화
    void InitialValues()
    {
        randomTiles = new List<GameObject>();
        badTiles = new List<GameObject>();
        activatedTiles = new List<GameObject>();

        InitializePrefabs();

        tileDelay = startTileDelay;
        actualSpeed = actualSpeedStart;
        createTileCount = 0;
        extraSpeed = 0;
        heartDirection = 0;
    }

    void InitializePrefabs()
    {
        // Prefab 불러오기
        heartTile = Resources.Load("Prefabs/heart-tile") as GameObject;
        obstacleTile = Resources.Load("Prefabs/obstacle-tile") as GameObject;
        emptyTile = Resources.Load("Prefabs/empty-tile") as GameObject;
        emptyTilePass = Resources.Load("Prefabs/empty-tile-pass") as GameObject;
        trapTile = Resources.Load("Prefabs/trap-tile") as GameObject;
        balloonTile = Resources.Load("Prefabs/balloon-tile") as GameObject;

        // List 설정
        randomTiles.Add(obstacleTile);
        randomTiles.Add(emptyTile);
        randomTiles.Add(trapTile);

        badTiles.Add(obstacleTile);
        badTiles.Add(trapTile);
    }


    void Update()
    {
        HandleTiles();
        CheckCollision();
    }

    // 타일 설정 (생성, 속도, 이동, 삭제)
    void HandleTiles()
    {
        HandleTileCreate();
        HandleTileSpeed();
        HandleTileMove();
        HandleTileDestroy();
    }

    // 타일 생성 알고리즘
    void HandleTileCreate()
    {
        if (IsTimeToCreateTiles())
        {
            CreateTiles();
            HandleTileAnimation();
            if (createTileCount % 10 == 0)
            {
                tileDelay -= tileDelayIncrease;
                createTileCount = 0;
            }
        }
    }

    // 마지막 줄 타일이 간격만큼 이동 시 타일 생성
    bool IsTimeToCreateTiles()
    {
        return activatedTiles.Count == 0 || activatedTiles[activatedTiles.Count - 1].transform.position.z < startPositionZ - tileDistance;
    }

    // 2분의 1 확률 랜덤 함수
    bool YesOrNo()
    {
        return Random.Range(0, 10) % 2 == 0;
    }

    // 리스트에서 랜덤 오브젝트 반환
    GameObject GetRandomFromList(List<GameObject> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    // 마지막 줄 타일에 하트 존재 여부에 따른 타일 생성
    void CreateTiles()
    {
        if (heartDirection == 0)
            CreateTilesAfterEmpty();
        else
            CreateTilesAfterHeart();

        createTileCount++;
    }

    // 마지막 줄에 하트가 없을 경우의 타일 생성
    void CreateTilesAfterEmpty()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                SetGoodTile(left);
                CreateOne(GetRandomFromList(randomTiles), center);
                CreateOne(GetRandomFromList(randomTiles), right);
                break;
            case 1:
                CreateOne(GetRandomFromList(randomTiles), left);
                SetGoodTile(center);
                CreateOne(GetRandomFromList(randomTiles), right);
                break;
            case 2:
                CreateOne(GetRandomFromList(randomTiles), left);
                CreateOne(GetRandomFromList(randomTiles), center);
                SetGoodTile(right);
                break;
        }
    }
    
    // 마지막 줄에 하트가 있는 경우의 타일 생성
    void CreateTilesAfterHeart()
    {
        if (heartDirection == left)
        {
            CreateOne(GetRandomFromList(badTiles), left);
            SetGoodAndRandomTile(center, right);
        }
        else if (heartDirection == center)
        {
            CreateOne(GetRandomFromList(badTiles), center);
            SetGoodAndRandomTile(left, right);
        }
        else if (heartDirection == right)
        {
            CreateOne(GetRandomFromList(badTiles), right);
            SetGoodAndRandomTile(left, center);
        }
    }

    // 지나갈 수 있는 타일과 랜덤타일 생성
    void SetGoodAndRandomTile(float dir1, float dir2)
    {
        if (YesOrNo())
        {
            SetGoodTile(dir1);
            CreateOne(GetRandomFromList(randomTiles), dir2);
        }
        else
        {
            CreateOne(GetRandomFromList(randomTiles), dir1);
            SetGoodTile(dir2);
        }
    }

    // 지나갈 수 있는 길 생성 (하트 혹은 빈칸)
    void SetGoodTile(float direction)
    {
        if (YesOrNo())
        {
            int temp = Random.Range(0, 3);
            if (temp != 2)
                CreateOne(heartTile, direction);
            else
                CreateOne(balloonTile, direction);
            heartDirection = direction;

        }
        else
        {
            CreateOne(emptyTilePass, direction);
            heartDirection = 0;
        }
    }

    // 타일을 direction에 한 개 생성
    void CreateOne(GameObject tile, float direction)
    {
        activatedTiles.Add(Instantiate(tile, new Vector3(direction, startPositionY, startPositionZ), Player.player.transform.rotation) as GameObject);
    }

    // 조건 만족 시 장애물 애니메이션 재생
    void HandleObstacleAnimation()
    {
        GameObject lastLeftTile = activatedTiles[activatedTiles.Count - 3];
        GameObject lastCenterTile = activatedTiles[activatedTiles.Count - 2];
        GameObject lastRightTile = activatedTiles[activatedTiles.Count - 1];

        // 중앙에 장애물이 있고 좌, 우로 통과가 가능한 경우
        if ((lastCenterTile.tag == "obstacle-tile" || lastCenterTile.tag == "trap-tile") 
            && ((IsPassDirection(lastLeftTile) && lastRightTile.tag == "empty-tile") || (IsPassDirection(lastRightTile) && lastLeftTile.tag == "empty-tile"))) 
        {
            if (IsPassDirection(lastLeftTile))
                HandleAnimation(lastRightTile, obstacleTile, activatedTiles.Count - 1, "obstacle-anim-right");
            else
                HandleAnimation(lastLeftTile, obstacleTile, activatedTiles.Count - 3, "obstacle-anim-left");
        }
    }

    // 타일이 최초 지나가는 길로 설정된 길인 지 판별
    bool IsPassDirection(GameObject tile) {
        return tile.tag == "heart-tile" || tile.tag == "empty-tile-pass";
    }

    // 이벤트 발생 알고리즘 (애니메이션)
    void HandleTileAnimation() {
        if (Player.timer <= Player.gameTime - obstableAnimStartTime)
            HandleObstacleAnimation();
        if (Player.timer <= Player.gameTime - trapAnimStartTime)
            HandleTrapAnimation();
    }

    // 조건 만족 시 구덩이 애니메이션 재생
    void HandleTrapAnimation()
    {
        GameObject lastLeftTile = activatedTiles[activatedTiles.Count - 3];
        GameObject lastCenterTile = activatedTiles[activatedTiles.Count - 2];
        GameObject lastRightTile = activatedTiles[activatedTiles.Count - 1];

        if (lastLeftTile.tag == "empty-tile" && !IsPassDirection(lastLeftTile))
            HandleAnimation(lastLeftTile, trapTile, activatedTiles.Count - 3, "trap-anim");
        if (lastCenterTile.tag == "empty-tile" && !IsPassDirection(lastCenterTile))
            HandleAnimation(lastCenterTile, trapTile, activatedTiles.Count - 2, "trap-anim");
        if (lastRightTile.tag == "empty-tile" && !IsPassDirection(lastRightTile))
            HandleAnimation(lastRightTile, trapTile, activatedTiles.Count - 1, "trap-anim");

    }

    // 애니메이션 재생 타일로 변경
    void HandleAnimation(GameObject oldTile, GameObject newTile, int index, string animationName)
    {
        activatedTiles[index] = Instantiate(newTile, new Vector3(oldTile.transform.position.x, startPositionY, oldTile.transform.position.z), 
            Player.player.transform.rotation) as GameObject;
        oldTile.SetActive(false);
        Destroy(oldTile);
        activatedTiles[index].GetComponent<Animation>()[animationName].speed = tileAnimationLength / (40 / actualSpeed);
        activatedTiles[index].GetComponent<Animation>().Play(animationName);
    }


    // 타일 이동 알고리즘
    void HandleTileMove()
    {
        for (int i = 0; i < activatedTiles.Count; i++)
            activatedTiles[i].transform.Translate(Vector3.back * actualSpeed * Time.deltaTime);
    }

    // 타일 속도 알고리즘
    void HandleTileSpeed()
    {
        actualSpeed = (tileDistance / tileDelay) + extraSpeed;

        if (actualSpeed > 90)
            actualSpeed = 90;

        if (GameManager.instance.GetGameState() != GameState.game)
            actualSpeed = 0;
    }

    // 타일 삭제 알고리즘
    void HandleTileDestroy()
    {
        for (int i = 0; i < activatedTiles.Count; i++)
        {
            if (activatedTiles[i].transform.position.z < destroyLine)
            {
                activatedTiles[i].SetActive(false);
                Destroy(activatedTiles[i]);
                activatedTiles.RemoveAt(i);
            }
        }
    }

    // 충동 판정 알고리즘
    void CheckCollision() // 하트 기준 좌표: 타일 + 30, 36, 42, 48
    {
        for (int i = 0; i < activatedTiles.Count; i++)
        {
            switch (activatedTiles[i].tag)
            {
                case "heart-tile":
                    CheckCollisionHeart(activatedTiles[i]);
                    break;
                case "obstacle-tile":
                    CheckCollisionObstacle(activatedTiles[i]);
                    break;
                case "empty-tile":
                    CheckCollisionEmpty(activatedTiles[i]);
                    break;
                case "empty-tile-pass":
                    CheckCollisionEmpty(activatedTiles[i]);
                    break;
                case "trap-tile":
                    CheckCollisionObstacle(activatedTiles[i]);
                    break;
                case "balloon-tile":
                    CheckCollisionBalloon(activatedTiles[i]);
                    break;
                default:
                    Debug.Log("[ERROR] Unknown tagged Tile.");
                    break;
            }
        }
    }

    // 하트 충돌 판정 알고리즘
    void CheckCollisionHeart(GameObject obj)
    {
        for (int i = 0; i < collisionPosition.Length; i++)
        {
            if (Mathf.Abs(obj.transform.position.z - collisionPosition[i]) < collisionGap
                && GetChildTransform(obj, i * 2).localScale.x != 0
                && obj.transform.position.x == Player.highlight.transform.position.x
                && !Player.isJumping)
            {
                GetChildTransform(obj, i * 2).localScale = new Vector3(0, 0, 0);
                GetChildTransform(obj, i * 2 + 1).localScale = new Vector3(0, 0, 0);
                Player.instance.MeetHeart();
            }
        }
    }

    // 장애물 충돌 판정 알고리즘
    void CheckCollisionObstacle(GameObject obj)
    {
        if (Mathf.Abs(obj.transform.position.z - collisionPosition[0]) < collisionGap
            && GetChildTransform(obj, 0).localScale.x != 0
            && obj.transform.position.x == Player.highlight.transform.position.x
            )
        {
            if (Player.isJumping)
            {
                if (GetChildTransform(obj, 1).localScale.x != 0)
                {
                    Player.instance.MeetEmpty();
                    GetChildTransform(obj, 1).localScale = new Vector3(0, 0, 0);
                }
            }
            else
            {
                GetChildTransform(obj, 0).localScale = new Vector3(0, 0, 0);
                Player.instance.MeetObstacle();
                GameFloorTile.InitialStepRecords();
            }

        }
    }

    void CheckCollisionBalloon(GameObject obj)
    {
        if (Mathf.Abs(obj.transform.position.z - collisionPosition[0]) < collisionGap
        && GetChildTransform(obj, 0).localScale.x != 0
        && obj.transform.position.x == Player.highlight.transform.position.x
        && GameFloorTile.isPunching)
        {
            Player.instance.MeetBalloon();
            GetChildTransform(obj, 0).localScale = new Vector3(0, 0, 0);
            GetChildTransform(obj, 1).localScale = new Vector3(0, 0, 0);
        }
    }

    // 빈칸 충돌 판정 알고리즘
    void CheckCollisionEmpty(GameObject obj)
    {
        if (Mathf.Abs(obj.transform.position.z - collisionPosition[0]) < collisionGap
            && GetChildTransform(obj, 0).localScale.x != 0
            && obj.transform.position.x == Player.highlight.transform.position.x)
        {
            GetChildTransform(obj, 0).localScale = new Vector3(0, 0, 0);
            Player.instance.MeetEmpty();
        }
    }

    // 자식 오브젝트 tramsform 반환
    Transform GetChildTransform(GameObject obj, int index)
    {
        return obj.transform.GetChild(index).gameObject.transform;
    }
}
