using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    menu,
    inGame,
    gameOver,
    inPause,
}




public class GameManager : MonoBehaviour
{
    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static GameManager _instance;
    // 인스턴스에 접근하기 위한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }



    public GameState currentGameState = GameState.menu;

    void Awake() // Start 시작하기 전에 실행
    {
        if (_instance == null)
        {
            //싱글턴 설정
            _instance = this;
        }

        // 인스턴스가 존재하는 경우 새로 생기는 인스턴스를 삭제(중복 방지)
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 씬이 전환되더라도 선언되었던 인스턴스를 파괴하지 않는다.
        DontDestroyOnLoad(gameObject);

    }

    public void StartGame()
    {
        // 점프해야 시작하도록 추후에 조건문 설정
        SetGameState(GameState.inGame);
    }

    public void GameOver()
    {
        // life가 0이 되면 시작
        SetGameState(GameState.gameOver);
    }

    public void Pause()
    {
        SetGameState(GameState.inPause);
    }

    public void BackToMenu()
    {
        // 게임 중간에 그만두기를 원할 시에
        SetGameState(GameState.menu);
    }


    // Start is called before the first frame update
    void Start()
    {
        //if(키넥트에서 점프를 했는가?)
        StartGame();
    }

    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {

        }
        else if (newGameState == GameState.inGame)
        {

        }
        else if (newGameState == GameState.gameOver)
        {

        }
        else if (newGameState == GameState.inPause)
        {

        }
        //현재 게임 상태값
        currentGameState = newGameState;
    }

    void Update()
    {

    }
}

