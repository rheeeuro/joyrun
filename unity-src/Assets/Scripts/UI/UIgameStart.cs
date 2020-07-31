using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIgameStart : MonoBehaviour
{

    public static UIgameStart instance;
    public enum MenuStatus
    {
        start,
        ranking,
        quit
    }
    public GameObject startButton;
    public GameObject rankingButton;
    public GameObject quitButton;

    public MenuStatus status;

    void Awake()
    {
        transform.gameObject.SetActive(true);
        instance = this;
    }
    void Start()
    {
        if (UIinGame.instance != null)
        {
            UIinGame.instance.transform.gameObject.SetActive(false);
        }
        status = MenuStatus.start;
    }

    void Update()
    {
        UnselectButtons();
        switch (status) {
            case MenuStatus.start:
                startButton.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
                break;
            case MenuStatus.ranking:
                rankingButton.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
                break;
            case MenuStatus.quit:
                quitButton.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
                break;
        }
    }

    void UnselectButtons()
    {
        startButton.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        rankingButton.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        quitButton.GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }

    public void HandleUp() {
        if (status == MenuStatus.start)
            status = MenuStatus.quit;
        else
            status--;
    }

    public void HandleDown() {
        if (status == MenuStatus.quit)
            status = MenuStatus.start;
        else
            status++;
    }

    public void HandleConfirm() {
        switch (status) {
            case MenuStatus.start:
                HandleStart();
                break;
            case MenuStatus.ranking:
                HandleRanking();
                break;
            case MenuStatus.quit:
                HandleQuit();
                break;
        }
    }

    public void HandleStart()
    {
        //Start the game

        Debug.Log("Game Scene 로드");
        SceneManager.LoadScene("Game");


        transform.gameObject.SetActive(false);
        GameManager.instance.StartGame();
    }

    public void HandleRanking()
    {
        transform.gameObject.SetActive(false);
        GameManager.instance.Ranking();
        UIranking.instance.Show();
    }

    public void HandleQuit()
    {
        Application.Quit();
    }

    public void ViewRankingButton()
    {
        //GameManager.Instance.viewRanking();
    }



}