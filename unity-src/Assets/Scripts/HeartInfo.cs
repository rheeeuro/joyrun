using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartInfo : MonoBehaviour
{
    Vector3 thisTilePos;

    public int life = 2;
    public int Combo = 2;
    public static HeartInfo instance;

    private void Awake()
    {
        instance = this;
        thisTilePos = transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
