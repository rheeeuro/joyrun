using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public static GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
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
            player.transform.position = new Vector3(TileCreate.left, player.transform.position.y, player.transform.position.z);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            player.transform.position = new Vector3(TileCreate.center, player.transform.position.y, player.transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.position = new Vector3(TileCreate.right, player.transform.position.y, player.transform.position.z);
        }
    }
}
