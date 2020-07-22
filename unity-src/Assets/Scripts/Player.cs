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
    public float comboTimer = 0;
    public int hp = 50;
    public int maxHP = 100;

    public float timer = 60;


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
        hpText.text = "HP : " + hp.ToString();
        timer = Mathf.Round((timer - Time.deltaTime) * 100) / 100;
        if (timer < 0) {
            timer = 0;
        }
        timerText.text = "Timer : " + timer.ToString("00.00");

        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else {
            comboText.text = "";
        }
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
        if (Input.GetKey(KeyCode.G)) {
            Tile.extraSpeed += 0.1f;
            
        }
    }


    public void meetHeart()
    {
        combo = combo + HeartInfo.Combo;
        changeCombo();
        if (hp < maxHP)
        {
            hp = hp + HeartInfo.life;
        }
    }

    public void meetObstacle()
    {
        combo = 0;
        Tile.extraSpeed = 0;
        changeCombo();
        havaDamaged();
        
    }
    public void meetEmpty()
    {
        combo = combo + EmptyInfo.Combo;
        changeCombo();
    }

    void havaDamaged()
    {
        if ((hp / 2) > 10)
        {
            hp = hp - (hp / 2);
        } else {
            hp -= 10;
        }


        if (hp < 0)
        {
            hp = 0;
        }
        else if (hp > 100) {
            hp = 100;
        }
    }

    void changeCombo() {
        comboText.text = combo.ToString() + " COMBO";
        comboTimer = 0.7f;
    }
}
