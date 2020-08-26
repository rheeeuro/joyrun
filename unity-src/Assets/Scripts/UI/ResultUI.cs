using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResultUI : MonoBehaviour
{
    // 인스턴스 변수 선언
    public static ResultUI instance;

    // 결과 텍스트 변수
    public Text maxCombo;
    public Text playTimeText;
    public Text pointText;

    void Awake() { instance = this; }
    void Start() { transform.gameObject.SetActive(false); }

    // 보이도록 설정
    public void Show()
    {
        GameManager.instance.SetGameState(GameState.Result);
        transform.gameObject.SetActive(true);
        GetComponent<Animation>().Play("ShowGuide");
    }



    // 다음페이지 버튼을 누른 경우
    public void HandleNextPage()
    {
        transform.gameObject.SetActive(false);
        MyRankUI.instance.Show();
    }
}