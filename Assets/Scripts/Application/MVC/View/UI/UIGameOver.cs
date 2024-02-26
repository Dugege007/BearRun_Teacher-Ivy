using QFramework;
using UnityEngine.UI;

namespace BearRun
{
    public class UIGameOver : View
    {
        public Text NeedPayText;
        public Button BtnBribery;
        public Button BtnClose;

        private GameModel mGameModel;

        public override string Name => Consts.V_UIGameOver;

        private void Awake()
        {
            mGameModel = GetModel<GameModel>();

            mGameModel.BriberyPrice.RegisterWithInitValue(price =>
            {
                NeedPayText.text = "$" + price;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            mGameModel.BriberyTimes.RegisterWithInitValue(time =>
            {
                mGameModel.BriberyPrice.Value *= 2;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            BtnBribery.onClick.AddListener(() =>
            {
                // ���Ǯ������Ǯ��������Ϸ
                mGameModel.Coin.Value -= mGameModel.BriberyPrice.Value;
                mGameModel.BriberyTimes.Value++;
                // ��ʱ
                mGameModel.GameTime.Value += 20f;
                SendEvent(Consts.E_ResumeGame);
            });

            BtnClose.onClick.AddListener(() =>
            {
                // ������Ϸ
                SendEvent(Consts.E_FinalShowUI);
            });
        }

        private void OnEnable()
        {
            if (mGameModel.Coin.Value >= mGameModel.BriberyPrice.Value)
                BtnBribery.interactable = true;
            else
                BtnBribery.interactable = false;
        }

        public override void HandleEvent(string eventName, object data)
        {

        }
    }
}
