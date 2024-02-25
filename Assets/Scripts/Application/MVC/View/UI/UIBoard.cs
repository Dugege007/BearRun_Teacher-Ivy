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

        public Button BtnPause;
        public Button BtnMagnet;
        public Button BtnMultiply;
        public Button BtnInvincible;
        public Button BtnFootball;
        public Slider BoardFootball;

        private float mMaxGameTime;

        private IEnumerator mMagnetCor;
        private IEnumerator mMultiplyCor;
        private IEnumerator mInvincibleCor;
        private float mSkillTime;

        private IEnumerator mGoalCor;

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

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 时间
            mGameModel.GameTime.RegisterWithInitValue(gameTime =>
            {
                if (gameTime > mMaxGameTime)
                    mMaxGameTime = gameTime;

                timeSlider.value = gameTime / mMaxGameTime;
                BoardTimer.TimeText.text = gameTime.ToString("0.00") + "s";

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            #endregion

            #region 按钮
            // 暂停按钮
            BtnPause.onClick.AddListener(() =>
            {
                SendEvent(Consts.E_PauseGame);
            });

            // 技能按钮
            BtnMagnet.onClick.AddListener(() =>
            {
                UseSkill(SkillType.Magnet);
                StartCoroutine(MagnetCDCoroutine(mGameModel.MagnetCDTime.Value, BtnMagnet));
                // 在 HitItemController 中完成技能数量的消耗
            });

            BtnMultiply.onClick.AddListener(() =>
            {
                UseSkill(SkillType.Multiply);
                StartCoroutine(MultiplyCDCoroutine(mGameModel.MagnetCDTime.Value, BtnMultiply));
                // 在 HitItemController 中完成技能数量的消耗
            });

            BtnInvincible.onClick.AddListener(() =>
            {
                UseSkill(SkillType.Invincible);
                StartCoroutine(InvincibleCDCoroutine(mGameModel.MagnetCDTime.Value, BtnInvincible));
                // 在 HitItemController 中完成技能数量的消耗
            });

            Text magnetCountText = BtnMagnet.transform.Find("CountText").GetComponent<Text>();
            Text multiplyCountText = BtnMultiply.transform.Find("CountText").GetComponent<Text>();
            Text invincibleCountText = BtnInvincible.transform.Find("CountText").GetComponent<Text>();

            // 按钮显示
            mGameModel.MagnetCount.RegisterWithInitValue(count =>
            {
                if (count > 0)
                    BtnMagnet.interactable = true;
                else
                    BtnMagnet.interactable = false;

                magnetCountText.text = "x" + count.ToString();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            mGameModel.MultiplyCount.RegisterWithInitValue(count =>
            {
                if (count > 0)
                    BtnMultiply.interactable = true;
                else
                    BtnMultiply.interactable = false;

                multiplyCountText.text = "x" + count.ToString();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            mGameModel.InvincibleCount.RegisterWithInitValue(count =>
            {
                if (count > 0)
                    BtnInvincible.interactable = true;
                else
                    BtnInvincible.interactable = false;

                invincibleCountText.text = "x" + count.ToString();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 射门按钮
            BtnFootball.onClick.AddListener(() =>
            {
                mGameModel.CanGoal.Value = false;
                SendEvent(Consts.E_ClickBtnGoal);
            });

            mGameModel.CanGoal.RegisterWithInitValue(canGoal =>
            {
                if (canGoal)
                {
                    BoardFootball.Show();
                    StartCountGoalTime();
                }
                else
                {
                    BoardFootball.Hide();
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
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

        public override void RegisterAttentionEvent()
        {
        }

        public override void HandleEvent(string eventName, object data)
        {
        }

        // 双倍金币时间
        public void UseSkill(SkillType skillType)
        {
            ItemArgs eItemArgs = new ItemArgs()
            {
                SkillCount = 1,
                SkillType = skillType,
            };

            SendEvent(Consts.E_HitItem, eItemArgs);
        }

        #region Info 显示
        public void ShowMagnetInfo() // 由 HitItemController 调用
        {
            if (mMagnetCor != null)
                StopCoroutine(mMagnetCor);

            mMagnetCor = MagnetInfoCoroutine(BoardTimer.MagnetInfo, mSkillTime);
            StartCoroutine(mMagnetCor);
        }

        public void ShowMultiplyInfo() // 由 HitItemController 调用
        {
            if (mMultiplyCor != null)
                StopCoroutine(mMultiplyCor);

            mMultiplyCor = MultiplyInfoCoroutine(BoardTimer.MultiplyInfo, mSkillTime);
            StartCoroutine(mMultiplyCor);
        }

        public void ShowInvincibleInfo() // 由 HitItemController 调用
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

            if (mGameModel.MagnetCount.Value > 0)
                skillButton.interactable = true;
            else
                skillButton.interactable = false;
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

            if (mGameModel.MultiplyCount.Value > 0)
                skillButton.interactable = true;
            else
                skillButton.interactable = false;
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

            if (mGameModel.InvincibleCount.Value > 0)
                skillButton.interactable = true;
            else
                skillButton.interactable = false;
        }
        #endregion

        #region 射门
        private void StartCountGoalTime()
        {
            if (mGoalCor != null)
                StopCoroutine(mGoalCor);

            mGoalCor = GoalTimeCount();
            StartCoroutine(mGoalCor);
        }

        private IEnumerator GoalTimeCount()
        {
            float timer = mGameModel.GoalTime.Value;
            while (timer > 0)
            {
                if (mGameModel.GamePlaying())
                {
                    timer -= Time.deltaTime;
                    BoardFootball.value = timer / mGameModel.GoalTime.Value;
                }
                yield return null;
            }

            mGameModel.CanGoal.Value = false;
        }
        #endregion
    }
}
