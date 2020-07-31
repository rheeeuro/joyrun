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

    private void Awake() { 
        //싱글턴 사용을 위한 오브젝트 연결
        instance = this;
        //시작할 때는 안보이도록 설정
        transform.gameObject.SetActive(false);
    }

    public void Show()
    {
        GameManager.instance.GameOver();
        transform.gameObject.SetActive(true);
        UIinGame.instance.transform.gameObject.SetActive(false);
    }


    public void RankingButton()
    {

        transform.gameObject.SetActive(false);
        //Main Scene Loaded
        UIresultPage.instance.transform.gameObject.SetActive(true);

    }
}