using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject heartTile;
    public GameObject obstacleTile;
    public GameObject emptyTile;
    public GameObject trapTile;

    public static List<GameObject> tiles = new List<GameObject>();
    public static List<GameObject> activatedTiles = new List<GameObject>();

    // 지정 위치
    public static float left = 100;
    public static float center = 114;
    public static float right = 128;
    public static float destroyLine = -100;

    // 속도 변수
    public static float initialSpeed = 30;
    public static float speed;
    public static float speedIncrease = 0.1f;

    // 시간 변수
    public static float timer;
    public int delay = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Prefab 불러오기
        heartTile = Resources.Load("Prefabs/heart-tile") as GameObject;
        obstacleTile = Resources.Load("Prefabs/obstacle-tile") as GameObject;
        emptyTile = Resources.Load("Prefabs/empty-tile") as GameObject;
        trapTile = Resources.Load("Prefabs/trap-tile") as GameObject;

        tiles.Add(heartTile);
        tiles.Add(obstacleTile);
        tiles.Add(emptyTile);
        tiles.Add(trapTile);

        // 시간 속도 초기화
        InitializeSpeed();
        timer = 0;
    }


    // Update is called once per frame
    void Update()
    {
        // 일정 시간마다 타일 생성 (createTiles)
        timer += Time.deltaTime;
        if (timer > delay)
        {
            createTiles();
            // 타이머 0으로 초기화 -> 나중에는 초기화 대신 남은시간으로 구현
            timer = 0;
        }

        moveTiles();
        checkCollision();
        speed += speedIncrease;
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
                activatedTiles.Add(Instantiate(emptyTile, new Vector3(left, 0.99f, 80), Player.player.transform.rotation));
                activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(center, 0.99f, 80), Player.player.transform.rotation));
                activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(right, 0.99f, 80), Player.player.transform.rotation));
                break;
            case 1:
                activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(left, 0.99f, 80), Player.player.transform.rotation));
                activatedTiles.Add(Instantiate(emptyTile, new Vector3(center, 0.99f, 80), Player.player.transform.rotation));
                activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(right, 0.99f, 80), Player.player.transform.rotation));
                break;
            case 2:
                activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(left, 0.99f, 80), Player.player.transform.rotation));
                activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(center, 0.99f, 80), Player.player.transform.rotation));
                activatedTiles.Add(Instantiate(emptyTile, new Vector3(right, 0.99f, 80), Player.player.transform.rotation));
                break;
        }
    }

    void moveTiles()
    {
        // 타일 이동 후 끝까지 간 경우 삭제
        for (int i = 0; i < activatedTiles.Count; i++)
        {
            activatedTiles[i].transform.Translate(Vector3.back * speed * Time.deltaTime);
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
                        && activatedTiles[i].transform.position.x == Player.player.transform.position.x)
                    {
                        getChildTransform(activatedTiles[i], 0).localScale = new Vector3(0, 0, 0);
                        getChildTransform(activatedTiles[i], 1).localScale = new Vector3(0, 0, 0);
                        Debug.Log("hp++");

                    }
                    else if (Mathf.Abs(activatedTiles[i].transform.position.z + 36) < 5
                      && getChildTransform(activatedTiles[i], 2).localScale.x != 0
                      && activatedTiles[i].transform.position.x == Player.player.transform.position.x)
                    {
                        getChildTransform(activatedTiles[i], 2).localScale = new Vector3(0, 0, 0);
                        getChildTransform(activatedTiles[i], 3).localScale = new Vector3(0, 0, 0);
                        Debug.Log("hp++");

                    }
                    else if (Mathf.Abs(activatedTiles[i].transform.position.z + 42) < 5
                      && getChildTransform(activatedTiles[i], 4).localScale.x != 0
                      && activatedTiles[i].transform.position.x == Player.player.transform.position.x)
                    {
                        getChildTransform(activatedTiles[i], 4).localScale = new Vector3(0, 0, 0);
                        getChildTransform(activatedTiles[i], 5).localScale = new Vector3(0, 0, 0);
                        Debug.Log("hp++");
                    }
                    else if (Mathf.Abs(activatedTiles[i].transform.position.z + 48) < 5
                      && getChildTransform(activatedTiles[i], 6).localScale.x != 0
                      && activatedTiles[i].transform.position.x == Player.player.transform.position.x)
                    {
                        getChildTransform(activatedTiles[i], 6).localScale = new Vector3(0, 0, 0);
                        getChildTransform(activatedTiles[i], 7).localScale = new Vector3(0, 0, 0);
                        Debug.Log("hp++");
                    }

                    // To Do: 하트 먹는 기능 구현
                    break;
                case "obstacle-tile":
                    if (activatedTiles[i].transform.position.z < -25
                        && activatedTiles[i].transform.position.z > -35
                        && activatedTiles[i].transform.position.x == Player.player.transform.position.x)
                    {
                        Debug.Log("hit by obstacle!");
                        activatedTiles[i].SetActive(false);
                        Destroy(activatedTiles[i]);
                        activatedTiles.RemoveAt(i);
                        InitializeSpeed();
                    }
                    break;
                case "empty-tile":
                    // 점수 증가
                    break;
                case "trap-tile":
                    if (activatedTiles[i].transform.position.z < -25
                        && activatedTiles[i].transform.position.z > -35
                        && activatedTiles[i].transform.position.x == Player.player.transform.position.x)
                    {
                        Debug.Log("fall into a trap!");
                        activatedTiles[i].SetActive(false);
                        Destroy(activatedTiles[i]);
                        activatedTiles.RemoveAt(i);
                        InitializeSpeed();
                    }
                    break;
            }
        }
    }

    Transform getChildTransform(GameObject obj, int index) {
        return obj.transform.GetChild(index).gameObject.transform;
    }

    void playerDamaged() {
        Player.hp = Player.hp / 2;
    }


}
