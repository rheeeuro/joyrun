using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameScript : MonoBehaviour
{

    public GameObject heartTile;
    public GameObject obstacleTile;
    public GameObject emptyTile;
    public GameObject trapTile;

    public GameObject user;
    public List<GameObject> tiles = new List<GameObject>();
    public List<GameObject> activatedTiles = new List<GameObject>();

    public float left = 118.45f;
    public float center = 132.7f;
    public float right = 147.2f;

    public float speed = 30;



    // Start is called before the first frame update
    void Start()
    {
        heartTile = Resources.Load("Prefabs/heart-tile") as GameObject;
        obstacleTile = Resources.Load("Prefabs/obstacle-tile") as GameObject;
        emptyTile = Resources.Load("Prefabs/empty-tile") as GameObject;
        trapTile = Resources.Load("Prefabs/trap-tile") as GameObject;

        tiles.Add(heartTile);
        tiles.Add(obstacleTile);
        tiles.Add(emptyTile);
        tiles.Add(trapTile);

        user = GameObject.Find("user");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Backspace))
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    activatedTiles.Add(Instantiate(emptyTile, new Vector3(left, 1, 77), emptyTile.transform.rotation));
                    activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(center, 1, 77), emptyTile.transform.rotation));
                    activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(right, 1, 77), emptyTile.transform.rotation));
                    break;
                case 1:
                    activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(left, 1, 77), emptyTile.transform.rotation));
                    activatedTiles.Add(Instantiate(emptyTile, new Vector3(center, 1, 77), emptyTile.transform.rotation));
                    activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(right, 1, 77), emptyTile.transform.rotation));
                    break;
                case 2:
                    activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(left, 1, 77), emptyTile.transform.rotation));
                    activatedTiles.Add(Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(center, 1, 77), emptyTile.transform.rotation));
                    activatedTiles.Add(Instantiate(emptyTile, new Vector3(right, 1, 77), emptyTile.transform.rotation));
                    break;
            }
        }


        for (int i = 0; i < activatedTiles.Count; i++)
        {
            activatedTiles[i].transform.Translate(Vector3.back * speed * Time.deltaTime);
            if (activatedTiles[i].transform.position.z < -100)
            {
                activatedTiles.RemoveAt(i);
            }
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            user.transform.position = new Vector3(left, user.transform.position.y, user.transform.position.z);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            user.transform.position = new Vector3(center, user.transform.position.y, user.transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            user.transform.position = new Vector3(right, user.transform.position.y, user.transform.position.z);
        }
    }
}
