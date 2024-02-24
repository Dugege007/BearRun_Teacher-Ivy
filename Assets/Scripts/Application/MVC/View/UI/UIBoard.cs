using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections;

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
            UpdateSkillUI(mGameModel.MagnetCount, BtnMagnet);
            UpdateSkillUI(mGameModel.MultiplyCount, BtnMultiply);
            UpdateSkillUI(mGameModel.InvincibleCount, BtnInvincible);
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
                StartCoroutine(MagnetCDCoroutine(mGameModel.MagnetCDTime.Value, BtnMagnet));
            });

            BtnMultiply.onClick.AddListener(() =>
            {
                UseSkill(SkillType.Multiply);
                StartCoroutine(MultiplyCDCoroutine(mGameModel.MagnetCDTime.Value, BtnMultiply));
            });

            BtnInvincible.onClick.AddListener(() =>
            {
                UseSkill(SkillType.Invincible);
                StartCoroutine(InvincibleCDCoroutine(mGameModel.MagnetCDTime.Value, BtnInvincible));
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

        private void UpdateSkillUI(BindableProperty<int> count, Button btn)
        {
            Slider cdTimer = btn.transform.Find("CDTimer").GetComponent<Slider>();

            count.RegisterWithInitValue(c =>
            {
                if (c > 0 && cdTimer.value == 0)
                    btn.interactable = true;
                else
                    btn.interactable = false;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        // 双倍金币时间
        public void UseSkill(SkillType skillType)
        {
            switch (skillType)
            {
                case SkillType.Magnet:
                    ShowMagnetInfo();
                    break;

                case SkillType.Multiply:
                    ShowMultiplyInfo();
                    break;

                case SkillType.Invincible:
                    ShowInvincibleInfo();
                    break;

                default:
                    break;
            }
        }

        #region Info 显示
        public void ShowMagnetInfo()
        {
            if (mMagnetCor != null)
                StopCoroutine(mMagnetCor);

            mMagnetCor = MagnetInfoCoroutine(BoardTimer.MagnetInfo, mSkillTime);
            StartCoroutine(mMagnetCor);
        }

        public void ShowMultiplyInfo()
        {
            if (mMultiplyCor != null)
                StopCoroutine(mMultiplyCor);

            mMultiplyCor = MultiplyInfoCoroutine(BoardTimer.MultiplyInfo, mSkillTime);
            StartCoroutine(mMultiplyCor);
        }

        public void ShowInvincibleInfo()
        {
            if (mInvincibleCor != null)
                StopCoroutine(mInvincibleCor);

            mInvincibleCor = InvincibleInfoCoroutine(BoardTimer.InvincibleInfo, mSkillTime);
            StartCoroutine(mInvincibleCor);
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

        private IEnumerator MagnetCDCoroutine(float cdTime, Button skillButton)
        {
            skillButton.interactable = false;
            float timer = cdTime;
            Slider cdView = skillButton.transform.Find("CDTimer").GetComponent<Slider>();
            while (timer > 0)
            {
                if (mGameModel.GamePlaying())
                {
                    timer -= Time.deltaTime;
                    cdView.value = timer / cdTime;
                }
                yield return null;
            }
            mGameModel.MagnetCount.Value = Mathf.Max(mGameModel.MagnetCount.Value - 1, 0);

            //if (mGameModel.MagnetTimes.Value == 0)
            //    BtnMagnet.Hide();
        }
        #endregion

        #region CD 显示
        private IEnumerator MultiplyCDCoroutine(float cdTime, Button skillButton)
        {
            skillButton.interactable = false;
            float timer = cdTime;
            Slider cdView = skillButton.transform.Find("CDTimer").GetComponent<Slider>();
            while (timer > 0)
            {
                if (mGameModel.GamePlaying())
                {
                    timer -= Time.deltaTime;
                    cdView.value = timer / cdTime;
                }
                yield return null;
            }
            mGameModel.MultiplyCount.Value = Mathf.Max(mGameModel.MultiplyCount.Value - 1, 0);

            //if (mGameModel.MultiplyTimes.Value == 0)
            //    BtnMultiply.Hide();
        }

        private IEnumerator InvincibleCDCoroutine(float cdTime, Button skillButton)
        {
            skillButton.interactable = false;
            float timer = cdTime;
            Slider cdView = skillButton.transform.Find("CDTimer").GetComponent<Slider>();
            while (timer > 0)
            {
                if (mGameModel.GamePlaying())
                {
                    timer -= Time.deltaTime;
                    cdView.value = timer / cdTime;
                }
                yield return null;
            }
            mGameModel.InvincibleCount.Value = Mathf.Max(mGameModel.InvincibleCount.Value - 1, 0);

            //if (mGameModel.InvincibleTimes.Value == 0)
            //    BtnInvincible.Hide();
        }
        #endregion
    }
}
