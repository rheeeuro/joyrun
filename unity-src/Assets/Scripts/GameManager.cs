using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    game,
    menu,
    myRank,
    pause,
    ranking,
    result
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameState currentGameState;

    void Awake() 
    {        
        instance = this;
        Time.timeScale = 1;
    }

    public void Game() {
        SetGameState(GameState.game);
    }

    public void Menu() {
        SetGameState(GameState.menu);
    }

    public void MyRank() {
        SetGameState(GameState.myRank);
    }

    public void Pause() {
        SetGameState(GameState.pause);
    }

    public void Ranking() {
        SetGameState(GameState.ranking);
    }

    public void Result() {
        SetGameState(GameState.result);
    }

    public GameState GetGameState() {
        return currentGameState;
    }

    public void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
    }
}