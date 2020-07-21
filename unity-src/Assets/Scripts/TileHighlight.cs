using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHighlight : MonoBehaviour
{
    public Color primarycolor;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        primarycolor = rend.material.color;
    }


    void Update()
    {

        switch (gameObject.name) {
            case "playerGround1":
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    rend.material.SetColor("_Color", Color.yellow);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    rend.material.SetColor("_Color", primarycolor);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    rend.material.SetColor("_Color", primarycolor);
                }
                break;
            case "playerGround2":
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    rend.material.SetColor("_Color", primarycolor);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    
                    rend.material.SetColor("_Color", Color.yellow);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    rend.material.SetColor("_Color", primarycolor);
                }
                break;
            case "playerGround3":
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    rend.material.SetColor("_Color", primarycolor);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    rend.material.SetColor("_Color", primarycolor);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                   
                    rend.material.SetColor("_Color", Color.yellow);
                }
                break;

        }


    }
}



