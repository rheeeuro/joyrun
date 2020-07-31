using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIresultPage : MonoBehaviour
{
    //오브젝트 변수 및 랭킹, 점수 텍스트 변수 선언
    public static UIresultPage instance;

    public Text myRank;
    public Text ranking;
    public Text point;


    private void Awake()
    {
        //싱글턴 사용을 위한 오브젝트 연결
        instance = this;


        //시작할 때는 안보이도록 설정
        transform.gameObject.SetActive(false);
    }
    public void Show()
    {

        GameManager.instance.Result();

        //보이도록 설정
        transform.gameObject.SetActive(true);
    }

    public void RetryButton()
    {
        //When Click the retryButton
        transform.gameObject.SetActive(false);
        //Main Scene Loaded
        SceneManager.LoadScene("Game");
        GameManager.instance.StartGame();



        //Allow time to flow again
    }

    void Start()
    {
        //새로 저장된 랭킹은 Result화면으로 올떄마다 한 번만 부르면 되기 때문에 Start에 배정
        UpdateRanking();

    }


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


