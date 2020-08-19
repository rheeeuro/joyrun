using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    GameObject track;
    GameObject ground;

    float groundOffset;
    float trackOffset;

    void Start() {
        ground = GameObject.Find("ground");

        track = GameObject.Find("track");
        groundOffset = 0;
        trackOffset = 0;
    }

    void Update()
    {
        HandleMeshs();
    }

    void HandleMeshs() {
        HandleMesh(ground);
        HandleTrackMesh(track);
    }


    void HandleMesh(GameObject obj)
    {
        groundOffset -= (Tile.actualSpeed / (obj.GetComponent<Collider>().bounds.size.z / 50)) * Time.deltaTime;
        if (groundOffset < 0)
            groundOffset += 1;

        obj.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, groundOffset));
    }

    void HandleTrackMesh(GameObject obj)
    {
        trackOffset -= (Tile.actualSpeed / obj.GetComponent<Collider>().bounds.size.z) * Time.deltaTime;
        if (trackOffset < 0)
            trackOffset += 1;

        obj.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(trackOffset, 0));
    }
}
