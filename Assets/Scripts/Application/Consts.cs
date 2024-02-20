using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public enum InputDirection
    {
        Null,
        Left,
        Right,
        Up,
        Down,
    }

    // 这里存储一些静态字段
    public static class Consts
    {
        // Event 名称
        public const string E_ExitScene = "E_ExitScene";
        public const string E_EnterScene = "E_EnterScene";
        public const string E_StartUp = "E_StartUp";

        // Model 名称
        public const string M_GameModel = "M_GameModel";

        // View 名称
        public const string V_PlayerMove = "V_PlayerMove";
        public const string V_PlayerAnim = "V_PlayerAnim";
    }
}
