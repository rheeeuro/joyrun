using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // 게임 오브젝트 변수 선언
    public GameObject heartTile;
    public GameObject obstacleTile;
    public GameObject emptyTile;
    public GameObject emptyTilePass;
    public GameObject trapTile;
    public GameObject balloonTile;
    public GameObject trapTileEvent;
    public GameObject obstacleTileEvent;

    public GameObject leftTile;
    public GameObject centerTile;
    public GameObject rightTile;

    // 리스트 변수 선언
    public static List<GameObject> randomTiles;
    public static List<GameObject> badTiles;
    public static List<GameObject> activatedTiles;

    // 마지막 줄의 하트 위치 변수
    public static float heartDirection;

    // 추가속도, 실제속도 변수
    public static float extraSpeed;
    public static float actualSpeed;

    // 타일이 나온 뒤 다음타일이 나오기까지의 시간
    public float tileDelay;

    // 생성된 타일의 줄 수 변수
    public int createTileCount;

    public int heartBonusCount;

    RuntimeAnimatorController trapActivate;
    RuntimeAnimatorController trapEvent;
    RuntimeAnimatorController hurdleEvent;
    RuntimeAnimatorController hurdleDown;
    RuntimeAnimatorController balloonEvent;

    void Start()
    {
        InitialLists();
        InitialValues();
        LoadPrefabs();
        SetTileLists();
    }

    // 리스트 초기화
    void InitialLists()
    {
        randomTiles = new List<GameObject>();
        badTiles = new List<GameObject>();
        activatedTiles = new List<GameObject>();
    }

    // 변수 초기화
    void InitialValues()
    {
        tileDelay = ConstInfo.startTileDelay;
        actualSpeed = ConstInfo.actualSpeedStart;
        createTileCount = 0;
        extraSpeed = 0;
        heartDirection = 0;
        heartBonusCount = 0;
    }

    // Prefab 불러오기
    void LoadPrefabs()
    {
        heartTile = Resources.Load("Prefabs/Prefabs/heart-tile") as GameObject;
        obstacleTile = Resources.Load("Prefabs/Prefabs/obstacle-tile") as GameObject;
        emptyTile = Resources.Load("Prefabs/Prefabs/empty-tile") as GameObject;
        emptyTilePass = Resources.Load("Prefabs/Prefabs/empty-tile-pass") as GameObject;
        trapTile = Resources.Load("Prefabs/Prefabs/trap-tile") as GameObject;
        balloonTile = Resources.Load("Prefabs/Prefabs/balloon-tile") as GameObject;

        obstacleTileEvent = Resources.Load("Prefabs/Prefabs/obstacle-tile-event") as GameObject;
        trapTileEvent = Resources.Load("Prefabs/Prefabs/trap-tile-event") as GameObject;

        trapActivate = Resources.Load("Prefabs/AnimationControllers/TrapActivate") as RuntimeAnimatorController;
        trapEvent = Resources.Load("Prefabs/AnimationControllers/TrapAppear") as RuntimeAnimatorController;
        hurdleEvent = Resources.Load("Prefabs/AnimationControllers/HurdleAppear") as RuntimeAnimatorController;
        hurdleDown = Resources.Load("Prefabs/AnimationControllers/HurdleDown") as RuntimeAnimatorController;
        balloonEvent = Resources.Load("Prefabs/AnimationControllers/BalloonEvent") as RuntimeAnimatorController;
    }

    // List 설정 (random: 장애물, 빈, 함정 / bad: 장애물, 함정)
    void SetTileLists() {
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
        HandleTileAnimation();
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
            if (createTileCount % 10 == 0)
            {
                tileDelay -= ConstInfo.tileDelayIncrease;
                createTileCount = 0;
            }
        }
    }

    // 마지막 줄 타일이 간격만큼 이동 시 타일 생성
    bool IsTimeToCreateTiles()
    {
        return activatedTiles.Count == 0 
            || activatedTiles[activatedTiles.Count - 1].transform.position.z < ConstInfo.tileStartPositionZ - ConstInfo.tileDistance;
    }

    // 마지막 줄 타일에 하트 존재 여부에 따른 타일 생성
    void CreateTiles()
    {
        if (heartDirection == 0)
            CreateTilesAfterNoHeart();
        else
            CreateTilesAfterHeart();
        createTileCount++;
    }

    // 마지막 줄에 하트가 없을 경우의 타일 생성
    void CreateTilesAfterNoHeart()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                SetGoodTile(ConstInfo.left);
                CreateOne(GetRandomFromList(randomTiles), ConstInfo.center);
                CreateOne(GetRandomFromList(randomTiles), ConstInfo.right);
                break;
            case 1:
                CreateOne(GetRandomFromList(randomTiles), ConstInfo.left);
                SetGoodTile(ConstInfo.center);
                CreateOne(GetRandomFromList(randomTiles), ConstInfo.right);
                break;
            case 2:
                CreateOne(GetRandomFromList(randomTiles), ConstInfo.left);
                CreateOne(GetRandomFromList(randomTiles), ConstInfo.center);
                SetGoodTile(ConstInfo.right);
                break;
        }
    }
    
    // 마지막 줄에 하트가 있는 경우의 타일 생성
    void CreateTilesAfterHeart()
    {
        if (heartDirection == ConstInfo.left)
        {
            if (YesOrNo())
            {
                CreateOne(GetRandomFromList(badTiles), ConstInfo.left);
                SetGoodTile(ConstInfo.center);
                CreateOne(GetRandomFromList(randomTiles), ConstInfo.right);
            }
            else
            {
                CreateOne(GetRandomFromList(badTiles), ConstInfo.left);
                CreateOne(GetRandomFromList(randomTiles), ConstInfo.center);
                SetGoodTile(ConstInfo.right);
            }
        }
        else if (heartDirection == ConstInfo.center)
        {
            if (YesOrNo())
            {
                SetGoodTile(ConstInfo.left);
                CreateOne(GetRandomFromList(badTiles), ConstInfo.center);
                CreateOne(GetRandomFromList(randomTiles), ConstInfo.right);
            }
            else
            {
                CreateOne(GetRandomFromList(randomTiles), ConstInfo.left);
                CreateOne(GetRandomFromList(badTiles), ConstInfo.center);
                SetGoodTile(ConstInfo.right);
            }
        }
        else if (heartDirection == ConstInfo.right)
        {
            if (YesOrNo())
            {
                SetGoodTile(ConstInfo.left);
                CreateOne(GetRandomFromList(randomTiles), ConstInfo.center);
                CreateOne(GetRandomFromList(badTiles), ConstInfo.right);
            }
            else
            {
                CreateOne(GetRandomFromList(randomTiles), ConstInfo.left);
                SetGoodTile(ConstInfo.center);
                CreateOne(GetRandomFromList(badTiles), ConstInfo.right);
            }
        }
    }

    // 지나갈 수 있는 길 생성 (하트 혹은 빈칸, 풍선)
    void SetGoodTile(float direction)
    {
        if (YesOrNo())
        {
            if (Random.Range(0, 3) != 0)
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
        activatedTiles.Add(Instantiate(tile, new Vector3(direction, ConstInfo.tileStartPositionY, ConstInfo.tileStartPositionZ), 
            Player.instance.player.transform.rotation) as GameObject);
    }



    // 조건 만족 시 장애물 애니메이션 재생
    void HandleObstacleAnimation()
    {
        for (int i = 0; i <= activatedTiles.Count-3; i += 3)
        {
            GameObject lastLeftTile = activatedTiles[i];
            GameObject lastCenterTile = activatedTiles[i+1];
            GameObject lastRightTile = activatedTiles[i+2];

            // 중앙에 장애물이 있고 좌, 우로 통과가 가능한 경우
            if ((lastCenterTile.tag == "obstacle-tile" || lastCenterTile.tag == "trap-tile")
                && ((IsPassDirection(lastLeftTile) && lastRightTile.tag == "empty-tile") || (IsPassDirection(lastRightTile) && lastLeftTile.tag == "empty-tile"))
                && Mathf.Abs(lastCenterTile.transform.position.z - (ConstInfo.tileAnimationStartPositionZ + actualSpeed)) < ConstInfo.collisionGap)
            {
                if (IsPassDirection(lastLeftTile))
                    HandleAnimation(lastRightTile, obstacleTileEvent, activatedTiles.IndexOf(lastRightTile), hurdleEvent, 2);
                else
                    HandleAnimation(lastLeftTile, obstacleTileEvent, activatedTiles.IndexOf(lastLeftTile), hurdleEvent, 2);
            }
        }
    }


    // 이벤트 발생 알고리즘 (애니메이션)
    void HandleTileAnimation() {
        if (GameUI.instance.timer <= ConstInfo.gameTime - ConstInfo.obstableAnimStartTime)
            HandleObstacleAnimation();
        if (GameUI.instance.timer <= ConstInfo.gameTime - ConstInfo.trapAnimStartTime)
            HandleTrapAnimation();
    }

    // 조건 만족 시 구덩이 애니메이션 재생
    void HandleTrapAnimation()
    {
        for (int i = 0; i < activatedTiles.Count; i++)
            if (activatedTiles[i].tag == "empty-tile" && !IsPassDirection(activatedTiles[i]) 
                && Mathf.Abs(activatedTiles[i].transform.position.z - (ConstInfo.tileAnimationStartPositionZ + actualSpeed)) < ConstInfo.collisionGap)
                HandleAnimation(activatedTiles[i], trapTileEvent, i, trapEvent, 0);
    }

    // 애니메이션 재생 타일로 변경
    void HandleAnimation(GameObject oldTile, GameObject newTile, int index, RuntimeAnimatorController runtimeAnimatorController, int childIndex)
    {
        activatedTiles[index] = Instantiate(newTile, new Vector3(oldTile.transform.position.x, ConstInfo.tileStartPositionY, oldTile.transform.position.z), 
            Player.instance.player.transform.rotation) as GameObject;
        oldTile.SetActive(false);
        Destroy(oldTile);
        //activatedTiles[index].transform.GetChild(childIndex).GetComponent<Animator>().runtimeAnimatorController = runtimeAnimatorController;
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
        if (GameManager.instance.GetGameState() == GameState.game)
            actualSpeed = ((ConstInfo.tileDistance / tileDelay) + extraSpeed) < ConstInfo.actualSpeedMax ? 
                ((ConstInfo.tileDistance / tileDelay) + extraSpeed) : ConstInfo.actualSpeedMax;
        else
            actualSpeed = 0;
    }

    // 타일 삭제 알고리즘
    void HandleTileDestroy()
    {
        for (int i = 0; i < activatedTiles.Count; i++)
        {
            if (activatedTiles[i].transform.position.z < ConstInfo.tileDestroyPositionZ)
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
                    CheckCollisionObstacle(activatedTiles[i], hurdleDown);
                    break;
                case "empty-tile":
                    CheckCollisionEmpty(activatedTiles[i]);
                    break;
                case "empty-tile-pass":
                    CheckCollisionEmpty(activatedTiles[i]);
                    break;
                case "trap-tile":
                    CheckCollisionObstacle(activatedTiles[i], trapActivate);
                    break;
                case "balloon-tile":
                    CheckCollisionBalloon(activatedTiles[i]);
                    break;
            }
        }
    }

    // 하트 충돌 판정 알고리즘
    void CheckCollisionHeart(GameObject obj)
    {
        for (int i = 0; i < ConstInfo.collisionPositionZ.Length; i++)
        {
            if (Mathf.Abs(obj.transform.position.z - ConstInfo.collisionPositionZ[i]) < ConstInfo.collisionGap
                && GetChildTransform(obj, i * 2).localScale.x != 0
                && obj.transform.position.x == Player.instance.highlight.transform.position.x
                && !Player.instance.isJumping)
            {
                GetChildTransform(obj, i * 2).localScale = Vector3.zero;
                GetChildTransform(obj, i * 2 + 1).localScale = Vector3.zero;
                heartBonusCount++;
                Player.instance.HeartCollision(i);
            }
        }

        if (Mathf.Abs(obj.transform.position.z - ConstInfo.CheckHeartBonusPositionZ) < ConstInfo.collisionGap
            && GetChildTransform(obj, 8).localScale.x != 0)
            HandleHeartBonus(obj);
    }

    void HandleHeartBonus(GameObject obj) {
        if (heartBonusCount == 4)
            Player.instance.point += 15;
        else
            Player.instance.point += 3 * heartBonusCount;
        GetChildTransform(obj, 8).localScale = Vector3.zero;
    }


    // 장애물 충돌 판정 알고리즘
    void CheckCollisionObstacle(GameObject obj, RuntimeAnimatorController runtimeAnimatorController)
    {
        if (Mathf.Abs(obj.transform.position.z - ConstInfo.collisionPositionZ[0]) < ConstInfo.collisionGap
            && GetChildTransform(obj, 0).localScale.x != 0
            && obj.transform.position.x == Player.instance.highlight.transform.position.x
            )
        {
            if (Player.instance.isJumping)
            {
                if (GetChildTransform(obj, 1).localScale.x != 0)
                {
                    Player.instance.EmptyCollision();
                    GetChildTransform(obj, 1).localScale = Vector3.zero;
                }
            }
            else
            {
                GetChildTransform(obj, 0).localScale = Vector3.zero;
                obj.transform.GetChild(2).GetComponent<Animator>().runtimeAnimatorController = null;
                obj.transform.GetChild(2).GetComponent<Animator>().runtimeAnimatorController = runtimeAnimatorController;
                Player.instance.ObstacleCollision();
                GameFloorTile.InitialStepRecords();
                extraSpeed = 0;
            }

        }
    }

    // 풍선 충돌 판정 알고리즘
    void CheckCollisionBalloon(GameObject obj)
    {
        if (Mathf.Abs(obj.transform.position.z - ConstInfo.collisionPositionZ[0]) < ConstInfo.collisionGap
        && GetChildTransform(obj, 1).localScale.x != 0
        && obj.transform.position.x == Player.instance.highlight.transform.position.x
        && Player.instance.isPunching)
        {
            Player.instance.BalloonCollision();
            GetChildTransform(obj, 0).GetComponent<Animator>().runtimeAnimatorController = balloonEvent;
            GetChildTransform(obj, 1).localScale = Vector3.zero;
        }
    }

    // 빈칸 충돌 판정 알고리즘
    void CheckCollisionEmpty(GameObject obj)
    {
        if (Mathf.Abs(obj.transform.position.z - ConstInfo.collisionPositionZ[0]) < ConstInfo.collisionGap
            && GetChildTransform(obj, 0).localScale.x != 0
            && obj.transform.position.x == Player.instance.highlight.transform.position.x)
        {
            GetChildTransform(obj, 0).localScale = Vector3.zero;
            Player.instance.EmptyCollision();
        }
    }



    // 2분의 1 확률 랜덤 함수
    bool YesOrNo() { return Random.Range(0, 10) % 2 == 0; }

    // 리스트에서 랜덤 오브젝트 반환
    GameObject GetRandomFromList(List<GameObject> list) { return list[Random.Range(0, list.Count)]; }

    // 타일이 최초 지나가는 길로 설정된 길인 지 판별
    bool IsPassDirection(GameObject tile) { return tile.tag == "heart-tile" || tile.tag == "empty-tile-pass"; }

    // 자식 오브젝트 tramsform 반환
    Transform GetChildTransform(GameObject obj, int index) { return obj.transform.GetChild(index).gameObject.transform; }

}
