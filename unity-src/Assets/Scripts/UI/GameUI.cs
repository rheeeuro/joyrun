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
        if (Setting.GetCurrentTimeState() == TimeState.normal)
            HandleTimer();
        HandleText();
    }

    // 상태 텍스트 설정 (체력, 타이머, 콤보)
    void HandleText()
    {
        if (Setting.GetCurrentHpState() == HpState.normal)
            hpText.text = "HP : " + Player.instance.hp.ToString();
        else if (Setting.GetCurrentHpState() == HpState.immortal)
            hpText.text = "Immortal Mode";

        if (Setting.GetCurrentTimeState() == TimeState.normal)
            timerText.text = "Timer : " + timer.ToString("00.00");
        else if (Setting.GetCurrentTimeState() == TimeState.infinite)
            timerText.text = "Infinite Mode";

        if (comboTimer > 0)
            comboTimer -= Time.deltaTime;
        else
            comboText.text = "";
    }

    // 타이머 변수 설정
    void HandleTimer()
    {
        if (GameManager.instance.GetGameState() == GameState.game && Setting.GetCurrentTimeState() == TimeState.normal)
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
        List<int> scores = new List<int>();
        scores.Add(-1 * score);
        for (int i = 0; i < 5; i++)
            scores.Add(-1 * PlayerPrefs.GetInt(i.ToString()));
        scores.Sort();
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), scores[i] * -1);

            if (scores[i] == -1 * score)
                myRank = i+1;
        }

        if(myRank <= 5)
            MyRankUI.instance.myRank.text = "내 순위 : " + myRank.ToString();
        else
            MyRankUI.instance.myRank.text = "순위권에 들지 못했습니다.";

    }

    // UI 보여주기
    public void Show()
    {
        GameManager.instance.SetGameState(GameState.game);
        transform.gameObject.SetActive(true);
    }

    // 일시정지 설정, 해제
    public void Pause()
    {
        if (isPausing)
            GameManager.instance.SetGameState(GameState.game);
        else
            GameManager.instance.SetGameState(GameState.pause);

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


