using QFramework;

namespace BearRun
{
    public class GameModel : Model
    {
        #region 常量
        #endregion

        #region 事件
        #endregion

        #region 字段
        public BindableProperty<bool> IsPlaying = new(true);
        public BindableProperty<bool> IsPause = new(false);

        public BindableProperty<int> GoalCount = new(0);
        public BindableProperty<int> Score = new(0);
        public BindableProperty<int> Coin = new(0);
        public BindableProperty<int> Distance = new(0);

        public BindableProperty<float> GameTime = new(50f);
        public BindableProperty<float> AddTime = new(10f);
        public BindableProperty<float> SkillTime = new(5f);

        public BindableProperty<int> MagnetCount = new(1);
        public BindableProperty<int> MultiplyCount = new(2);
        public BindableProperty<int> InvincibleCount = new(3);

        public BindableProperty<float> MagnetCDTime = new(5f);
        public BindableProperty<float> MultiplyCDTime = new(5f);
        public BindableProperty<float> InvincibleCDTime = new(5f);

        public BindableProperty<bool> CanGoal = new(false);
        public BindableProperty<float> GoalTime = new(1.5f);

        public BindableProperty<int> BriberyPrice = new(100);
        public BindableProperty<int> BriberyTimes = new(0);

        public BindableProperty<int> Level = new(1);
        public BindableProperty<int> Exp = new(0);
        public BindableProperty<int> LevelExp = new(100);
        #endregion

        #region 属性
        public override string Name => Consts.M_GameModel;
        #endregion

        #region Unity回调
        #endregion

        #region 事件回调
        #endregion

        #region 方法
        public void Init()
        {
            IsPlaying.Value = true;
            IsPause.Value = false;

            GoalCount.Value = 0;
            Score.Value = 0;
            Coin.Value = 500;
            Distance.Value = 0;

            GameTime.Value = 50f;
            AddTime.Value = 20f;
            SkillTime.Value = 5f;

            MagnetCount.Value = 1;
            MultiplyCount.Value = 2;
            InvincibleCount.Value = 3;

            MagnetCDTime.Value = 5f;
            MultiplyCDTime.Value = 5f;
            InvincibleCDTime.Value = 5f;

            CanGoal.Value = false;
            GoalTime.Value = 1.5f;

            BriberyPrice.Value = 100;
            BriberyTimes.Value = 0;

            Level.Value = 1;
            Exp.Value = 0;
            LevelExp.Value = 100;
        }

        public bool GamePlaying()
        {
            return IsPause.Value == false && IsPlaying.Value;
        }
        #endregion

        #region 帮助方法
        #endregion
    }
}
