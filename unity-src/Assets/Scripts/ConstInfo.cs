using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstInfo : MonoBehaviour
{
    // 바닥 UI 관련 상수
    public const float buttonPushLimitY = 1.5f;
    public const float floorTileScaleX = 9;
    public const float pushTime = 1;

    // 바닥 UI 발 위치 원 관련 상수
    public const float footPrintStartSize = 0.7f;
    public const float foorPrintScaleY = 0.005f;

    // 게임 장애물 관련 상수
    public const int emptyTileComboIncrease = 1;
    public const int heartTileComboIncrease = 2;
    public const int heartTileHpIncrease = 2;

    // 게임 거리 관련 상수
    public const float stepCountY = 2;
    public const float jumpConditionY = 2;
    public const float punchDistance = 3;

    // 게임 시간 관련 상수

    //
    public const int maxHp = 100;
    public const int startHp = 50;
}
