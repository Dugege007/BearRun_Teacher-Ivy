using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace BearRun
{
    public class UIPause : View
    {
        private GameModel mGameModel;

        public Text ScoreText;
        public Text CoinText;
        public Text DistanceText;
        public Button BtnHome;
        public Button BtnContinue;

        public override string Name => Consts.V_UIPause;

        private void Awake()
        {
            mGameModel = GetModel<GameModel>();

            // 分数
            mGameModel.Score.RegisterWithInitValue(score =>
            {
                ScoreText.text = "分数：" + score;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 金币
            mGameModel.Coin.RegisterWithInitValue(coin =>
            {
                CoinText.text = "金币：$" + coin;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 距离
            mGameModel.Distance.RegisterWithInitValue(distance =>
            {
                DistanceText.text = "距离：" + distance + "m";

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 主页按钮
            BtnHome.onClick.AddListener(() =>
            {

            });

            // 继续按钮
            BtnContinue.onClick.AddListener(() =>
            {
                SendEvent(Consts.E_ResumeGame);
            });
        }

        public override void HandleEvent(string eventName, object data)
        {
        }
    }
}
