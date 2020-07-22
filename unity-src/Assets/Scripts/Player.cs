using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public static GameObject player;
    public static Player instance;

    // 콤보, 체력 변수
    public int combo = 0;
    public int hp = 50;
    public int maxHP = 100;

    public float timer = 60.0f;


    public Text comboText;
    public Text hpText;
    public Text timerText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
    
    }

    // Update is called once per frame
    void Update()
    {
        handleKeyboard();
        comboText.text = "Combo : " + combo.ToString();
        hpText.text = "HP : " + hp.ToString();
        timer = timer - 0.001f;
        timerText.text = "Timer : " + timer.ToString();
    }


    void handleKeyboard()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.position = new Vector3(Tile.left, player.transform.position.y, player.transform.position.z);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            player.transform.position = new Vector3(Tile.center, player.transform.position.y, player.transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.position = new Vector3(Tile.right, player.transform.position.y, player.transform.position.z);
        }
    }


    public void meetHeart()
    {
        combo = combo + HeartInfo.instance.Combo;

        if (hp < maxHP)
        {
            hp = hp + HeartInfo.instance.life;
        }
    }

    public void meetObstacle()
    {
        combo = 0;
        havaDamaged();
        
    }
    public void meetEmpty()
    {
        combo = combo + EmptyInfo.instance.Combo;
    }

    void havaDamaged()
    {
        if ((hp / 2) > 10 )
        {
            hp = hp - (hp / 2);
        }
        else if ((hp / 2) <= 10)
        {
            hp = hp - 10;
        }
    }
}
