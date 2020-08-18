using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    GameObject leftTile;
    GameObject centerTile;
    GameObject rightTile;
    GameObject ground;

    float offset;

    void Start() {
        ground = GameObject.Find("ground");

        leftTile = GameObject.Find("leftTile");
        centerTile = GameObject.Find("centerTile");
        rightTile = GameObject.Find("rightTile");
        offset = 0;
    }

    void Update()
    {
        HandleMeshs();
    }

    void HandleMeshs() {
        HandleMesh(ground);
    }


    void HandleMesh(GameObject obj)
    {
        offset -= (Tile.actualSpeed / 9f) * Time.deltaTime;
        if (offset < 0)
            offset += 1;

        obj.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
