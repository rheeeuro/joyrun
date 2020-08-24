using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 게임 상태 (0: 인게임, 1: 메뉴, 2: 내순위, 3: 일시정지, 4: 랭킹, 5: 게임결과, 6: 환경설정)
public enum GameState
{
    game,
    menu,
    myRank,
    pause,
    ranking,
    result,
    setting
}

public class GameManager : MonoBehaviour
{
    // 인스턴스, 게임상태, 키넥트 연결 상태 변수 선언
    public static GameManager instance;
    private GameState currentGameState;
    private bool kinectState;



    void Awake() 
    {        
        instance = this;
    }

    void Start()
    {
        DisplaySetting();
        HandleFrameRate();
        kinectState = false;
        Time.timeScale = 1;
    }

    // 프레임 설정
    public void HandleFrameRate() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = ConstInfo.gameFrameRate;
    }

    // 화면 설정 (디스플레이가 하나일 경우 전면 UI만 출력, 두개 이상일 경우 바닥 UI 출력)
    public void DisplaySetting()
    {
        Screen.SetResolution(1920, 1080, true);
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
        if (Display.displays.Length > 2)
            Display.displays[2].Activate();
    }



    // 키넥트 연결 여부 변수 Getter & Setter
    public bool GetKinectState() { return kinectState; }
    public void SetKinectState(bool newKinectState) { kinectState = newKinectState; }



    // 게임상태 변수 Getter & Setter
    public GameState GetGameState() { return currentGameState; }
    public void SetGameState(GameState newGameState) { currentGameState = newGameState; }
}