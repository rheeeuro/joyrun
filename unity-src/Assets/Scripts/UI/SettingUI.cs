using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SettingState
{
    animation,
    hp,
    time
}

public class SettingUI : MonoBehaviour
{
    public static SettingUI instance;
    private SettingState currentSettingState;

    // 색상 변수
    private Color selectedColor = Color.yellow;
    private Color unselectedColor = Color.white;

    // 메뉴 버튼 변수
    public GameObject animationButton;
    public GameObject hpButton;
    public GameObject timeButton;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSettingState = SettingState.animation;
        transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UnselectButtons();
        HandleSettingState();

        animationButton.GetComponentInChildren<Text>().text = Setting.GetCurrentMovingState().ToString();
        hpButton.GetComponentInChildren<Text>().text = Setting.GetCurrentHpState().ToString();
        timeButton.GetComponentInChildren<Text>().text = Setting.GetCurrentTimeState().ToString();
    }

    public void Show()
    {
        GameManager.instance.SetGameState(GameState.setting);
        transform.gameObject.SetActive(true);
    }

    void UnselectButtons()
    {
        animationButton.GetComponent<UnityEngine.UI.Image>().color = unselectedColor;
        hpButton.GetComponent<UnityEngine.UI.Image>().color = unselectedColor;
        timeButton.GetComponent<UnityEngine.UI.Image>().color = unselectedColor;
    }

    void HandleSettingState() {
        switch (currentSettingState)
        {
            case SettingState.animation:
                animationButton.GetComponent<UnityEngine.UI.Image>().color = selectedColor;
                break;
            case SettingState.hp:
                hpButton.GetComponent<UnityEngine.UI.Image>().color = selectedColor;
                break;
            case SettingState.time:
                timeButton.GetComponent<UnityEngine.UI.Image>().color = selectedColor;
                break;

        }
    }

    public void HandleUp(){
        if (currentSettingState == SettingState.animation)
            currentSettingState = SettingState.time;
        else
            currentSettingState--;
    }

    public void HandleDown() {
        if (currentSettingState == SettingState.time)
            currentSettingState = SettingState.animation;
        else
            currentSettingState++;
    }

    public void HandleLeft() {
        switch (currentSettingState) {
            case SettingState.animation:
                if (Setting.GetCurrentMovingState() == MovingState.animation)
                    Setting.SetCurrentMovingState(MovingState.kinect);
                else
                    Setting.SetCurrentMovingState(Setting.GetCurrentMovingState()+1);
                break;
            case SettingState.hp:
                if (Setting.GetCurrentHpState() == HpState.immortal)
                    Setting.SetCurrentHpState(HpState.normal);
                else
                    Setting.SetCurrentHpState(Setting.GetCurrentHpState() + 1);
                break;
            case SettingState.time:
                if (Setting.GetCurrentTimeState() == TimeState.infinite)
                    Setting.SetCurrentTimeState(TimeState.normal);
                else
                    Setting.SetCurrentTimeState(Setting.GetCurrentTimeState() + 1);
                break;

        }
    }

    public void HandleRight() {
        switch (currentSettingState)
        {
            case SettingState.animation:
                if (Setting.GetCurrentMovingState() == MovingState.kinect)
                    Setting.SetCurrentMovingState(MovingState.animation);
                else
                    Setting.SetCurrentMovingState(Setting.GetCurrentMovingState() - 1);
                break;
            case SettingState.hp:
                if (Setting.GetCurrentHpState() == HpState.normal)
                    Setting.SetCurrentHpState(HpState.immortal);
                else
                    Setting.SetCurrentHpState(Setting.GetCurrentHpState() - 1);
                break;
            case SettingState.time:
                if (Setting.GetCurrentTimeState() == TimeState.normal)
                    Setting.SetCurrentTimeState(TimeState.infinite);
                else
                    Setting.SetCurrentTimeState(Setting.GetCurrentTimeState() - 1);
                break;

        }
    }

    public void HandleCancel() {
        transform.gameObject.SetActive(false);
        MenuUI.instance.Show();
    }
}
