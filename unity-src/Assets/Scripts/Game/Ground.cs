using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    void Update()
    {
        if (transform.position.z < -3000)
            transform.position = new Vector3(transform.position.x, transform.position.y, 3000);
        else
            transform.Translate(Vector3.back * Tile.actualSpeed * Time.deltaTime);
    }
}
