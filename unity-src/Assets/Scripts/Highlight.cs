using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public Color primarycolor;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        primarycolor = rend.material.color;
        if (gameObject.name == "centerTile-highlight") {
            rend.material.SetColor("_Color", Color.yellow);
        }
    }


    void Update()
    {

            switch (gameObject.name)
            {
                case "leftTile-highlight":
                    if (Player.playerPosition.transform.position.x == Tile.left)
                    {
                        rend.material.SetColor("_Color", Color.yellow);
                    }

                    if (Player.playerPosition.transform.position.x == Tile.center)
                    {
                        rend.material.SetColor("_Color", primarycolor);
                    }
                    if (Player.playerPosition.transform.position.x == Tile.right)
                    {
                        rend.material.SetColor("_Color", primarycolor);
                    }
                    break;
                case "centerTile-highlight":
                    if (Player.playerPosition.transform.position.x == Tile.left)
                    {
                        rend.material.SetColor("_Color", primarycolor);
                    }

                    if (Player.playerPosition.transform.position.x == Tile.center)
                    {

                        rend.material.SetColor("_Color", Color.yellow);
                    }
                    if (Player.playerPosition.transform.position.x == Tile.right)
                    {
                        rend.material.SetColor("_Color", primarycolor);
                    }
                    break;
                case "rightTile-highlight":
                    if (Player.playerPosition.transform.position.x == Tile.left)
                    {
                        rend.material.SetColor("_Color", primarycolor);
                    }

                    if (Player.playerPosition.transform.position.x == Tile.center)
                    {
                        rend.material.SetColor("_Color", primarycolor);
                    }
                    if (Player.playerPosition.transform.position.x == Tile.right)
                    {

                        rend.material.SetColor("_Color", Color.yellow);
                    }
                    break;

            }
        }
        



}



