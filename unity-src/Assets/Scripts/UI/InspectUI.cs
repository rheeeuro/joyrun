﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectUI : MonoBehaviour
{
    public static InspectUI instance;
    public int frameChangeCount;
    public float frameTimer;
    public Text frameText;

    void Awake() { instance = this; }
        
    void Start()
    {
        InitialFrameCount();
        InitialText();
    }

    // 정확한 1초당 키넥트정보 변화량을 측정하기 위해 Fixed Update 이용
    private void FixedUpdate()
    {
        HandleDisplay();
        if (Setting.GetDisplayInspect())
            HandleInspectDisplay();
        else
            InitialText();
    }

    // F1 키를 통해 키넥트 프레임 체크 가능
    void HandleDisplay()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Setting.SetDisplayInspect(!Setting.GetDisplayInspect());
            InitialFrameCount();
        }
    }


    void HandleInspectDisplay()
    {
        if (!GameManager.instance.GetKinectState())
            frameText.text = "Kinect found.";
        else
            HandleFrameCheck();


    }

    void HandleFrameCheck() {
        frameTimer += Time.fixedDeltaTime;
        if (frameTimer >= 1)
        {
            frameText.text = frameChangeCount.ToString() + " fps";
            InitialFrameCount();
        }
    }




    void InitialFrameCount() {
        frameChangeCount = 0;
        frameTimer = 0;
    }

    void InitialText() { frameText.text = ""; }
}
