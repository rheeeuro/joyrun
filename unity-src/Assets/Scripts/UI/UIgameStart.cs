using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIgameStart : MonoBehaviour
{

    void Start()
    {
        Time.timeScale = 0f;
        transform.gameObject.SetActive(true);
        UIgameOver.Instance.transform.gameObject.SetActive(false);

    }




    public void startButton()
    {
        //Start the game

        transform.gameObject.SetActive(false);
        GameManager.Instance.StartGame();
        Time.timeScale = 1f;


    }

    public void viewRankingButton()
    {
        //GameManager.Instance.viewRanking();
    }

}

