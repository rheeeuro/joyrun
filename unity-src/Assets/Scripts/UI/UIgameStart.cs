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

        Debug.Log("Game Scene 로드");
        SceneManager.LoadScene("Game");


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