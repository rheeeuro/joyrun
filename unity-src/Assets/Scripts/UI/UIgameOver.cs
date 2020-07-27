using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIgameOver : MonoBehaviour
{

    public static UIgameOver instance;

    public Text maxCombo;
    public Text playTime;
    public Text point;

    private void Awake()
    {
        instance = this;
        transform.gameObject.SetActive(false);
    }

    public void Show()
    {
        GameManager.instance.GameOver();
        transform.gameObject.SetActive(true);
        UIinGame.instance.transform.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }


    public void RetryButton()
    {
        //When Click the retryButton
        transform.gameObject.SetActive(false);
        //Main Scene Loaded
        SceneManager.LoadScene("Main");
        GameManager.instance.StartGame();



        //Allow time to flow again
        Time.timeScale = 1f;

    }

    public void RankingButton()
    {
        transform.gameObject.SetActive(false);
        UIresultPage.instance.Show();

    }
}