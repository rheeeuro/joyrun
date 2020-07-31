using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIgameStart : MonoBehaviour
{

    void Awake()
    {
        transform.gameObject.SetActive(true);
        
    }
    void Start()
    {
        if (UIinGame.instance != null)
        {
            UIinGame.instance.transform.gameObject.SetActive(false);
        }
    }




    public void StartButton()
    {
        //Start the game

        Debug.Log("Game Scene 로드");
        SceneManager.LoadScene("Game");


        transform.gameObject.SetActive(false);
        GameManager.instance.StartGame();
        UIinGame.instance.transform.gameObject.SetActive(true);
    }

    public void RankingButton()
    {
        transform.gameObject.SetActive(false);
        GameManager.instance.Result();
        MenuResult.instance.Show();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ViewRankingButton()
    {
        //GameManager.Instance.viewRanking();
    }



}