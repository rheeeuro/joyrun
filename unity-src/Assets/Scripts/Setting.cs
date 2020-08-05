using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingState
{
    kinect,
    animation
}

public enum TimeState
{
    normal,
    infinite
}

public enum HpState
{
    normal,
    immortal
}

public class Setting : MonoBehaviour
{

    public static Setting instance;
    private static MovingState currentMovingState;
    private static TimeState currentTimeState;
    private static HpState currentHpState;

    public static void InitialSetting()
    {
        DisplaySetting();
        currentMovingState = MovingState.animation;
        currentTimeState = TimeState.normal;
        currentHpState = HpState.normal;

    }

    public static void DisplaySetting()
    {
        Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] is the primary, default display and is always ON.
        // Check if additional displays are available and activate each.
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
        if (Display.displays.Length > 2)
            Display.displays[2].Activate();
    }

    public static MovingState GetCurrentMovingState() {
        return currentMovingState;
    }

    public static void SetCurrentMovingState(MovingState newMovingState) {
        currentMovingState = newMovingState;
    }

    public static TimeState GetCurrentTimeState() {
        return currentTimeState;
    }

    public static void SetCurrentTimeState(TimeState newTimeState) {
        currentTimeState = newTimeState;
    }

    public static HpState GetCurrentHpState()
    {
        return currentHpState;
    }

    public static void SetCurrentHpState(HpState newHpState)
    {
        currentHpState = newHpState;
    }
}
