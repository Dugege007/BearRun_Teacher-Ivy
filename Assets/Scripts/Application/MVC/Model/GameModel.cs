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
        public BindableProperty<float> SkillTime = new(5f);

        public BindableProperty<int> Coin = new(0);
        public BindableProperty<int> Distance = new(0);

        #endregion

        #region 属性
        public override string Name => Consts.M_GameModel;
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
