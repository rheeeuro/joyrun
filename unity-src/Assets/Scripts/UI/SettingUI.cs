using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 환경설정 상태 (선택 하이라이트, 0: 애니메이션, 1: 체력제한, 2: 시간제한)
public enum SettingChoice
{
    animation,
    hp,
    time
}

public class SettingUI : MonoBehaviour
{
    // 인스턴스 및 현재 선택된 항목 변수 선언
    public static SettingUI instance;
    private SettingChoice currentSettingChoice;

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
        currentSettingChoice = SettingChoice.animation;
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
        HandleSettingChoice();
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
    void HandleSettingChoice() {
        switch (currentSettingChoice)
        {
            case SettingChoice.animation:
                animationButton.GetComponent<UnityEngine.UI.Image>().color = selectedColor;
                break;
            case SettingChoice.hp:
                hpButton.GetComponent<UnityEngine.UI.Image>().color = selectedColor;
                break;
            case SettingChoice.time:
                timeButton.GetComponent<UnityEngine.UI.Image>().color = selectedColor;
                break;
        }
    }



    // 위 버튼을 누른 경우
    public void HandleUp(){
        if (currentSettingChoice == SettingChoice.animation)
            currentSettingChoice = SettingChoice.time;
        else
            currentSettingChoice--;
    }

    // 아래 버튼을 누른 경우
    public void HandleDown() {
        if (currentSettingChoice == SettingChoice.time)
            currentSettingChoice = SettingChoice.animation;
        else
            currentSettingChoice++;
    }

    // 왼쪽 버튼을 누른 경우 
    public void HandleLeft() {
        switch (currentSettingChoice) {
            case SettingChoice.animation:
                if (Setting.GetCurrentAnimationState() == AnimationState.animation)
                    Setting.SetCurrentAnimationState(AnimationState.kinect);
                else
                    Setting.SetCurrentAnimationState(Setting.GetCurrentAnimationState()+1);
                break;
            case SettingChoice.hp:
                if (Setting.GetCurrentHpState() == HpState.immortal)
                    Setting.SetCurrentHpState(HpState.normal);
                else
                    Setting.SetCurrentHpState(Setting.GetCurrentHpState() + 1);
                break;
            case SettingChoice.time:
                if (Setting.GetCurrentTimeState() == TimeState.infinite)
                    Setting.SetCurrentTimeState(TimeState.normal);
                else
                    Setting.SetCurrentTimeState(Setting.GetCurrentTimeState() + 1);
                break;

        }
    }

    // 오른쪽 버튼을 누른 경우
    public void HandleRight() {
        switch (currentSettingChoice)
        {
            case SettingChoice.animation:
                if (Setting.GetCurrentAnimationState() == AnimationState.kinect)
                    Setting.SetCurrentAnimationState(AnimationState.animation);
                else
                    Setting.SetCurrentAnimationState(Setting.GetCurrentAnimationState() - 1);
                break;
            case SettingChoice.hp:
                if (Setting.GetCurrentHpState() == HpState.normal)
                    Setting.SetCurrentHpState(HpState.immortal);
                else
                    Setting.SetCurrentHpState(Setting.GetCurrentHpState() - 1);
                break;
            case SettingChoice.time:
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
        animationButton.GetComponentInChildren<Text>().text = Setting.GetCurrentAnimationState().ToString();
        hpButton.GetComponentInChildren<Text>().text = Setting.GetCurrentHpState().ToString();
        timeButton.GetComponentInChildren<Text>().text = Setting.GetCurrentTimeState().ToString();
    }
}
