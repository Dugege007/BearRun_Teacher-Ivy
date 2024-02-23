using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace BearRun
{
    public partial class UIBoard : View
    {
        private const float mStartTime = 50f;

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

        private float mMaxTime;

        public override string Name => Consts.V_UIBoard;

        private void Awake()
        {
            mGameModel = GetModel<GameModel>();
            Slider timeSlider = BoardTimer.GetComponent<Slider>();
            mGameModel.GameTime.Value = mStartTime;
            mMaxTime = mStartTime;

            #region 信息
            // 金币
            mGameModel.Coin.RegisterWithInitValue(coin =>
            {
                CoinText.text = "$" + coin;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 距离
            mGameModel.Distance.RegisterWithInitValue(distance =>
            {
                DistanceText.text = distance + "m";
                // 分数
                mGameModel.Score.Value = distance + mGameModel.GoalCount.Value * 5;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 时间
            mGameModel.GameTime.RegisterWithInitValue(gameTime =>
            {
                if (gameTime > mMaxTime)
                    mMaxTime = gameTime;

                timeSlider.value = gameTime / mMaxTime;
                BoardTimer.TimeText.text = gameTime.ToString("0.00") + "s";

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 更新技能 UI
            UpdateSkillUI(mGameModel.MagnetTime, BtnMagnet);
            UpdateSkillUI(mGameModel.MultiplyTime, BtnMultiply);
            UpdateSkillUI(mGameModel.InvincibleTime, BtnInvincible);
            #endregion

            #region 按钮
            // 暂停按钮
            BtnPause.onClick.AddListener(() =>
            {
                SendEvent(Consts.E_PauseGame);
            });
            #endregion
        }

        private void Update()
        {
            if (mGameModel.IsPause.Value == false && mGameModel.IsPlaying.Value)
                mGameModel.GameTime.Value -= Time.deltaTime;

            if (mGameModel.GameTime.Value < 0)
            {
                mGameModel.GameTime.Value = 0;
                SendEvent(Consts.E_EndGame);
            }
        }

        public override void HandleEvent(string eventName, object data)
        {
        }

        private void UpdateSkillUI(BindableProperty<float> time, Button btn)
        {
            Slider cdTimer = btn.transform.Find("CDTimer").GetComponent<Slider>();

            time.RegisterWithInitValue(t =>
            {
                if (t > 0)
                {
                    btn.interactable = true;
                    cdTimer.value = 0;
                }
                else
                {
                    btn.interactable = false;
                    cdTimer.value = 1;
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}
