using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public bool isPausing;
    public static GameUI instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isPausing = false;
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


