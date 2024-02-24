using QFramework;

namespace BearRun
{
    public class GameModel : Model
    {
        #region ����
        #endregion

        #region �¼�
        #endregion

        #region �ֶ�
        public BindableProperty<bool> IsPlaying = new(true);
        public BindableProperty<bool> IsPause = new(false);

        public BindableProperty<int> GoalCount = new(0);
        public BindableProperty<int> Score = new(0);
        public BindableProperty<int> Coin = new(0);
        public BindableProperty<int> Distance = new(0);

        public BindableProperty<float> GameTime = new(50f);
        public BindableProperty<float> AddTime = new(10f);
        public BindableProperty<float> SkillTime = new(5f);
        public BindableProperty<float> MagnetTime = new(0);
        public BindableProperty<float> MultiplyTime = new(0);
        public BindableProperty<float> InvincibleTime = new(0);

        #endregion

        #region ����
        public override string Name => Consts.M_GameModel;
        #endregion

        #region Unity�ص�
        #endregion

        #region �¼��ص�
        #endregion

        #region ����
        public void Init()
        {
            IsPlaying.Value = true;
            IsPause.Value = false;

            GoalCount.Value = 0;
            Score.Value = 0;
            Coin.Value = 0;
            Distance.Value = 0;

            GameTime.Value = 50f;
            AddTime.Value = 20f;
            SkillTime.Value = 5f;
            MagnetTime.Value = 3f;
            MultiplyTime.Value = 3f;
            InvincibleTime.Value = 3f;
        }

        public bool GamePlaying()
        {
            return IsPause.Value == false && IsPlaying.Value;
        }
        #endregion

        #region ��������
        #endregion
    }
}
