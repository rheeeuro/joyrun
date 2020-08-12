using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameUI : MonoBehaviour
{
    public static FrameUI instance;
    public int changeCount;
    public float frameTimer;
    public Text frameText;

    void Awake() { instance = this; }
        
    void Start()
    {
        changeCount = 0;
        frameTimer = 0;
    }

    private void FixedUpdate()
    {
        DisplayFrameRate();
        HandleDisplay();
    }

    void DisplayFrameRate()
    {
        frameTimer += Time.fixedDeltaTime;
        if (frameTimer >= 1)
        {
            if(Setting.GetDisplayFrame())
                frameText.text = changeCount.ToString() + " fps";
            else
                frameText.text = "";
            changeCount = 0;
            frameTimer = 0;
        }
    }

    void HandleDisplay() {
        if (Input.GetKeyDown(KeyCode.F1))
            Setting.SetDisplayFrame(!Setting.GetDisplayFrame());
    }
}
