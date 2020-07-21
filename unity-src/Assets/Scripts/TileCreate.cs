using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreate : MonoBehaviour
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
    }

    void createTiles()
    {
        // 랜덤으로 한 구역은 빈 타일
        switch (Random.Range(0, 3))
        {
            case 0:
                activatedTiles.Add(Instantiate(emptyTile, new Vector3(left, 0.99f, 80), PlayerMove.player.transform.rotation));
                activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(center, 0.99f, 80), PlayerMove.player.transform.rotation));
                activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(right, 0.99f, 80), PlayerMove.player.transform.rotation));
                break;
            case 1:
                activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(left, 0.99f, 80), PlayerMove.player.transform.rotation));
                activatedTiles.Add(Instantiate(emptyTile, new Vector3(center, 0.99f, 80), PlayerMove.player.transform.rotation));
                activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(right, 0.99f, 80), PlayerMove.player.transform.rotation));
                break;
            case 2:
                activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(left, 0.99f, 80), PlayerMove.player.transform.rotation));
                activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(center, 0.99f, 80), PlayerMove.player.transform.rotation));
                activatedTiles.Add(Instantiate(emptyTile, new Vector3(right, 0.99f, 80), PlayerMove.player.transform.rotation));
                break;
        }
    }
}
