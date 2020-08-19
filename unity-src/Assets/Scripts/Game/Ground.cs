using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    GameObject track;
    GameObject ground;
    GameObject sideTrack1;
    GameObject sideTrack2;

    float groundOffset;
    float trackOffset;
    float speed;

    void Start() {
        ground = GameObject.Find("ground");
        track = GameObject.Find("track");
        sideTrack1 = GameObject.Find("trackSide1");
        sideTrack2 = GameObject.Find("trackSide2");
        groundOffset = 0;
        trackOffset = 0;
        speed = 0;
    }

    void Update()
    {
        speed = Tile.actualSpeed;
        HandleMeshs();
        /**
        HandleSideTrack(sideTrack1);
        HandleSideTrack(sideTrack2);
        FixPosition();
        **/
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
    /**
    void HandleSideTrack() {
        obj.transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (obj.transform.position.z < -3000)
        {
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z + 6000);
            side = !side;
        }
    }
    **/
}
