using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyRankUI : MonoBehaviour
{
    public static MyRankUI instance;

    // 내 등수 표시 관련 변수
    public Text myRank;
    public Text ranking;
    public Text point;

    void Awake()
    {
        instance = this;     
    }

    void Start()
    {
        transform.gameObject.SetActive(false);
        UpdateRanking();
    }

    // 보이도록 설정
    public void Show()
    {
        GameManager.instance.MyRank();
        transform.gameObject.SetActive(true);
    }
    
    // 재시작 버튼을 누른 경우
    public void HandleRetry()
    {
        SceneManager.LoadScene("Game");
    }

    // 메뉴로 버튼을 누른 경우
    public void HandleToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // 랭킹을 업데이트
    public void UpdateRanking()
    {
        for (int i = 0; i < 5; i++)
        {
            ranking.text = "Ranking\n\n" + "1. " + PlayerPrefs.GetInt("0") + "\n\n" +
            "2. " + PlayerPrefs.GetInt("1") + "\n\n" +
            "3. " + PlayerPrefs.GetInt("2") + "\n\n" +
            "4. " + PlayerPrefs.GetInt("3") + "\n\n" +
            "5. " + PlayerPrefs.GetInt("4") + "\n\n";
        }
    }


}


