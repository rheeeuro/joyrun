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

    void checkCollision() // 30 36 42 48
    {
        for (int i = 0; i < TileCreate.activatedTiles.Count; i++)
        {
            switch (TileCreate.activatedTiles[i].tag)
            {
                case "heart-tile":

                    if (Mathf.Abs(TileCreate.activatedTiles[i].transform.position.z + 30) < 5
                        && TileCreate.activatedTiles[i].transform.GetChild(0).gameObject.transform.localScale.x != 0
                        && TileCreate.activatedTiles[i].transform.position.x == PlayerMove.player.transform.position.x)
                    {
                        TileCreate.activatedTiles[i].transform.GetChild(0).gameObject.transform.localScale = new Vector3(0, 0, 0);
                        TileCreate.activatedTiles[i].transform.GetChild(1).gameObject.transform.localScale = new Vector3(0, 0, 0);
                        Debug.Log("hp++");

                    } else if (Mathf.Abs(TileCreate.activatedTiles[i].transform.position.z + 36) < 5
                        && TileCreate.activatedTiles[i].transform.GetChild(2).gameObject.transform.localScale.x != 0
                        && TileCreate.activatedTiles[i].transform.position.x == PlayerMove.player.transform.position.x)
                    {
                        TileCreate.activatedTiles[i].transform.GetChild(2).gameObject.transform.localScale = new Vector3(0, 0, 0);
                        TileCreate.activatedTiles[i].transform.GetChild(3).gameObject.transform.localScale = new Vector3(0, 0, 0);
                        Debug.Log("hp++");

                    } else if (Mathf.Abs(TileCreate.activatedTiles[i].transform.position.z + 42) < 5
                        && TileCreate.activatedTiles[i].transform.GetChild(4).gameObject.transform.localScale.x != 0
                        && TileCreate.activatedTiles[i].transform.position.x == PlayerMove.player.transform.position.x)
                    { 
                        TileCreate.activatedTiles[i].transform.GetChild(4).gameObject.transform.localScale = new Vector3(0, 0, 0);
                        TileCreate.activatedTiles[i].transform.GetChild(5).gameObject.transform.localScale = new Vector3(0, 0, 0);
                        Debug.Log("hp++");
                    } else if (Mathf.Abs(TileCreate.activatedTiles[i].transform.position.z + 48) < 5
                        && TileCreate.activatedTiles[i].transform.GetChild(6).gameObject.transform.localScale.x != 0
                        && TileCreate.activatedTiles[i].transform.position.x == PlayerMove.player.transform.position.x)
                    {
                        TileCreate.activatedTiles[i].transform.GetChild(6).gameObject.transform.localScale = new Vector3(0, 0, 0);
                        TileCreate.activatedTiles[i].transform.GetChild(7).gameObject.transform.localScale = new Vector3(0, 0, 0);
                        Debug.Log("hp++");
                    }

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
