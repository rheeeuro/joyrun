using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // 인스턴스 및 일시정지 상태 변수 선언
    public static GameUI instance;
    public bool isPausing;

    // 텍스트 , 체력 게이지 변수 선언
    public Text comboText;
    public Text timerText;
    public Text speedText;
    public Text countDownTimer;
    public Text timeIncreaseText;
    public Text HpIncreaseText;
    public UIBarScript barHp;

    // 타이머  변수 선언
    public float timer;
    public float comboDisplayTimer;
    public float damageEffectTimer;

    public int balloonCount;

    void Awake() { instance = this; }

    void Start()
    {
        isPausing = false;
        InitialGameTimer();
        Show();
    }

    // 게임 타이머 초기화
    void InitialGameTimer() 
    {
        comboDisplayTimer = 0;
        damageEffectTimer = 0;
        balloonCount = 0;
        timer = ConstInfo.gameTime;
    }



    // UI 보여주기
    public void Show()
    {
        GameManager.instance.SetGameState(GameState.game);
        transform.gameObject.SetActive(true);
    }



    // 정확한 시간 측정을 위해 FixedUpdate 사용
    void FixedUpdate()
    {
        if (Setting.GetCurrentTimeState() == TimeState.normal)
            TimeDecrease();
        if (GameManager.instance.GetGameState() == GameState.game)
            HandleUI();
    }

    // 타이머 변수 설정
    void TimeDecrease()
    {
        if (GameManager.instance.GetGameState() == GameState.game && Setting.GetCurrentTimeState() == TimeState.normal)
            timer = Mathf.Round((timer - Time.fixedDeltaTime) * 100) / 100;
        if (timer < 0)
            timer = 0;
    }

    // 게임 UI 업데이트
    void HandleUI()
    {
        HandleGameText();
        barHp.UpdateValue(Player.instance.hp, ConstInfo.maxHp);
        ShowDamageEffect();
    }

    // 상태 텍스트 설정 (시간, 콤보, 속도)
    void HandleGameText()
    {
        HandleTimeText();
        HandleComboText();
        HandleSpeedText();
        HandleTimeIncrease();
        HandleHpIncrease();
    }



    // 속도 텍스트 업데이트
    void HandleSpeedText() {
        speedText.text = ActualSpeedToDisplaySpeed(Tile.actualSpeed).ToString("#0.00") + " km/s";
    }

    // 타일 실제 속도를 UI상의 속도로 변환 (30 ~ 90 -> 5km/s ~ 20km/s)
    float ActualSpeedToDisplaySpeed(float actualSpeed) {
        return Mathf.Round((((actualSpeed - ConstInfo.actualSpeedStart) / 4) + 5) * 100) / 100;
    }



    // 콤보 표시 타이머에 따른 콤보 텍스트 업데이트 (ConstInfo.comboDisplayTime 에 따른)
    void HandleComboText()
    {
        if (comboDisplayTimer > 0)
            comboDisplayTimer -= Time.fixedDeltaTime;
        else
            comboText.text = "";
    }



    // 시간 설정에 따른 시간 텍스트 업데이트
    void HandleTimeText() {
        if (Setting.GetCurrentTimeState() == TimeState.normal)
            timerText.text = timer.ToString("Time : 00.00");
        else if (Setting.GetCurrentTimeState() == TimeState.infinite)
            timerText.text = "Infinite";
        if (timer < 5)
            HandleCountDownText();
    }

    // 5초 이하로 남았을 경우 카운트다운 텍스트 업데이트
    void HandleCountDownText() {
        if (timer > 0)
            countDownTimer.text = Mathf.Floor(timer + 1).ToString();
        else
            countDownTimer.text = "";
    }



    // 피격된 경우 (이펙트 생성)
    public void ShowDamageEffect()
    {
        if (damageEffectTimer > 0)
        {
            GetComponent<Image>().color = new Color(1, 0, 0, 0.3f);
            damageEffectTimer -= Time.fixedDeltaTime;
        }
        else
            GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    // 콤보 변경된 경우 (증가)
    public void ChangeCombo(int newCombo)
    {
        if (Player.instance.maxCombo <= newCombo)
            Player.instance.maxCombo = newCombo;
        comboText.text = newCombo.ToString() + " COMBO";
        comboDisplayTimer = ConstInfo.comboDisplayTime;
    }

    public void ShowTimeIncrease() {
        timeIncreaseText.color = new Color(1, 1, 1, 1);
    } 
    
    public void ShowHpIncrease() {
        HpIncreaseText.color = new Color(1, 1, 1, 1);
    }



    public void HandleTimeIncrease() {
        if (timeIncreaseText.color.a > 0.3)
            timeIncreaseText.color = new Color(1, 1, 1, timeIncreaseText.color.a - ConstInfo.timeIncreaseTextAlphaDecrease);
        else
            timeIncreaseText.color = new Color(1, 1, 1, 0);
        timeIncreaseText.GetComponent<Outline>().effectColor = new Color(0, 0, 0, timeIncreaseText.color.a / 2);
        timerText.color = new Color(1 - (timeIncreaseText.color.a), 1, 1 - (timeIncreaseText.color.a));
    }

    public void HandleHpIncrease()
    {
        if (HpIncreaseText.color.a > 0.3)
            HpIncreaseText.color = new Color(1, 1, 1, HpIncreaseText.color.a - ConstInfo.timeIncreaseTextAlphaDecrease);
        else
            HpIncreaseText.color = new Color(1, 1, 1, 0);
        HpIncreaseText.GetComponent<Outline>().effectColor = new Color(0, 0, 0, HpIncreaseText.color.a / 2);
    }



    // 일시정지 설정, 해제 (점프 시 점프상태 조기화)
    public void Pause()
    {
        if (isPausing)
            GameManager.instance.SetGameState(GameState.game);
        else
            GameManager.instance.SetGameState(GameState.pause);
        isPausing = !isPausing;
        Player.instance.InitialJumpState();
    }

    // 새 게임 버튼을 누른 경우
    public void HandleNewGame() { SceneManager.LoadScene("Game"); }

    // 메뉴로 버튼을 누른 경우
    public void HandleToMenu() { SceneManager.LoadScene("Menu"); }



    // 게임이 종료된 경우
    public void HandleGameEnd()
    {
        ResultUI.instance.Show();
        transform.gameObject.SetActive(false);
    }

    // 랭킹 등록 알고리즘
    public void InsertRank(int score)
    {
        int myRank = 0;
        List<int> scores = new List<int>();
        scores.Add(-1 * score);
        for (int i = 0; i < 5; i++)
            scores.Add(-1 * PlayerPrefs.GetInt(i.ToString()));
        scores.Sort();
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), scores[i] * -1);

            if (scores[i] == -1 * score)
                myRank = i + 1;
        }
        if (myRank == 0)
            MyRankUI.instance.myRank.text = "순위권에 들지 못했습니다.";
        else
            MyRankUI.instance.myRank.text = "내 순위 : " + myRank.ToString();
    }
}


