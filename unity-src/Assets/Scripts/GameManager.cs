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
    private bool kinectState;

    void Awake() 
    {        
        instance = this;
    }

    void Start()
    {
        Setting.InitialSetting();
        kinectState = false;
        Time.timeScale = 1;
    }


    public bool GetKinectState() {
        return kinectState;
    }

    public void SetKinectState(bool newKinectState) {
        kinectState = newKinectState;
    }

    public GameState GetGameState() {
        return currentGameState;
    }

    public void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
    }
}