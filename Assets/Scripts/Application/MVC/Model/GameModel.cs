using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class GameModel : Model
    {
        #region ����
        #endregion

        #region �¼�
        #endregion

        #region �ֶ�
        private bool mIsPlaying = true;
        private bool mIsPause = false;
        #endregion

        #region ����
        public override string Name => Consts.M_GameModel;
        public bool IsPlaying { get => mIsPlaying; set => mIsPlaying = value; }
        public bool IsPause { get => mIsPause; set => mIsPause = value; }
        #endregion

        #region Unity�ص�
        #endregion

        #region �¼��ص�
        #endregion

        #region ����
        #endregion

        #region ��������
        #endregion
    }
}
