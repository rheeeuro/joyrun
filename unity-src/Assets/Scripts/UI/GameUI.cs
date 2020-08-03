using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;
    public bool isPausing;

    // 텍스트 변수 선언
    public Text comboText;
    public Text hpText;
    public Text timerText;

    // 타이머  변수 선언
    public static float timer;
    public float comboTimer;


    public static int myRank;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isPausing = false;
        myRank = 9999;
        comboTimer = 0;
        timer = ConstInfo.gameTime;
    }

    void FixedUpdate()
    {
        HandleTimer();
        HandleText();
    }

    // 상태 텍스트 설정 (체력, 타이머, 콤보)
    void HandleText()
    {
        hpText.text = "HP : " + Player.instance.hp.ToString();
        timerText.text = "Timer : " + timer.ToString("00.00");

        if (comboTimer > 0)
            comboTimer -= Time.deltaTime;
        else
            comboText.text = "";
    }

    // 타이머 변수 설정
    void HandleTimer()
    {
        if (GameManager.instance.GetGameState() == GameState.game)
            timer = Mathf.Round((timer - Time.fixedDeltaTime) * 100) / 100;
        if (timer < 0)
            timer = 0;
    }

    // 콤보 변경 시 (증가 혹은 초기화)
    public void ChangeCombo(int newCombo)
    {
        if (Player.instance.maxCombo <= newCombo)
            Player.instance.maxCombo = newCombo;

        comboText.text = newCombo.ToString() + " COMBO";
        comboTimer = 0.7f;
    }

    // 랭킹 등록 알고리즘
    public void InsertRank(int score)
    {
        for (int i = 0; i < 5; i++)
        {
            if (score > PlayerPrefs.GetInt(i.ToString()))
            {
                for (int j = 4 - 1; j < 0; j--)
                {
                    PlayerPrefs.SetInt(j.ToString(), PlayerPrefs.GetInt((j - 1).ToString()));
                    // 스코어가 1등 기준으로 PlayerPrefs의 Key값(j의 위치값 4(5등))을 j-1위치의 값(4등)으로 바꾼다.
                    // j가 1씩 줄어들면서 윗 순번도 차례대로 비교해서 바꾼다.
                }
                PlayerPrefs.SetInt(i.ToString(), score); //현재 등수를 현재 스코어 값으로 바꾸어 준다.
                myRank = i + 1;

                if (myRank <= 5)
                    MyRankUI.instance.myRank.text = "내 순위 : " + myRank.ToString();


                MyRankUI.instance.UpdateRanking();
                break; // 종료


            }

            if (myRank > 5)
                MyRankUI.instance.myRank.text = "5위 미만입니다.";
        }
    }

    // UI 보여주기
    public void Show()
    {
        GameManager.instance.Game();
        transform.gameObject.SetActive(true);
    }

    // 일시정지 설정, 해제
    public void Pause()
    {
        if (isPausing)
            GameManager.instance.Game();
        else
            GameManager.instance.Pause();

        isPausing = !isPausing;
    }

    // 게임이 종료된 경우
    public void HandleGameEnd() {
        transform.gameObject.SetActive(false);
        ResultUI.instance.Show();
    }

    // 새 게임 버튼을 누른 경우
    public void HandleNewGame()
    {
        SceneManager.LoadScene("Game");
    }

    // 메뉴로 버튼을 누른 경우
    public void HandleToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}


