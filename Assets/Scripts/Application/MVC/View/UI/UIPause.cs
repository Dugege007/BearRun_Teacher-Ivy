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

            // ����
            mGameModel.Score.RegisterWithInitValue(score =>
            {
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ���
            mGameModel.Coin.RegisterWithInitValue(coin =>
            {
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ����
            mGameModel.Distance.RegisterWithInitValue(distance =>
            {
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ��ҳ��ť
            BtnHome.onClick.AddListener(() =>
            {

            });

            // ������ť
            BtnContinue.onClick.AddListener(() =>
            {
                SendEvent(Consts.E_ResumeGame);
            });
        }

        private void OnEnable()
        {
            mGameModel.Score.Value = mGameModel.Distance.Value + mGameModel.GoalCount.Value * 5;
            ScoreText.text = "������" + mGameModel.Score.Value;
            CoinText.text = "��ң�$" + mGameModel.Coin.Value;
            DistanceText.text = "���룺" + mGameModel.Distance.Value + "m";
        }

        public override void HandleEvent(string eventName, object data)
        {
        }
    }
}
