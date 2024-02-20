using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class GameModel : Model
    {
        #region 常量
        #endregion

        #region 事件
        #endregion

        #region 字段
        private bool mIsPlaying = true;
        private bool mIsPause = false;
        #endregion

        #region 属性
        public override string Name => Consts.M_GameModel;
        public bool IsPlaying { get => mIsPlaying; set => mIsPlaying = value; }
        public bool IsPause { get => mIsPause; set => mIsPause = value; }
        #endregion

        #region Unity回调
        #endregion

        #region 事件回调
        #endregion

        #region 方法
        #endregion

        #region 帮助方法
        #endregion
    }
}
