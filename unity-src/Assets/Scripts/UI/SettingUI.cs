using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSettingState = SettingState.animation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show()
    {
        GameManager.instance.SetGameState(GameState.menu);
        transform.gameObject.SetActive(true);
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

    public void HandleLeft() { }

    public void HandleRight() { }

    public void HandleCancel() {
        transform.gameObject.SetActive(false);
        MenuUI.instance.Show();
    }
}
