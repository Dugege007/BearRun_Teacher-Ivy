using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace BearRun
{
    public partial class UIBoard : View
    {
        #region 常量
        #endregion

        #region 事件
        #endregion

        #region 字段
        private GameModel mGameModel;

        public BoardTimer BoardTimer;

        public Text CoinText;
        public Text DistanceText;
        public Slider BoardFootball;
        public Button BtnFootball;
        public Button BtnInvincible;
        public Button BtnMultiply;
        public Button BtnMagnet;
        public Button BtnPause;
        #endregion

        #region 属性
        public override string Name => Consts.V_UIBoard;
        #endregion

        #region Unity回调
        private void Start()
        {
            mGameModel = GetModel<GameModel>();

            // 金币
            mGameModel.Coin.RegisterWithInitValue(coin =>
            {
                CoinText.text = "$" + coin;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 距离
            mGameModel.Distance.RegisterWithInitValue(distance =>
            {
                DistanceText.text = distance + "m";

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        #endregion

        #region 事件回调
        public override void HandleEvent(string eventName, object data)
        {
        }
        #endregion

        #region 方法
        #endregion

        #region 帮助方法
        #endregion



    }
}
