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

            BtnBribery.onClick.AddListener(() =>
            {
                // 继续游戏
            });

            BtnClose.onClick.AddListener(() =>
            {
                // 结束游戏
                SendEvent(Consts.E_FinalShowUI);
            });
        }

        public override void HandleEvent(string eventName, object data)
        {
            
        }
    }
}
