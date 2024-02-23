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

    // ����洢һЩ��̬�ֶ�
    public static class Consts
    {
        // Event ����
        public const string E_ExitScene = "E_ExitScene"; // SceneArgs
        public const string E_EnterScene = "E_EnterScene"; // SceneArgs
        public const string E_StartUp = "E_StartUp";
        public const string E_EndGame = "E_EndGame";

        // UI ���
        public const string E_UpdateDistance = "E_UpdateDistance"; // DistanceArgs

        // Model ����
        public const string M_GameModel = "M_GameModel";

        // View ����
        public const string V_PlayerMove = "V_PlayerMove";
        public const string V_PlayerAnim = "V_PlayerAnim";
        public const string V_UIBoard = "V_UIBoard";
    }
}
