using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectUI : MonoBehaviour
{
    public static InspectUI instance;
    public int frameChangeCount;
    public float frameTimer;
    public Text frameText;
    public Text kinectText;

    void Awake() { instance = this; }
        
    void Start()
    {
        InitialFrameCount();
        InitialText();
    }

    private void FixedUpdate()
    {
        HandleDisplay();
        if (Setting.GetDisplayInspect())
            HandleInspectDisplay();
        else
            InitialText();
    }

    void HandleInspectDisplay() {
        DisplayFrameRate();
        DisplayKinectState();
    }

    void DisplayFrameRate()
    {
        frameTimer += Time.fixedDeltaTime;
        if (frameTimer >= 1)
        {
            frameText.text = frameChangeCount.ToString() + " fps";
            InitialFrameCount();
        }
    }

    void DisplayKinectState()
    {
        if (GameManager.instance.GetKinectState())
            kinectText.text = "Kinect found.";
        else
            kinectText.text = "Kinect not found.";
    }

    void HandleDisplay() {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Setting.SetDisplayInspect(!Setting.GetDisplayInspect());
            InitialFrameCount();
        }     
    }

    void InitialFrameCount() {
        frameChangeCount = 0;
        frameTimer = 0;
    }

    void InitialText() {
        frameText.text = "";
        kinectText.text = "";
    }
}
