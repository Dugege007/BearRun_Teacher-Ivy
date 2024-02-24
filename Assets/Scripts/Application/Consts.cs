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

    public enum SkillType
    {
        Magnet,
        Multiply,
        Invincible,
    }

    // 这里存储一些静态字段
    public static class Consts
    {
        // Event 名称
        public const string E_ExitScene = "E_ExitScene"; // SceneArgs
        public const string E_EnterScene = "E_EnterScene"; // SceneArgs
        public const string E_StartUp = "E_StartUp";
        public const string E_EndGame = "E_EndGame";

        public const string E_PauseGame = "E_PauseGame";
        public const string E_ResumeGame = "E_ResumeGame";
        public const string E_ContinueGame = "E_ContinueGame";

        // UI 相关
        public const string E_UpdateDistance = "E_UpdateDistance"; // DistanceArgs

        // Model 名称
        public const string M_GameModel = "M_GameModel";

        // View 名称
        public const string V_PlayerMove = "V_PlayerMove";
        public const string V_PlayerAnim = "V_PlayerAnim";
        public const string V_UIBoard = "V_UIBoard";
        public const string V_UIPause = "V_UIPause";
        public const string V_UIResume = "V_UIResume";
    }
}
