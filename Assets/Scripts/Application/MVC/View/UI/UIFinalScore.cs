using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BearRun
{
    public class UIFinalScore : View
    {
        public Text DistanceCountText;
        public Text CoinCountText;
        public Text BallCountText;
        public Text FinalScoreText;

        public Slider ExpSlider;
        public Text ExpText;
        public Text LevelText;

        public Button BtnShop;
        public Button BtnHome;
        public Button BtnContinue;

        private GameModel mGameModel;

        public override string Name => Consts.V_UIFinalScore;

        private void Awake()
        {
            mGameModel = GetModel<GameModel>();

            // 基本数据
            mGameModel.Distance.RegisterWithInitValue(distance =>
            {
                DistanceCountText.text = "距离：" + distance + "m";

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            mGameModel.Coin.RegisterWithInitValue(coin =>
            {
                CoinCountText.text = "金币：$" + coin;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            mGameModel.GoalCount.RegisterWithInitValue(goal =>
            {
                BallCountText.text = "进球：" + goal + "个";

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            mGameModel.Score.RegisterWithInitValue(score =>
            {
                mGameModel.Exp.Value = score;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 经验
            mGameModel.Exp.RegisterWithInitValue(exp =>
            {
                if (exp > mGameModel.Level.Value * 100)
                {
                    // 消耗经验值
                    exp -= mGameModel.Level.Value * 100;
                    // 升级
                    mGameModel.Level.Value++;
                    mGameModel.LevelExp.Value = mGameModel.Level.Value * 100;
                }

                ExpSlider.value = (float)exp / mGameModel.LevelExp.Value;
                ExpText.text = exp + "/" + mGameModel.LevelExp.Value;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            mGameModel.Level.RegisterWithInitValue(level =>
            {
                LevelText.text = level + "级";

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 按钮
            BtnShop.onClick.AddListener(() =>
            {
                // 打开商店
            });

            BtnHome.onClick.AddListener(() =>
            {
                // 返回主页
            });

            BtnContinue.onClick.AddListener(() =>
            {
                // 继续游戏
            });
        }

        private void OnEnable()
        {
            mGameModel.Score.Value = mGameModel.Distance.Value + mGameModel.GoalCount.Value * 5;
            FinalScoreText.text = "分数：" + mGameModel.Score.Value;
        }

        public override void HandleEvent(string eventName, object data)
        {
        }
    }
}
