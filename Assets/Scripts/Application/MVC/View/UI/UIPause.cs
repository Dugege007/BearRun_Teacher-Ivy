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

            // ����
            mGameModel.Score.RegisterWithInitValue(score =>
            {
                ScoreText.text = "������" + score;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ���
            mGameModel.Coin.RegisterWithInitValue(coin =>
            {
                CoinText.text = "��ң�$" + coin;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ����
            mGameModel.Distance.RegisterWithInitValue(distance =>
            {
                DistanceText.text = "���룺" + distance + "m";

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

        public override void HandleEvent(string eventName, object data)
        {
        }
    }
}
