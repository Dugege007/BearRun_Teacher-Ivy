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

            // ��������
            mGameModel.Distance.RegisterWithInitValue(distance =>
            {
                DistanceCountText.text = "���룺" + distance + "m";

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            mGameModel.Coin.RegisterWithInitValue(coin =>
            {
                CoinCountText.text = "��ң�$" + coin;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            mGameModel.GoalCount.RegisterWithInitValue(goal =>
            {
                BallCountText.text = "����" + goal + "��";

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            mGameModel.Score.RegisterWithInitValue(score =>
            {
                mGameModel.Exp.Value = score;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ����
            mGameModel.Exp.RegisterWithInitValue(exp =>
            {
                if (exp > mGameModel.Level.Value * 100)
                {
                    // ���ľ���ֵ
                    exp -= mGameModel.Level.Value * 100;
                    // ����
                    mGameModel.Level.Value++;
                    mGameModel.LevelExp.Value = mGameModel.Level.Value * 100;
                }

                ExpSlider.value = (float)exp / mGameModel.LevelExp.Value;
                ExpText.text = exp + "/" + mGameModel.LevelExp.Value;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            mGameModel.Level.RegisterWithInitValue(level =>
            {
                LevelText.text = level + "��";

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ��ť
            BtnShop.onClick.AddListener(() =>
            {
                // ���̵�
            });

            BtnHome.onClick.AddListener(() =>
            {
                // ������ҳ
            });

            BtnContinue.onClick.AddListener(() =>
            {
                // ������Ϸ
            });
        }

        private void OnEnable()
        {
            mGameModel.Score.Value = mGameModel.Distance.Value + mGameModel.GoalCount.Value * 5;
            FinalScoreText.text = "������" + mGameModel.Score.Value;
        }

        public override void HandleEvent(string eventName, object data)
        {
        }
    }
}
