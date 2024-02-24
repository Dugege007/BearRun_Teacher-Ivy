using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections;
using UnityEditor.Experimental.GraphView;

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

        private float mMaxGameTime;

        private IEnumerator mMagnetCor;
        private IEnumerator mMultiplyCor;
        private IEnumerator mInvincibleCor;

        private float mSkillTime;

        public override string Name => Consts.V_UIBoard;

        private void Awake()
        {
            mGameModel = GetModel<GameModel>();
            Slider timeSlider = BoardTimer.GetComponent<Slider>();
            mGameModel.GameTime.Value = mStartTime;
            mMaxGameTime = mStartTime;
            mSkillTime = mGameModel.SkillTime.Value;

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
                if (gameTime > mMaxGameTime)
                    mMaxGameTime = gameTime;

                timeSlider.value = gameTime / mMaxGameTime;
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

            BtnMagnet.onClick.AddListener(() =>
            {
                UseSkill(SkillType.Magnet);
            });

            BtnMultiply.onClick.AddListener(() =>
            {
                UseSkill(SkillType.Multiply);
            });

            BtnInvincible.onClick.AddListener(() =>
            {
                UseSkill(SkillType.Invincible);
            });
            #endregion
        }

        private void Update()
        {
            if (mGameModel.GamePlaying())
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

        // 双倍金币时间
        public void UseSkill(SkillType skillType)
        {
            switch (skillType)
            {
                case SkillType.Magnet:
                    if (mMagnetCor != null)
                        StopCoroutine(mMagnetCor);
                    mMagnetCor = MagnetInfoCoroutine(BoardTimer.MagnetInfo, mSkillTime);
                    StartCoroutine(mMagnetCor);
                    break;

                case SkillType.Multiply:
                    if (mMultiplyCor != null)
                        StopCoroutine(mMultiplyCor);
                    mMultiplyCor = MultiplyInfoCoroutine(BoardTimer.MultiplyInfo, mSkillTime);
                    StartCoroutine(mMultiplyCor);
                    break;

                case SkillType.Invincible:
                    if (mInvincibleCor != null)
                        StopCoroutine(mInvincibleCor);
                    mInvincibleCor = InvincibleInfoCoroutine(BoardTimer.InvincibleInfo, mSkillTime);
                    StartCoroutine(mInvincibleCor);
                    break;

                default:
                    break;
            }

        }

        private IEnumerator MagnetInfoCoroutine(Image skill, float timer)
        {
            Text remainTime = skill.transform.Find("TimeText").GetComponent<Text>();
            skill.Show();
            while (timer > 0)
            {
                if (mGameModel.GamePlaying())
                {
                    timer -= Time.deltaTime;
                    remainTime.text = timer.ToString("0.0");
                }
                yield return null;
            }
            
            skill.Hide();
        }

        private IEnumerator MultiplyInfoCoroutine(Image skill, float timer)
        {
            Text remainTime = skill.transform.Find("TimeText").GetComponent<Text>();
            skill.Show();
            while (timer > 0)
            {
                if (mGameModel.GamePlaying())
                {
                    timer -= Time.deltaTime;
                    remainTime.text = timer.ToString("0.0");
                }
                yield return null;
            }
            
            skill.Hide();
        }

        private IEnumerator InvincibleInfoCoroutine(Image skill, float timer)
        {
            Text remainTime = skill.transform.Find("TimeText").GetComponent<Text>();
            skill.Show();
            while (timer > 0)
            {
                if (mGameModel.GamePlaying())
                {
                    timer -= Time.deltaTime;
                    remainTime.text = timer.ToString("0.0");
                }
                yield return null;
            }
            
            skill.Hide();
        }
    }
}
