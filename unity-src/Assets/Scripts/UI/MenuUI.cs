﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 메뉴 상태 (선택 하이라이트, 0: 게임시작, 1: 환경설정, 2: 랭킹, 3: 게임종료)
public enum MenuChoice
{
    start,
    setting,
    ranking,
    quit
}

public class MenuUI : MonoBehaviour
{


    // 인스턴스 및 현재 선택한 메뉴 항목
    public static MenuUI instance;
    private MenuChoice currentMenuChoice;

    //색상 스프라이트
    public Sprite selected;
    public Sprite unSelected;

    // 메뉴 버튼 변수
    public GameObject startButton;
    public GameObject settingButton;
    public GameObject rankingButton;
    public GameObject quitButton;

    void Awake() { instance = this; }

    // 초기에는 게임시작 버튼 하이라이트
    void Start() {
        currentMenuChoice = MenuChoice.start;
        Show();
    }


    // 메뉴 UI 보여주기
    public void Show()
    {
        GameManager.instance.SetGameState(GameState.menu);
        transform.gameObject.SetActive(true);
        GetComponent<Animation>().Play("ShowGuide");
    }



    void Update()
    {
        UnselectButtons();
        HandleMenuChoice();
    }

    // 모든 버튼을 선택 해제
    void UnselectButtons()
    {
        startButton.GetComponent<UnityEngine.UI.Image>().sprite = unSelected;
        settingButton.GetComponent<UnityEngine.UI.Image>().sprite = unSelected;
        rankingButton.GetComponent<UnityEngine.UI.Image>().sprite = unSelected;
        quitButton.GetComponent<UnityEngine.UI.Image>().sprite = unSelected;

    }

    // 메뉴 상태에 따라 버튼 선택 (색상 변경) 
    void HandleMenuChoice()
    {
        switch (currentMenuChoice)
        {
            case MenuChoice.start:
                startButton.GetComponent<UnityEngine.UI.Image>().sprite = selected;
                break;
            case MenuChoice.setting:
                settingButton.GetComponent<UnityEngine.UI.Image>().sprite = selected;
                break;
            case MenuChoice.ranking:
                rankingButton.GetComponent<UnityEngine.UI.Image>().sprite = selected;
                break;
            case MenuChoice.quit:
                quitButton.GetComponent<UnityEngine.UI.Image>().sprite = selected;
                break;
        }
    }



    // 위 방향 버튼을 누른 경우
    public void HandleUp()
    {
        if (currentMenuChoice == MenuChoice.start)
            currentMenuChoice = MenuChoice.quit;
        else
            currentMenuChoice--;
    }

    // 아래 방향 버튼을 누른 경우
    public void HandleDown()
    {
        if (currentMenuChoice == MenuChoice.quit)
            currentMenuChoice = MenuChoice.start;
        else
            currentMenuChoice++;
    }

    // 확인 버튼을 누른 경우
    public void HandleConfirm()
    {
        switch (currentMenuChoice)
        {
            case MenuChoice.start:
                HandleStart();
                break;
            case MenuChoice.setting:
                HandleSetting();
                break;
            case MenuChoice.ranking:
                HandleRanking();
                break;
            case MenuChoice.quit:
                HandleQuit();
                break;
        }
    }

    // 시작 상태에서 확인 버튼을 눌렀을 경우
    public void HandleStart()
    {
        SceneManager.LoadScene("Game");
    }

    // 환경설정을 고르고 확인 버튼을 누른 경우
    public void HandleSetting()
    {
        transform.gameObject.SetActive(false);
        SettingUI.instance.Show();
    }

    // 랭킹을 고르고 확인 버튼을 누른 경우
    public void HandleRanking()
    {
        transform.gameObject.SetActive(false);
        RankingUI.instance.Show();
    }

    // 게임 종료를 고르고 확인 버튼을 누른 경우
    public void HandleQuit()
    {
        Application.Quit();
    }
}
