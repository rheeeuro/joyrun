using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMove : MonoBehaviour
{

    // 사용자 지정 값
    public static float initialSpeed = 30;
    public static float speed;
    public static float speedIncrease = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        speed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        moveTiles();
        // 속도 증가
        speed += speedIncrease;
    }


    void moveTiles()
    {
        // 타일 이동 후 끝까지 간 경우 삭제
        for (int i = 0; i < TileCreate.activatedTiles.Count; i++)
        {
            TileCreate.activatedTiles[i].transform.Translate(Vector3.back * speed * Time.deltaTime);
            if (TileCreate.activatedTiles[i].transform.position.z < -100)
            {
                TileCreate.activatedTiles[i].SetActive(false);
                Destroy(TileCreate.activatedTiles[i]);
                TileCreate.activatedTiles.RemoveAt(i);
            }
        }
    }
}
