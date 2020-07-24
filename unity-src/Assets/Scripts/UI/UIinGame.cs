using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIinGame : MonoBehaviour
{
    bool bePause;
    public static UIinGame instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.bePause == true)
        {
            //눌리면 시간정지 후, gameState Pause로 변경
            Time.timeScale = 0f;
            GameManager.instance.Pause();
        }
        else
        {
            Time.timeScale = 1f;
            GameManager.instance.StartGame();
        }
    }

    public void Pause()
    {
        if (bePause == false)
            this.bePause = true;
        else
            this.bePause = false;

    }
}


