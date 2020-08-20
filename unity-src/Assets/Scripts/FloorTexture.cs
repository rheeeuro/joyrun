using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class FloorTexture : MonoBehaviour
{
    public static Texture2D PositionButton;
    public static Texture2D PositionButtonBlue;
    public static Texture2D PositionDisabled;
    public static Texture2D BackButton;
    public static Texture2D BackButtonPress;
    public static Texture2D DownArrowButton;
    public static Texture2D DownArrowButtonPress;
    public static Texture2D HomeButton;
    public static Texture2D HomeButtonPress;
    public static Texture2D LeftArrowButton;
    public static Texture2D LeftArrowButtonPress;
    public static Texture2D LeftButton;
    public static Texture2D LeftButtonPress;
    public static Texture2D RightArrowButton;
    public static Texture2D RightArrowButtonPress;
    public static Texture2D RightButton;
    public static Texture2D RightButtonPress;
    public static Texture2D UpArrowButton;
    public static Texture2D UpArrowButtonPress;
    public static Texture2D RemeasurementButton;
    public static Texture2D RemeasurementButtonPress;


    void Start()
    {
        PositionButton = Resources.Load("UIre/Floor/PositionButton.png") as Texture2D;
        PositionButtonBlue = Resources.Load("UIre/Floor/PositionButtonBlue.png") as Texture2D;
        PositionDisabled = Resources.Load("UIre/Floor/PositionDisabled.png") as Texture2D;
        BackButton = Resources.Load("UIre/Floor/BackButton.png") as Texture2D;
        BackButtonPress = Resources.Load("UIre/Floor/BackButtonPress.png") as Texture2D;
        DownArrowButton = Resources.Load("UIre/Floor/DownArrowButton.png") as Texture2D;
        DownArrowButtonPress = Resources.Load("UIre/Floor/DownArrowButtonPress.png") as Texture2D;
        HomeButton = Resources.Load("UIre/Floor/HomeButton.png") as Texture2D;
        HomeButtonPress = Resources.Load("UIre/Floor/HomeButtonPress.png") as Texture2D;
        LeftArrowButton = Resources.Load("UIre/Floor/LeftArrowButton.png") as Texture2D;
        LeftArrowButtonPress = Resources.Load("UIre/Floor/LeftArrowButtonPress.png") as Texture2D;
        LeftButton = Resources.Load("UIre/Floor/LeftButton.png") as Texture2D;
        LeftButtonPress = Resources.Load("UIre/Floor/LeftButtonPress.png") as Texture2D;
        RightArrowButton = Resources.Load("UIre/Floor/RightArrowButton.png") as Texture2D;
        RightArrowButtonPress = Resources.Load("UIre/Floor/RightArrowButtonPress.png") as Texture2D;
        RightButton = Resources.Load("UIre/Floor/RightButton.png") as Texture2D;
        RightButtonPress = Resources.Load("UIre/Floor/RightButtonPress.png") as Texture2D;
        UpArrowButton = Resources.Load("UIre/Floor/UpArrowButton.png") as Texture2D;
        UpArrowButtonPress = Resources.Load("UIre/Floor/UpArrowButtonPress") as Texture2D;
        RemeasurementButton = Resources.Load("UIre/Floor/Re-measurementButton.png") as Texture2D;
        RemeasurementButtonPress = Resources.Load("UIre/Floor/Re-measurementButtonPress.png") as Texture2D;
    }


    public static void setButtonTexture(GameObject obj, Texture newTexture)
    {
        obj.transform.GetChild(0).GetComponent<RawImage>().texture = newTexture;
    }
}
