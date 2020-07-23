using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    public static GameManager Instance;




    public GameState currentGameState = GameState.menu;

    void Awake() // Start 시작하기 전에 실행
    {
        Instance = this;

    }

    public void StartGame()
    {
        // 점프해야 시작하도록 추후에 조건문 설정
        Time.timeScale = 1f;
        SetGameState(GameState.inGame);
    }

    public void GameOver()
    {
        // life가 0이 되면 시작
        Time.timeScale = 0f;
        SetGameState(GameState.gameOver);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        SetGameState(GameState.inPause);
    }

    public void Menu()
    {
        Time.timeScale = 0f;
        SetGameState(GameState.menu);
    }


    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Find("Canvas").transform.Find("UIgameStartPanel").gameObject.SetActive(true);
        //GameObject.Find("Canvas").transform.Find("UlgameOverPanel").gameObject.SetActive(false);
        Time.timeScale = 0f;
        Menu();

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

