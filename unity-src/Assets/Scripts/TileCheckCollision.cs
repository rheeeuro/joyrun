using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCheckCollision : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkCollision();
    }

    void checkCollision()
    {
        for (int i = 0; i < TileCreate.activatedTiles.Count; i++)
        {
            switch (TileCreate.activatedTiles[i].tag)
            {
                case "heart-tile":
                    if (TileCreate.activatedTiles[i].transform.position.z < -25
                        && TileCreate.activatedTiles[i].transform.position.x == PlayerMove.player.transform.position.x)
                    {
                        Debug.Log("hp ++");
                        TileCreate.activatedTiles[i].SetActive(false);
                        Destroy(TileCreate.activatedTiles[i]);
                        TileCreate.activatedTiles.RemoveAt(i);
                    }
                    // 점수 증가
                    // To Do: 하트 먹는 기능 구현
                    break;
                case "obstacle-tile":
                    if (TileCreate.activatedTiles[i].transform.position.z < -25
                        && TileCreate.activatedTiles[i].transform.position.z > -35
                        && TileCreate.activatedTiles[i].transform.position.x == PlayerMove.player.transform.position.x)
                    {
                        Debug.Log("hit by obstacle!");
                        TileCreate.activatedTiles[i].SetActive(false);
                        Destroy(TileCreate.activatedTiles[i]);
                        TileCreate.activatedTiles.RemoveAt(i);
                        TileMove.speed = TileMove.initialSpeed;
                    }
                    break;
                case "empty-tile":
                    // 점수 증가
                    break;
                case "trap-tile":
                    if (TileCreate.activatedTiles[i].transform.position.z < -25
                        && TileCreate.activatedTiles[i].transform.position.z > -35
                        && TileCreate.activatedTiles[i].transform.position.x == PlayerMove.player.transform.position.x)
                    {
                        Debug.Log("fall into a trap!");
                        TileCreate.activatedTiles[i].SetActive(false);
                        Destroy(TileCreate.activatedTiles[i]);
                        TileCreate.activatedTiles.RemoveAt(i);
                        TileMove.speed = TileMove.initialSpeed;
                    }
                    break;
            }
        }
    }
}
