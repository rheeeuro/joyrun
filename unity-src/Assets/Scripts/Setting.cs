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
        currentMovingState = MovingState.animation;
        currentTimeState = TimeState.normal;
        currentHpState = HpState.normal;

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
