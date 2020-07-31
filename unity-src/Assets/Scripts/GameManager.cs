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
    result,
    ranking
}




public class GameManager : MonoBehaviour
{

    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    public static GameManager instance;




    public GameState currentGameState = GameState.menu;

    void Awake() // Start 시작하기 전에 실행
    {
        Time.timeScale = 1;
        //싱글턴 사용을 위한 오브젝트 연결
        instance = this;
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

    public void Menu()
    {
        SetGameState(GameState.menu);
    }
    public void Result()
    {
        SetGameState(GameState.result);
    }

    public void Ranking()
    {
        SetGameState(GameState.ranking);
    }


    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Find("Canvas").transform.Find("UIgameStartPanel").gameObject.SetActive(true);
        //GameObject.Find("Canvas").transform.Find("UlgameOverPanel").gameObject.SetActive(false);
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
        else if (newGameState == GameState.result) {
        }
        else if (newGameState == GameState.ranking)
            //현재 게임 상태값
            currentGameState = newGameState;
    }
}