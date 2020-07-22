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

        switch (gameObject.name) {
            case "leftTile-highlight":
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
            case "centerTile-highlight":
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
            case "rightTile-highlight":
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



