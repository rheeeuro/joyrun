using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 환경설정 상태 (선택 하이라이트, 0: 애니메이션, 1: 체력제한, 2: 시간제한)
public enum SettingState
{
    animation,
    hp,
    time
}

public class SettingUI : MonoBehaviour
{
    // 인스턴스 및 현재 선택된 항목 변수 선언
    public static SettingUI instance;
    private SettingState currentSettingState;

    // 색상 변수
    private Color selectedColor = Color.yellow;
    private Color unselectedColor = Color.white;

    // 메뉴 버튼 변수
    public GameObject animationButton;
    public GameObject hpButton;
    public GameObject timeButton;


    void Awake() { instance = this; }
    
    void Start()
    {
        currentSettingState = SettingState.animation;
        transform.gameObject.SetActive(false);
    }

    // 환경설정 UI 보여주기
    public void Show()
    {
        GameManager.instance.SetGameState(GameState.setting);
        transform.gameObject.SetActive(true);
    }

    void Update()
    {
        UnselectButtons();
        HandleSettingState();
        DisplaySettingText();
    }


    // 모든 버튼을 선택 해제
    void UnselectButtons()
    {
        animationButton.GetComponent<UnityEngine.UI.Image>().color = unselectedColor;
        hpButton.GetComponent<UnityEngine.UI.Image>().color = unselectedColor;
        timeButton.GetComponent<UnityEngine.UI.Image>().color = unselectedColor;
    }

    // 환경설정 상태에 따라 버튼 선택 (색상 변경) 
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



    // 위 버튼을 누른 경우
    public void HandleUp(){
        if (currentSettingState == SettingState.animation)
            currentSettingState = SettingState.time;
        else
            currentSettingState--;
    }

    // 아래 버튼을 누른 경우
    public void HandleDown() {
        if (currentSettingState == SettingState.time)
            currentSettingState = SettingState.animation;
        else
            currentSettingState++;
    }

    // 왼쪽 버튼을 누른 경우 
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

    // 오른쪽 버튼을 누른 경우
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

    // 취소 버튼을 누른 경우
    public void HandleCancel() {
        transform.gameObject.SetActive(false);
        MenuUI.instance.Show();
    }

    // 설정된 환경설정 변수를 버튼에 출력
    public void DisplaySettingText() {
        animationButton.GetComponentInChildren<Text>().text = Setting.GetCurrentMovingState().ToString();
        hpButton.GetComponentInChildren<Text>().text = Setting.GetCurrentHpState().ToString();
        timeButton.GetComponentInChildren<Text>().text = Setting.GetCurrentTimeState().ToString();
    }
}
