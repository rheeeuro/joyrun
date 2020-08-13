using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class Ground : MonoBehaviour
{
    GameObject ground1;
    GameObject ground2;
    float speed;
    bool side;

    void Start() {
        ground1 = GameObject.Find("ground 1");
        ground2 = GameObject.Find("ground 2");
        speed = 0;
        side = true;
    }

    void Update()
    {
        speed = Tile.actualSpeed;
        HandleGround(ground1);
        HandleGround(ground2);
        FixPosition();
    }

    void HandleGround(GameObject obj) {
        obj.transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (obj.transform.position.z < -3000)
        {
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z + 6000);
            side = !side;
        }
    }

    void FixPosition() {
        if (side)
            ground2.transform.position = new Vector3(ground2.transform.position.x, ground2.transform.position.y, ground1.transform.position.z - 3000);
        else
            ground2.transform.position = new Vector3(ground2.transform.position.x, ground2.transform.position.y, ground1.transform.position.z + 3000);
    }
}
