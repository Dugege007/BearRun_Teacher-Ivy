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
        public const string E_HitItem = "E_HitItem"; // ItemArgs

        public const string E_HitGoalTrigger = "E_HitGoalTrigger"; // 可以开始射门
        public const string E_ClickBtnGoal = "E_ClickBtnGoal"; // 可以开始射门

        // 结算
        public const string E_FinalShowUI = "E_FinalShowUI";

        // Model 名称
        public const string M_GameModel = "M_GameModel";

        // View 名称
        public const string V_PlayerMove = "V_PlayerMove";
        public const string V_PlayerAnim = "V_PlayerAnim";
        public const string V_UIBoard = "V_UIBoard";
        public const string V_UIPause = "V_UIPause";
        public const string V_UIResume = "V_UIResume";
        public const string V_UIGameOver = "V_UIGameOver";
        public const string V_UIFinalScore = "V_UIFinalScore";
    }
}
