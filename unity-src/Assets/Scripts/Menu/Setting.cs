using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 애니메이션 설정 (0: 키넥트, 1: 애니메이션)
public enum AnimationState { kinect, animation }

// 체력제한 설정 (0: 기본, 1: 무적)
public enum HpState { normal, immortal }

// 시간제한 설정 (0: 기본 60초, 1: 시간제한없음)
public enum TimeState { normal, infinite }



public static class Setting
{
    // ConstInfo 에 따른 초기설정값
    private static AnimationState currentAnimationState = ConstInfo.InitialAnimationState;
    private static HpState currentHpState = ConstInfo.InitialHpState;
    private static TimeState currentTimeState = ConstInfo.InitialTimeState;


    // 설정값 Getters & Setters
    public static AnimationState GetCurrentAnimationState() { return currentAnimationState; }
    public static void SetCurrentAnimationState(AnimationState newAnimationState) { currentAnimationState = newAnimationState; }

    public static HpState GetCurrentHpState() { return currentHpState; }
    public static void SetCurrentHpState(HpState newHpState) { currentHpState = newHpState; }

    public static TimeState GetCurrentTimeState() { return currentTimeState; }
    public static void SetCurrentTimeState(TimeState newTimeState) { currentTimeState = newTimeState; }


}
