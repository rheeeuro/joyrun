using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIgameStart : MonoBehaviour
{

    void Awake()
    {
        transform.gameObject.SetActive(true);
        UIgameOver.instance.transform.gameObject.SetActive(false);
        
    }
    void Start()
    {
        Player.instance.transform.gameObject.SetActive(false);
        Time.timeScale = 0f;
        if (UIinGame.instance != null)
        {
            UIinGame.instance.transform.gameObject.SetActive(false);
        }
        Time.timeScale = 0f;


    }




    public void StartButton()
    {
        //Start the game
        Player.instance.transform.gameObject.SetActive(true);
        transform.gameObject.SetActive(false);
        GameManager.instance.StartGame();
        UIinGame.instance.transform.gameObject.SetActive(true);
        Time.timeScale = 1f;


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