using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstInfo
{
    // < 공통 >

    // 프레임
    public const int gameFrameRate = 60;

    // 바닥 UI 관련 상수
    public const float buttonPushLimitY = 1.5f;
    public const float buttonPushTime = 0.8f;
    public const float floorTileScaleX = 9;

    // 바닥 UI 발 위치 원 관련 상수
    public const float footPrintStartSize = 0.7f;
    public const float foorPrintScaleY = 0.005f;

    // 초기설정값
    public const AnimationState InitialAnimationState = AnimationState.animation;
    public const HpState InitialHpState = HpState.normal;
    public const TimeState InitialTimeState = TimeState.normal;



    // < Game Scene >

    // 게임 체력 상수
    public const int maxHp = 100;
    public const int startHp = 50;

    // 장애물 상호작용 관련 상수
    public const int emptyTileComboIncrease = 1;
    public const int heartTileComboIncrease = 2;
    public const int heartTileHpIncrease = 2;
    public const int balloonComboIncrease = 10;

    // 게임 UI 표시 시간 상수
    public const float comboDisplayTime = 0.7f;
    public const float damageShowTime = 0.1f;

    // 플레이어 위치 관련 상수
    public const float left = 100;
    public const float center = 114;
    public const float right = 128;
    public const float playerStartPositionY = 1.55f;
    public const float playerStartPositionZ = -40;

    // 플레이어 동작 조건 관련 상수
    public const float stepCountY = 2;
    public const float jumpHeight = 0.3f;
    public const float jumpYLimitBetweenFoots = 0.3f;
    public const float jumpXChangeLimit = 5;
    public const float punchDistance = 3;

    // 타일 위치 관련 상수
    public const float tileStartPositionY = 1;
    public const float tileStartPositionZ = 80;
    public const float tileScaleX = 14;
    public const float destroyLine = -65;
    public const float tileDistance = 60;
    public static float[] collisionPosition = { -30, -36, -42, -48 };
    public const float collisionGap = 5;

    // 속도 관련 상수
    public const float actualSpeedStart = 30;
    public const float actualSpeedMax = 90;
    public const float startTileDelay = 2;
    public const float tileDelayIncrease = 0.01f;
    public const float extraSpeedIncrease = 0.1f;

    // 이벤트 시간 관련 상수
    public const float obstableAnimStartTime = 20;
    public const float trapAnimStartTime = 30;
    public const float tileAnimationLength = 2;

    // 게임 시간 관련 상수
    public const float gameTime = 60;
    public const float jumpTime = 0.6f;
}
