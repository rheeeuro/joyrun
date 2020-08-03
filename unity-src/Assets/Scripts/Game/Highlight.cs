using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public Color primarycolor;
    Renderer rend;

    // 초기 색상 저장
    void Start()
    {
        rend = GetComponent<Renderer>();
        primarycolor = rend.material.color;
        if (gameObject.name == "centerTile-highlight")
            rend.material.SetColor("_Color", Color.yellow);
    }

    // 하이라이트 타일 색상 업데이트
    void Update()
    {
        rend.material.SetColor("_Color", primarycolor);

        switch (gameObject.name)
        {
            case "leftTile-highlight":
                if (Player.highlight.transform.position.x == ConstInfo.left)
                    rend.material.SetColor("_Color", Color.yellow);
                break;
            case "centerTile-highlight":
                if (Player.highlight.transform.position.x == ConstInfo.center)
                    rend.material.SetColor("_Color", Color.yellow);
                break;
            case "rightTile-highlight":
                if (Player.highlight.transform.position.x == ConstInfo.right)
                    rend.material.SetColor("_Color", Color.yellow);
                break;
        }
    }
        



}



