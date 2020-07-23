using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIgameOver : MonoBehaviour
{

    public static UIgameOver Instance;

    private void Awake()
    {
        Instance = this;
        transform.gameObject.SetActive(false);
    }

    public void Show()
    {
        GameManager.Instance.GameOver();
        transform.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }


    public void retryButton()
    {
        //When Click the retryButton

        //Main Scene Loaded
        SceneManager.LoadScene("Main");


        //Allow time to flow again

    }
}


