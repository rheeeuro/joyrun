using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuState
{
    start,
    ranking,
    quit
}

public class MenuUI : MonoBehaviour
{

    public static MenuUI instance;
    private MenuState currentMenuState;

    // 색상 변수
    private Color selectedColor = Color.yellow;
    private Color unselectedColor = Color.white;

    // 메뉴 버튼 변수
    public GameObject startButton;
    public GameObject rankingButton;
    public GameObject quitButton;

    void Awake()
    {
        instance = this;
    }

    // 초기에는 게임시작 버튼 하이라이트
    void Start()
    {
        currentMenuState = MenuState.start;
    }

    void Update()
    {
        UnselectButtons();
        HandleMenuState();
    }

    // 메뉴 UI 보여주기
    public void Show() {
        GameManager.instance.SetGameState(GameState.menu);
        transform.gameObject.SetActive(true);
    }

    // 모든 버튼을 선택 해제
    void UnselectButtons()
    {
        startButton.GetComponent<UnityEngine.UI.Image>().color = unselectedColor;
        rankingButton.GetComponent<UnityEngine.UI.Image>().color = unselectedColor;
        quitButton.GetComponent<UnityEngine.UI.Image>().color = unselectedColor;
    }

    // 메뉴 상태에 따라 버튼 선택 (색상 변경) 
    void HandleMenuState() {
        switch (currentMenuState)
        {
            case MenuState.start:
                startButton.GetComponent<UnityEngine.UI.Image>().color = selectedColor;
                break;
            case MenuState.ranking:
                rankingButton.GetComponent<UnityEngine.UI.Image>().color = selectedColor;
                break;
            case MenuState.quit:
                quitButton.GetComponent<UnityEngine.UI.Image>().color = selectedColor;
                break;
        }
    }

    // 위 방향 버튼을 눌렀을 경우
    public void HandleUp() {
        if (currentMenuState == MenuState.start)
            currentMenuState = MenuState.quit;
        else
            currentMenuState--;
    }

    // 아래 방향 버튼을 눌렀을 경우
    public void HandleDown() {
        if (currentMenuState == MenuState.quit)
            currentMenuState = MenuState.start;
        else
            currentMenuState++;
    }

    // 확인 버튼을 눌렀을 경우
    public void HandleConfirm() {
        switch (currentMenuState) {
            case MenuState.start:
                HandleStart();
                break;
            case MenuState.ranking:
                HandleRanking();
                break;
            case MenuState.quit:
                HandleQuit();
                break;
        }
    }

    // 시작 상태에서 확인 버튼을 눌렀을 경우
    public void HandleStart()
    {
        SceneManager.LoadScene("Game");
    }

    // 랭킹 상태에서 확인 버튼을 눌렀을 경우
    public void HandleRanking()
    {
        transform.gameObject.SetActive(false);
        RankingUI.instance.Show();
    }

    // 종료 상태에서 확인 버튼을 눌렀을 경우
    public void HandleQuit()
    {
        Application.Quit();
    }
}