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
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 金币
            mGameModel.Coin.RegisterWithInitValue(coin =>
            {
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 距离
            mGameModel.Distance.RegisterWithInitValue(distance =>
            {
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

        private void OnEnable()
        {
            mGameModel.Score.Value = mGameModel.Distance.Value + mGameModel.GoalCount.Value * 5;
            ScoreText.text = "分数：" + mGameModel.Score.Value;
            CoinText.text = "金币：$" + mGameModel.Coin.Value;
            DistanceText.text = "距离：" + mGameModel.Distance.Value + "m";
        }

        public override void HandleEvent(string eventName, object data)
        {
        }
    }
}
