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
        private float mSkillTime = 5;
        #endregion

        #region ����
        public override string Name => Consts.M_GameModel;

        public bool IsPlaying { get => mIsPlaying; set => mIsPlaying = value; }
        public bool IsPause { get => mIsPause; set => mIsPause = value; }
        public float SkillTime { get => mSkillTime; set => mSkillTime = value; }
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
