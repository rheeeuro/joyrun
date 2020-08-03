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

    // 게임 체력, 콤보 관련 상수
    public const int maxHp = 100;
    public const int startHp = 50;
    public const int emptyTileComboIncrease = 1;
    public const int heartTileComboIncrease = 2;
    public const int heartTileHpIncrease = 2;

    // 게임 위치 관련 상수
    public const float left = 100;
    public const float center = 114;
    public const float right = 128;
    public const float playerStartPositionY = 1.55f;
    public const float playerStartPositionZ = -40;
    public const float tileStartPositionY = 1;
    public const float tileStartPositionZ = 80;
    public const float stepCountY = 2;
    public const float jumpConditionY = 2;
    public const float punchDistance = 3;

    // 게임 시간 관련 상수
    public const float gameTime = 60;
    public const float jumpTime = 0.6f;

    //

}
