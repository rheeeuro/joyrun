using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingState
{
    kinect,
    animation
}

public enum HpState
{
    normal,
    immortal
}

public enum TimeState
{
    normal,
    infinite
}



public static class Setting
{
    private static MovingState currentMovingState = MovingState.animation;
    private static HpState currentHpState = HpState.normal;
    private static TimeState currentTimeState = TimeState.normal;

    public static MovingState GetCurrentMovingState() {
        return currentMovingState;
    }

    public static void SetCurrentMovingState(MovingState newMovingState) {
        currentMovingState = newMovingState;
    }

    public static HpState GetCurrentHpState()
    {
        return currentHpState;
    }

    public static void SetCurrentHpState(HpState newHpState)
    {
        currentHpState = newHpState;
    }

    public static TimeState GetCurrentTimeState() {
        return currentTimeState;
    }

    public static void SetCurrentTimeState(TimeState newTimeState) {
        currentTimeState = newTimeState;
    }


}
