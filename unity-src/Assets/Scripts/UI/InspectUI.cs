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
        frameChangeCount = 0;
        frameTimer = 0;
    }

    private void FixedUpdate()
    {
        DisplayFrameRate();
        DisplayKinectState();
        HandleDisplay();
    }

    void DisplayFrameRate()
    {
        frameTimer += Time.fixedDeltaTime;
        if (frameTimer >= 1)
        {
            if(Setting.GetDisplayInspect())
                frameText.text = frameChangeCount.ToString() + " fps";
            else
                frameText.text = "";
            frameChangeCount = 0;
            frameTimer = 0;
        }
    }

    void DisplayKinectState() {
        if (Setting.GetDisplayInspect())
            if (GameManager.instance.GetKinectState())
                kinectText.text = "Kinect found.";                
            else
                kinectText.text = "Kinect not found.";
        else
            kinectText.text = "";


    }

    void HandleDisplay() {
        if (Input.GetKeyDown(KeyCode.F1))
            Setting.SetDisplayInspect(!Setting.GetDisplayInspect());
    }
}
