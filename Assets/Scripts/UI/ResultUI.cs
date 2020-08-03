﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResultUI : MonoBehaviour
{
    public static ResultUI instance;

    // 결과 텍스트 변수
    public Text maxCombo;
    public Text playTime;
    public Text point;

    void Awake() { 
        instance = this;
        
    }

    void Start()
    {
        transform.gameObject.SetActive(false);
    }

    // 보이도록 설정
    public void Show()
    {
        GameManager.instance.Result();
        transform.gameObject.SetActive(true);
    }

    // 다음페이지 버튼을 누른 경우
    public void HandleNextPage()
    {
        GameManager.instance.MyRank();
        transform.gameObject.SetActive(false);
        MyRankUI.instance.Show();
    }
}