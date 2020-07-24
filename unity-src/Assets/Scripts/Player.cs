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

    // 콤보, 체력 관련 변수, 상수 선언
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

        combo = 0;
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
            Debug.Log("게임 클리어");
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
        if (Input.GetKey(KeyCode.G))
        {
            Tile.extraSpeed += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.F))
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
            Debug.Log("게임 오버");
            UIgameOver.instance.Show();

        }
        else if (hp > 100) {
            hp = 100;
        }
    }

    void ChangeCombo() {
        comboText.text = combo.ToString() + " COMBO";
        comboTimer = 0.7f;
    }
}
