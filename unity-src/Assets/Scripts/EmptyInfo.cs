using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyInfo : MonoBehaviour
{
    Vector3 thisTilePos;

    float damage = 0;
    float Combo = 1;
    public static EmptyInfo instance;

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
