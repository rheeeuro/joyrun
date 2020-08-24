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

    //색상 스프라이트
    public Sprite selected;
    public Sprite Unselected;


    public Texture arrowLSelected;
    public Texture arrowLUnselected;
    public Texture arrowRSelected;
    public Texture arrowRUnselected;

    /*
    public Sprite arrowLSelected;
    public Sprite arrowLUnselected;
    public Sprite arrowRSelected;
    public Sprite arrowRUnselected;
    **/

    // 메뉴 버튼 변수
    public GameObject animationButton;
    public GameObject hpButton;
    public GameObject timeButton;

    //메뉴 옆 화살표
    public GameObject animArrowLF;
    public GameObject animArrowRF;
    public GameObject hpArrowLF;
    public GameObject hpArrowRF;
    public GameObject timeArrowLF;
    public GameObject timeArrowRF;


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
        GetComponent<Animation>().Play("ShowGuide");
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
        animationButton.GetComponent<UnityEngine.UI.Image>().sprite = Unselected;
        hpButton.GetComponent<UnityEngine.UI.Image>().sprite = Unselected;
        timeButton.GetComponent<UnityEngine.UI.Image>().sprite = Unselected;


        animArrowLF.GetComponent<UnityEngine.UI.RawImage>().texture = arrowLUnselected;
        animArrowRF.GetComponent<UnityEngine.UI.RawImage>().texture = arrowRUnselected;
        hpArrowLF.GetComponent<UnityEngine.UI.RawImage>().texture = arrowLUnselected;
        hpArrowRF.GetComponent<UnityEngine.UI.RawImage>().texture = arrowRUnselected;
        timeArrowLF.GetComponent<UnityEngine.UI.RawImage>().texture = arrowLUnselected;
        timeArrowRF.GetComponent<UnityEngine.UI.RawImage>().texture = arrowRUnselected;


    }

    // 환경설정 상태에 따라 버튼 선택 (색상 변경) 
    void HandleSettingChoice()
    {
        switch (currentSettingChoice)
        {
            case SettingChoice.animation:
                //animationButton.GetComponent<UnityEngine.UI.Image>().color = selectedColor;
                animationButton.GetComponent<UnityEngine.UI.Image>().sprite = selected;
                animArrowLF.gameObject.GetComponent<UnityEngine.UI.RawImage>().texture = arrowLSelected;
                animArrowRF.gameObject.GetComponent<UnityEngine.UI.RawImage>().texture = arrowRSelected;
                break;
            case SettingChoice.hp:
                //hpButton.GetComponent<UnityEngine.UI.Image>().color = selectedColor;
                hpButton.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = selected;
                hpArrowLF.gameObject.GetComponent<UnityEngine.UI.RawImage>().texture = arrowLSelected;
                hpArrowRF.gameObject.GetComponent<UnityEngine.UI.RawImage>().texture = arrowRSelected;
                break;
            case SettingChoice.time:
                //timeButton.GetComponent<UnityEngine.UI.Image>().color = selectedColor;
                timeButton.GetComponent<UnityEngine.UI.Image>().sprite = selected;
                timeArrowLF.gameObject.GetComponent<UnityEngine.UI.RawImage>().texture = arrowLSelected;
                timeArrowRF.gameObject.GetComponent<UnityEngine.UI.RawImage>().texture = arrowRSelected;
                break;
        }
    }



    // 위 버튼을 누른 경우
    public void HandleUp()
    {
        if (currentSettingChoice == SettingChoice.animation)
            currentSettingChoice = SettingChoice.time;
        else
            currentSettingChoice--;
    }

    // 아래 버튼을 누른 경우
    public void HandleDown()
    {
        if (currentSettingChoice == SettingChoice.time)
            currentSettingChoice = SettingChoice.animation;
        else
            currentSettingChoice++;
    }

    // 왼쪽 버튼을 누른 경우 
    public void HandleLeft()
    {
        switch (currentSettingChoice)
        {
            case SettingChoice.animation:
                if (Setting.GetCurrentAnimationState() == AnimationState.animation)
                    Setting.SetCurrentAnimationState(AnimationState.kinect);
                else
                    Setting.SetCurrentAnimationState(Setting.GetCurrentAnimationState() + 1);
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
    public void HandleRight()
    {
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
    public void HandleCancel()
    {
        transform.gameObject.SetActive(false);
        MenuUI.instance.Show();
    }



    // 설정된 환경설정 변수를 버튼에 출력
    public void DisplaySettingText()
    {
        animationButton.GetComponentInChildren<Text>().text = Setting.GetCurrentAnimationState().ToString();
        hpButton.GetComponentInChildren<Text>().text = Setting.GetCurrentHpState().ToString();
        timeButton.GetComponentInChildren<Text>().text = Setting.GetCurrentTimeState().ToString();
    }
}

