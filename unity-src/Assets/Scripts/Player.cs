using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // 오브젝트 변수 선언
    public static GameObject player;
    public static Player instance;

    // 애니매이션, 애니매이터 변수 선언
    Animator animator;
    RuntimeAnimatorController animIdle;
    RuntimeAnimatorController animRun;
    RuntimeAnimatorController animWalk;
    RuntimeAnimatorController animSprint;
    RuntimeAnimatorController animJump;

    //점수 변수 선언
    public static int point;
    public static int myRank;


    // 콤보, 체력 관련 변수, 상수 선언
    public int maxCombo;
    public int combo;
    public float comboTimer;
    public int hp;
    public const int startHp = 50;
    public const int maxHp = 100;

    // 점프 관련 번수 선언
    public static bool isJumping;
    public float jumpTimer;

    // 타이머  변수 선언
    public static float timer;
    public const float gameTime = 60;

    // 텍스트 변수 선언
    public Text comboText;
    public Text hpText;
    public Text timerText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitialValues();
    }

    // 시간 오차를 줄이기 위해 FixedUpdate로 타이머 변경
    void FixedUpdate()
    {
        HandleTimer();
    }
    
    void Update()
    {
        if (isJumping)
        {
            HandleJump();
        }
        else {
            HandleRuntimeAnimatorController(Tile.actualSpeed);
            HandleKeyboard();
        }

        HandleText();
    }

    void InitialValues() {
    
        player = gameObject ;

        animator = GetComponent<Animator>();
        player.GetComponent<Animation>().wrapMode = WrapMode.Loop;

        InitializePrefabs();

        myRank = 9999;
        point = 0;
        combo = 0;
        maxCombo = -1;
        comboTimer = 0;

        hp = startHp;
        timer = gameTime;

        isJumping = false;
        jumpTimer = 0;
    }

    void InitializePrefabs() {
        animIdle = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Idle") as RuntimeAnimatorController;
        animRun = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Run") as RuntimeAnimatorController;
        animWalk = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Walk") as RuntimeAnimatorController;
        animSprint = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Sprint") as RuntimeAnimatorController;
        animJump = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Jump") as RuntimeAnimatorController;
    }

    void HandleJump()
    {
        jumpTimer += Time.deltaTime;
        if (jumpTimer >= 0.6)
        {
            isJumping = false;
            jumpTimer = 0;
        }
    }

    void HandleTimer() {
        timer = Mathf.Round((timer - Time.fixedDeltaTime) * 100) / 100;
        if (timer < 0)
        {
            timer = 0;
            GameEnd();
        }
    }

    void HandleText() {
        hpText.text = "HP : " + hp.ToString();
        timerText.text = "Timer : " + timer.ToString("00.00");
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else
        {
            comboText.text = "";
        }
    }

    void HandleRuntimeAnimatorController(float speed) {
        if (speed <= 40)
        {
            animator.runtimeAnimatorController = animWalk as RuntimeAnimatorController;
        }
        else if (speed > 40 && speed <= 60)
        {
            animator.runtimeAnimatorController = animRun as RuntimeAnimatorController;
        }
        else
        {
            animator.runtimeAnimatorController = animSprint as RuntimeAnimatorController;
        }
    }


    void HandleKeyboard()
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
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Tile.extraSpeed += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            isJumping = true;
            animator.runtimeAnimatorController = animJump as RuntimeAnimatorController;
        }
    }


    public void MeetHeart()
    {
        combo = combo + HeartInfo.Combo;
        ChangeCombo();
        if (hp < maxHp)
        {
            hp = hp + HeartInfo.life;
            if (hp > maxHp){
                hp = maxHp;
            }
        }
    }

    public void MeetObstacle()
    {
        combo = 0;
        Tile.extraSpeed = 0;
        ChangeCombo();
        HavaDamaged();
        
    }
    public void MeetEmpty()
    {
        combo = combo + EmptyInfo.Combo;
        ChangeCombo();
    }

    void HavaDamaged()
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
            GameEnd();

        }
        else if (hp > 100) {
            hp = 100;
        }
    }

    void ChangeCombo() {
        if(maxCombo <= combo)
        {
            maxCombo = combo;
        }

        comboText.text = combo.ToString() + " COMBO";
        comboTimer = 0.7f;
    }

    void CaculatePoint()
    {
        point = combo + (int)(60f - timer);
        UIresultPage.instance.point.text = "내 점수 : " + point.ToString();


        UIgameOver.instance.maxCombo.text = "최대 콤보 횟수 : " + maxCombo.ToString() + " 회";
        UIgameOver.instance.playTime.text = "진행 시간 : " + (60f - timer) + " 초";
        UIgameOver.instance.point.text = "점수 : " + point;

    }
    void InsertRank(int score)
    {
        for (int i = 0; i < 5; i++)
        {
            if (score > PlayerPrefs.GetInt(i.ToString()))
            {
                for (int j = 4 - 1; j < 0; j--)
                {
                    PlayerPrefs.SetInt(j.ToString(), PlayerPrefs.GetInt((j - 1).ToString()));
                    // 스코어가 1등 기준으로 PlayerPrefs의 Key값(j의 위치값 4(5등))을 j-1위치의 값(4등)으로 바꾼다.
                    // j가 1씩 줄어들면서 윗 순번도 차례대로 비교해서 바꾼다.
                }
                PlayerPrefs.SetInt(i.ToString(), score); //현재 등수를 현재 스코어 값으로 바꾸어 준다.
                myRank = i + 1;

                if (myRank <= 5)
                    UIresultPage.instance.myRank.text = "내 순위 : " + myRank.ToString();


                UIresultPage.instance.UpdateRanking();
                break; // 종료

            }
            if (myRank > 5)
            {
                UIresultPage.instance.myRank.text = "5위 미만입니다.";
            }
        }
    }

    void GameEnd()
    {
        CaculatePoint();
        InsertRank(point);
        point = 0;
        UIgameOver.instance.Show();
    }
}
