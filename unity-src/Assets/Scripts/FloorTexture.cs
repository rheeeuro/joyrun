using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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
    
    public static Texture2D FloorTileSelected;
    public static Texture2D FloorTileUnSelected;



    void Start()
    {
        PositionButton = Resources.Load("UIre/Floor/PositionButton") as Texture2D;
        PositionButtonBlue = Resources.Load("UIre/Floor/PositionButtonBlue") as Texture2D;
        PositionDisabled = Resources.Load("UIre/Floor/PositionDisabled") as Texture2D;
        BackButton = Resources.Load("UIre/Floor/BackButton") as Texture2D;
        BackButtonPress = Resources.Load("UIre/Floor/BackButtonPress") as Texture2D;
        DownArrowButton = Resources.Load("UIre/Floor/DownArrowButton") as Texture2D;
        DownArrowButtonPress = Resources.Load("UIre/Floor/DownArrowButtonPress") as Texture2D;
        HomeButton = Resources.Load("UIre/Floor/HomeButton") as Texture2D;
        HomeButtonPress = Resources.Load("UIre/Floor/HomeButtonPress") as Texture2D;
        LeftArrowButton = Resources.Load("UIre/Floor/LeftArrowButton") as Texture2D;
        LeftArrowButtonPress = Resources.Load("UIre/Floor/LeftArrowButtonPress") as Texture2D;
        LeftButton = Resources.Load("UIre/Floor/LeftButton") as Texture2D;
        LeftButtonPress = Resources.Load("UIre/Floor/LeftButtonPress") as Texture2D;
        RightArrowButton = Resources.Load("UIre/Floor/RightArrowButton") as Texture2D;
        RightArrowButtonPress = Resources.Load("UIre/Floor/RightArrowButtonPress") as Texture2D;
        RightButton = Resources.Load("UIre/Floor/RightButton") as Texture2D;
        RightButtonPress = Resources.Load("UIre/Floor/RightButtonPress") as Texture2D;
        UpArrowButton = Resources.Load("UIre/Floor/UpArrowButton") as Texture2D;
        UpArrowButtonPress = Resources.Load("UIre/Floor/UpArrowButtonPress") as Texture2D;
        RemeasurementButton = Resources.Load("UIre/Floor/Re-measurementButton") as Texture2D;
        RemeasurementButtonPress = Resources.Load("UIre/Floor/Re-measurementButtonPress") as Texture2D;
        FloorTileSelected = Resources.Load("Image/FloorTileSelected") as Texture2D;
        FloorTileUnSelected = Resources.Load("Image/FloorTileUnSelected") as Texture2D;
    }


    public static void setButtonTexture(GameObject obj, Texture newTexture)
    {
        obj.transform.GetChild(0).GetComponent<RawImage>().texture = newTexture;
    }

    public static void setFloorTileTexture(GameObject obj, Texture newTexture)
    {
        obj.GetComponent<RawImage>().texture = newTexture;
    }

    public static void ProgressDelayTexture(GameObject obj, float ratio) {
        obj.transform.GetChild(1).GetComponent<Image>().fillAmount = 1 - ratio;
    }

    public static void MoveAllChildTexture(GameObject obj, bool move) {
        if (move)
        {
            for (int i = 0; i < 3; i++)
            {
                obj.transform.GetChild(i).transform.localPosition =
                    new Vector3(obj.transform.GetChild(i).transform.localPosition.x, -10);
            }
        }
        else 
        {
            for (int i = 0; i < 3; i++)
            {
                obj.transform.GetChild(i).transform.localPosition =
                    new Vector3(obj.transform.GetChild(i).transform.localPosition.x, 0);
            }
        }
    }
}
