using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIinGame : MonoBehaviour
{
    public bool bePause;
    public static UIinGame instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        GameManager.instance.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (bePause == true)
        {
            //눌리면 시간정지 후, gameState Pause로 변경
            GameManager.instance.Pause();
        }
        else
        {
            GameManager.instance.StartGame();
        }
    }

    public void Pause()
    {
        if (bePause == false)
        {
            bePause = true;
            GameManager.instance.Pause();
        }
        else
        {
            bePause = false;
            GameManager.instance.StartGame();
        }

    }
}


