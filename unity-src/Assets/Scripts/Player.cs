using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static GameObject player;
    public static int hp;
    public static int score;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        hp = 100;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        handleKeyboard();
    }


    void handleKeyboard()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.position = new Vector3(Tile.left, player.transform.position.y, player.transform.position.z);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            player.transform.position = new Vector3(Tile.center, player.transform.position.y, player.transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.position = new Vector3(Tile.right, player.transform.position.y, player.transform.position.z);
        }
    }
}
