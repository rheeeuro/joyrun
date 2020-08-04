using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankingUI : MonoBehaviour
{
    public static RankingUI instance;

    // 랭킹 텍스트 변수
    public Text ranking;

    void Awake()
    {
        instance = this;
    }

    // 시작시 안보이도록 설정
    void Start()
    {
        transform.gameObject.SetActive(false);
        UpdateRanking();
    }

    // 보이도록 설정
    public void Show()
    {
        GameManager.instance.SetGameState(GameState.ranking);
        transform.gameObject.SetActive(true);   
    }

    // 취소 버튼을 누른 경우
    public void HandleCancel()
    {
        transform.gameObject.SetActive(false);
        MenuUI.instance.Show();
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


