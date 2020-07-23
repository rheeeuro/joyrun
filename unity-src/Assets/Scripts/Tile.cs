using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject heartTile;
    public GameObject obstacleTile;
    public GameObject emptyTile;
    public GameObject trapTile;

    public static List<GameObject> randomTiles = new List<GameObject>();
    public static List<GameObject> goodTiles = new List<GameObject>();
    public static List<GameObject> badTiles = new List<GameObject>();
    public static List<GameObject> activatedTiles = new List<GameObject>();

    // 지정 위치
    public static float left = 100;
    public static float center = 114;
    public static float right = 128;
    public static float destroyLine = -65;

    // 속도 변수
    public static float initialSpeed = 25;
    public static float speed;
    public static float speedIncrease = 0.1f;
    public static float extraSpeed = 0;
    public static float actualSpeed = 0;

    // 시간 변수
    public float delay = 2;
    public int createTileCount = 0;
    public float tileDistance = 60;





    // Start is called before the first frame update
    void Start()
    {
        // Prefab 불러오기
        heartTile = Resources.Load("Prefabs/heart-tile") as GameObject;
        obstacleTile = Resources.Load("Prefabs/obstacle-tile") as GameObject;
        emptyTile = Resources.Load("Prefabs/empty-tile") as GameObject;
        trapTile = Resources.Load("Prefabs/trap-tile") as GameObject;

        randomTiles.Add(obstacleTile);
        randomTiles.Add(emptyTile);
        randomTiles.Add(trapTile);

        goodTiles.Add(emptyTile);
        goodTiles.Add(heartTile);

        badTiles.Add(obstacleTile);
        badTiles.Add(trapTile);

        // 시간 속도 초기화
        InitializeSpeed();
        createTileCount = 0;
        extraSpeed = 0;


        createTiles();
        createTileCount++;

    }


    // Update is called once per frame
    void Update()
    {
        if (activatedTiles.Count > 0)
        {
            if (activatedTiles[activatedTiles.Count - 1].transform.position.z < 80 - tileDistance)
            {
                createTiles();
                createTileCount++;
                if (createTileCount == 10)
                {
                    delay -= 0.1f;
                    createTileCount = 0;
                }
            }
        }
        else {
            createTiles();
            createTileCount++;
        }

        moveTiles();
        checkCollision();
        
    }

    void InitializeSpeed()
    {
        speed = initialSpeed;
    }

    void createTiles()
    {
        // 랜덤으로 한 구역은 빈 타일
        switch (Random.Range(0, 3))
        {
            case 0:
                createOne(goodTiles[Random.Range(0, goodTiles.Count)], left);
                if (Random.Range(0, 10) % 2 == 0)
                {
                    createOne(randomTiles[Random.Range(0, randomTiles.Count)], center);
                    createOne(badTiles[Random.Range(0, badTiles.Count)], right);
                }
                else {
                    createOne(badTiles[Random.Range(0, badTiles.Count)], center);
                    createOne(randomTiles[Random.Range(0, randomTiles.Count)], right);
                }
                break;
            case 1:
                createOne(goodTiles[Random.Range(0, goodTiles.Count)], center);
                if (Random.Range(0, 10) % 2 == 0)
                {
                    createOne(randomTiles[Random.Range(0, randomTiles.Count)], left);
                    createOne(badTiles[Random.Range(0, badTiles.Count)], right);
                }
                else
                {
                    createOne(badTiles[Random.Range(0, badTiles.Count)], left);
                    createOne(randomTiles[Random.Range(0, randomTiles.Count)], right);
                }
                break;
            case 2:
                createOne(goodTiles[Random.Range(0, goodTiles.Count)], right);
                if (Random.Range(0, 10) % 2 == 0)
                {
                    createOne(randomTiles[Random.Range(0, randomTiles.Count)], left);
                    createOne(badTiles[Random.Range(0, badTiles.Count)], center);
                }
                else
                {
                    createOne(badTiles[Random.Range(0, badTiles.Count)], left);
                    createOne(randomTiles[Random.Range(0, randomTiles.Count)], center);
                }
                break;
        }
    }

    void moveTiles()
    {
        actualSpeed = (tileDistance / delay) + extraSpeed;
        if (actualSpeed > 90) {
            actualSpeed = 90;
        }
        // 타일 이동 후 끝까지 간 경우 삭제
        for (int i = 0; i < activatedTiles.Count; i++)
        {
            activatedTiles[i].transform.Translate(Vector3.back * actualSpeed * Time.deltaTime);

            if (activatedTiles[i].transform.position.z < destroyLine)
            {
                activatedTiles[i].SetActive(false);
                Destroy(activatedTiles[i]);
                activatedTiles.RemoveAt(i);
            }
        }
    }

    void checkCollision() // 하트 기준 좌표: 타일 + 30, 36, 42, 48
    {
        for (int i = 0; i < activatedTiles.Count; i++)
        {
            switch (activatedTiles[i].tag)
            {
                case "heart-tile":

                    if (Mathf.Abs(activatedTiles[i].transform.position.z + 30) < 5
                        && getChildTransform(activatedTiles[i], 0).localScale.x != 0
                        && activatedTiles[i].transform.position.x == Player.player.transform.position.x
                        && !Player.isJumping)
                    {
                        getChildTransform(activatedTiles[i], 0).localScale = new Vector3(0, 0, 0);
                        getChildTransform(activatedTiles[i], 1).localScale = new Vector3(0, 0, 0);
                        Player.instance.meetHeart();
                    }
                    else if (Mathf.Abs(activatedTiles[i].transform.position.z + 36) < 5
                      && getChildTransform(activatedTiles[i], 2).localScale.x != 0
                      && activatedTiles[i].transform.position.x == Player.player.transform.position.x
                      && !Player.isJumping)
                    {
                        getChildTransform(activatedTiles[i], 2).localScale = new Vector3(0, 0, 0);
                        getChildTransform(activatedTiles[i], 3).localScale = new Vector3(0, 0, 0);
                        Player.instance.meetHeart();
                    }
                    else if (Mathf.Abs(activatedTiles[i].transform.position.z + 42) < 5
                      && getChildTransform(activatedTiles[i], 4).localScale.x != 0
                      && activatedTiles[i].transform.position.x == Player.player.transform.position.x
                      && !Player.isJumping)
                    {
                        getChildTransform(activatedTiles[i], 4).localScale = new Vector3(0, 0, 0);
                        getChildTransform(activatedTiles[i], 5).localScale = new Vector3(0, 0, 0);
                        Player.instance.meetHeart();
                    }
                    else if (Mathf.Abs(activatedTiles[i].transform.position.z + 48) < 5
                      && getChildTransform(activatedTiles[i], 6).localScale.x != 0
                      && activatedTiles[i].transform.position.x == Player.player.transform.position.x
                      && !Player.isJumping)
                    {
                        getChildTransform(activatedTiles[i], 6).localScale = new Vector3(0, 0, 0);
                        getChildTransform(activatedTiles[i], 7).localScale = new Vector3(0, 0, 0);
                        Player.instance.meetHeart();
                    }
                    break;
                case "obstacle-tile":
                    if (Mathf.Abs(activatedTiles[i].transform.position.z + 30) < 5
                        && getChildTransform(activatedTiles[i], 0).localScale.x != 0
                        && activatedTiles[i].transform.position.x == Player.player.transform.position.x
                        )
                    {
                        if (Player.isJumping)
                        {
                            if (getChildTransform(activatedTiles[i], 1).localScale.x != 0) {
                                Player.instance.meetEmpty();
                                getChildTransform(activatedTiles[i], 1).localScale = new Vector3(0, 0, 0);
                            }
                        }
                        else {
                            getChildTransform(activatedTiles[i], 0).localScale = new Vector3(0, 0, 0);
                            InitializeSpeed();
                            Player.instance.meetObstacle();
                        }

                    }
                    break;
                case "empty-tile":
                    if (Mathf.Abs(activatedTiles[i].transform.position.z + 30) < 5
                        && getChildTransform(activatedTiles[i], 0).localScale.x != 0
                        && activatedTiles[i].transform.position.x == Player.player.transform.position.x)
                    {
                        getChildTransform(activatedTiles[i], 0).localScale = new Vector3(0, 0, 0);                
                        Player.instance.meetEmpty();
                    }
                    break;
                case "trap-tile":
                    if (Mathf.Abs(activatedTiles[i].transform.position.z + 30) < 5
                        && getChildTransform(activatedTiles[i], 0).localScale.x != 0
                        && activatedTiles[i].transform.position.x == Player.player.transform.position.x
                        )
                    {
                        if (Player.isJumping)
                        {
                            if (getChildTransform(activatedTiles[i], 1).localScale.x != 0)
                            {
                                Player.instance.meetEmpty();
                                getChildTransform(activatedTiles[i], 1).localScale = new Vector3(0, 0, 0);
                            }
                        }
                        else
                        {
                            getChildTransform(activatedTiles[i], 0).localScale = new Vector3(0, 0, 0);
                            InitializeSpeed();
                            Player.instance.meetObstacle();
                        }
                    }
                    break;
            }
        }
    }

    Transform getChildTransform(GameObject obj, int index) {
        return obj.transform.GetChild(index).gameObject.transform;
    }


    void createOne(GameObject tile, float direction) {
        activatedTiles.Add(Instantiate(tile, new Vector3(direction, 1, 80), Player.player.transform.rotation));
    }


}
