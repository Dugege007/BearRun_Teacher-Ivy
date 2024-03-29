using QFramework;
using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace BearRun
{
    public class PlayerMove : View
    {
        #region 常量
        private const float mMoveHorizontalOffset = 1.8f;
        private const float mMoveHorizontalSpeed = 12f;
        private const float mGravity = 9.8f;
        private const float mJumpHeight = 3f;

        private const float mSpeedAddDistance = 300f;
        private const float mSpeedAddRate = 0.5f;
        private const float mMaxSpeed = 40f;
        #endregion

        #region 事件
        #endregion

        #region 字段
        private GameModel mGameModel;
        private CharacterController mCC;
        private SphereCollider mMagnetCollider;

        // 前进速度
        private float mSpeed = 10f;
        // 输入
        private InputDirection mInputDir = InputDirection.Null;
        private bool mActiveInput = false;
        private Vector3 mMousePos;
        // X 轴位置信息：0左边；1中间；2右边
        private int mNowIndex = 1;
        private int mTargetIndex = 1;
        private float mXDistance;
        // 跳跃
        private float mYDistance;
        // 滑铲
        private bool mIsSlide = false;
        private float mSlideTime = 0;
        // 加速
        private float mSpeedAddCount;
        private float mMaskSpeed;
        private float mAddRate = 5f;
        // 撞击
        private bool mIsHit = false;

        // 行人测试
        //public Transform mPeopleGenTrans;

        // 吃道具
        private int mDoubleCoin = 1;
        private IEnumerator mMultiplyCor;
        private IEnumerator mMagnetCor;
        private bool mIsInvincible = false;
        private IEnumerator mInvincibleCor;
        private float mSkillTime;

        // 射门相关
        private GameObject mBall;
        private GameObject mTrail;
        private IEnumerator mGoalCor;
        private bool mIsGoal = false;
        private ParticleSystem mFXGoal;
        #endregion

        #region 属性
        public override string Name => Consts.V_PlayerMove;
        public float Speed { get => mSpeed; set => mSpeed = Mathf.Min(value, mMaxSpeed); }
        #endregion

        #region Unity回调
        private void Awake()
        {
            mCC = GetComponent<CharacterController>();
            mGameModel = GetModel<GameModel>();
            mMagnetCollider = GetComponentInChildren<SphereCollider>();
            mMagnetCollider.enabled = false;
            mSkillTime = mGameModel.SkillTime.Value;

            mBall = transform.Find("Ball").gameObject;
            mTrail = transform.Find("Effect").transform.Find("Trail").gameObject;
            mFXGoal = transform.Find("Effect").transform.Find("FX_GOAL").GetComponent<ParticleSystem>();
            mTrail.Hide();
        }

        private void Start()
        {
            //StartCoroutine(UpdateAction());
        }

        private void Update()
        {
            if (mGameModel.GamePlaying())
            {
                // 更新数据
                if (Time.frameCount % 10 == 0)
                {
                    mGameModel.Distance.Value = (int)transform.position.z;
                    //Debug.Log("更新距离：" + mGameModel.Distance.Value);
                }

                mYDistance -= mGravity * Time.deltaTime;
                mCC.Move(Speed * Time.deltaTime * (transform.forward + new Vector3(0, mYDistance, 0)));
                MoveControl();
                UpdatePosition();
                UpdateSpeed();
            }

            // 暂停
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                mGameModel.IsPause.Value = !mGameModel.IsPause.Value;
            }

            // 行人测试
            //if (Input.GetKeyDown(KeyCode.P))
            //{
            //    Game.Instance.ObjectPool.Allocate("Block_RandomModelLateralRun", mPeopleGenTrans)
            //        .Position(transform.position + new Vector3(2.5f, 0, 15f))
            //        .Show();
            //}
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.SmallFence))
            {
                if (mIsInvincible) return;

                other.gameObject.SendMessage("HitPlayer", transform.position, SendMessageOptions.RequireReceiver);
                HitObstacle(); // 撞击减速
            }

            if (other.gameObject.CompareTag(Tags.BigFence))
            {
                if (mIsInvincible) return;

                if (mIsSlide) return;
                other.gameObject.SendMessage("HitPlayer", transform.position, SendMessageOptions.RequireReceiver);
                HitObstacle();
            }

            if (other.gameObject.CompareTag(Tags.Block))
            {
                // 游戏结束
                other.gameObject.SendMessage("HitPlayer", transform.position, SendMessageOptions.RequireReceiver);
                Game.Instance.Sound.PlaySFX("Se_UI_End");

                SendEvent(Consts.E_EndGame);
            }

            if (other.gameObject.CompareTag(Tags.BlockChild))
            {
                // 游戏结束
                other.transform.parent.parent.gameObject.SendMessage("HitPlayer", transform.position, SendMessageOptions.RequireReceiver);
                Game.Instance.Sound.PlaySFX("Se_UI_End");

                SendEvent(Consts.E_EndGame);
            }

            if (other.gameObject.CompareTag(Tags.BeforeTrigger)) // 汽车触发器
            {
                other.transform.parent.gameObject.SendMessage("HitTrigger", SendMessageOptions.RequireReceiver);
            }

            if (other.gameObject.CompareTag(Tags.BeforeGoalTrigger)) // 准备射门
            {
                mGameModel.CanGoal.Value = true;
                // 发消息给 UIBoard
                //SendEvent(Consts.E_HitGoalTrigger);
                //other.transform.parent.gameObject.SendMessage("HitTrigger", SendMessageOptions.RequireReceiver);

                // 显示加速特效
                Game.Instance.PoolManager.Allocate<Effect>("FX_JiaSu")
                    .Parent(mTrail.transform.parent)
                    .LocalPosition(new Vector3(0, -1f, 0))
                    .LocalScale(10f / 3f * Vector3.one);
            }

            if (other.gameObject.CompareTag(Tags.GoalKeeper)) // 撞到守门员
            {
                HitObstacle();
                // 守门员被撞飞
                other.transform.parent.parent.parent.SendMessage("HitGoalKeeper", transform.position, SendMessageOptions.RequireReceiver);
            }

            if (other.gameObject.CompareTag(Tags.BallDoor))
            {
                if (mIsGoal)
                {
                    mIsGoal = false;
                    return;
                }

                HitObstacle();
                // 球门动画

                // 球网粘身上
                Game.Instance.PoolManager.Allocate<Effect>("Effect_QiuWang")
                    .Parent(mTrail.transform.parent)
                    .LocalPosition(new Vector3(0, -1f, -1f))
                    .LocalScale(5f / 3f * Vector3.one);

                other.transform.parent.parent.SendMessage("HitDoor", mNowIndex, SendMessageOptions.RequireReceiver);
                // 回收球门

            }
        }
        #endregion

        #region 事件回调
        public override void RegisterAttentionEvent()
        {
            AttentionList.Add(Consts.E_ClickBtnGoal);
        }

        public override void HandleEvent(string eventName, object data)
        {
            switch (eventName)
            {
                case Consts.E_ClickBtnGoal:
                    OnGoalClick();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 方法

        #region 移动
        // 移动
        private void MoveControl()
        {
            // 左右移动
            if (mTargetIndex != mNowIndex)
            {
                float newX = Mathf.Lerp(0, mXDistance, mMoveHorizontalSpeed * Time.deltaTime);
                // 计算本帧应该移动的距离
                Vector3 move = new Vector3(newX, 0, 0);

                // 使用 CharacterController 移动角色
                mCC.Move(move);

                mXDistance -= newX;
                //Debug.Log("move: " + move + "\n"
                //    + "transform.position: " + transform.position + "\n"
                //    + "mXDistance: " + mXDistance);

                if (Mathf.Abs(mXDistance) < 0.01f)
                {
                    mXDistance = 0;
                    mNowIndex = mTargetIndex;
                    switch (mNowIndex)
                    {
                        case 0:
                            transform.position = new Vector3(-mMoveHorizontalOffset, transform.position.y, transform.position.z);
                            Debug.Log("移动到 左");
                            break;
                        case 1:
                            transform.position = new Vector3(0, transform.position.y, transform.position.z);
                            Debug.Log("移动到 中");
                            break;
                        case 2:
                            transform.position = new Vector3(mMoveHorizontalOffset, transform.position.y, transform.position.z);
                            Debug.Log("移动到 右");
                            break;
                        default:
                            break;
                    }
                }
            }

            // 上下移动
            if (mIsSlide)
            {
                mSlideTime -= Time.deltaTime;
                if (mSlideTime <= 0)
                    mIsSlide = false;
            }
        }

        // 更新位置
        private void UpdatePosition()
        {
            GetInputDirection();

            switch (mInputDir)
            {
                case InputDirection.Null:
                    break;
                case InputDirection.Left:
                    if (mTargetIndex > 0)
                    {
                        mTargetIndex--;
                        mXDistance = -mMoveHorizontalOffset;
                        SendMessage("AnimManager", mInputDir);
                        Game.Instance.Sound.PlaySFX("Se_UI_HuaDong");
                    }
                    break;
                case InputDirection.Right:
                    if (mTargetIndex < 2)
                    {
                        mTargetIndex++;
                        mXDistance = mMoveHorizontalOffset;
                        SendMessage("AnimManager", mInputDir);
                        Game.Instance.Sound.PlaySFX("Se_UI_HuaDong");
                    }
                    break;
                case InputDirection.Up:
                    if (mCC.isGrounded)
                    {
                        mYDistance = mJumpHeight;
                        SendMessage("AnimManager", mInputDir);
                        Game.Instance.Sound.PlaySFX("Se_UI_Jump");
                    }
                    break;
                case InputDirection.Down:
                    if (mIsSlide == false)
                    {
                        mIsSlide = true;
                        mSlideTime = 0.733f;
                        SendMessage("AnimManager", mInputDir);
                        Game.Instance.Sound.PlaySFX("Se_UI_Slide");
                    }
                    break;
                default:
                    break;
            }
        }

        private void GetInputDirection()
        {
            // 手势识别
            mInputDir = InputDirection.Null;

            if (Input.GetMouseButtonDown(0))
            {
                mActiveInput = true;
                mMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) && mActiveInput)
            {
                Vector3 dir = Input.mousePosition - mMousePos;
                // 移动距离超过一定阈值
                if (dir.magnitude > 20f)
                {
                    // 水平方向
                    if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                    {
                        if (dir.x < 0) // 朝左
                            mInputDir = InputDirection.Left;
                        if (dir.x > 0) // 朝右
                            mInputDir = InputDirection.Right;
                    }
                    // 竖直方向
                    if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
                    {
                        if (dir.y > 0) // 朝上
                            mInputDir = InputDirection.Up;
                        if (dir.y < 0) // 朝下
                            mInputDir = InputDirection.Down;
                    }

                    mActiveInput = false;
                }
            }

            // 键盘识别
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
                mInputDir = InputDirection.Up;
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftControl))
                mInputDir = InputDirection.Down;
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                mInputDir = InputDirection.Left;
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                mInputDir = InputDirection.Right;
        }

        // 更新速度
        private void UpdateSpeed()
        {
            // 计算距离
            mSpeedAddCount += Speed * Time.deltaTime;
            if (mSpeedAddCount > mSpeedAddDistance)
            {
                mSpeedAddCount = 0;
                Speed += mSpeedAddRate;
                Debug.Log("当前速度：" + Speed);
            }
        }
        #endregion

        #region 减速
        // 撞击减速
        public void HitObstacle()
        {
            if (mIsHit) return;

            mMaskSpeed = Speed;
            mIsHit = true;
            Speed = 0;

            Game.Instance.Sound.PlaySFX("Se_UI_Hit"); // 声音

            StartCoroutine(SlowAccelerate());
        }

        // 缓慢加速
        private IEnumerator SlowAccelerate()
        {
            while (Speed < mMaskSpeed)
            {
                Speed += Time.deltaTime * mAddRate;
                yield return null;
            }

            mIsHit = false;
        }
        #endregion

        #region 吃道具
        // 金币
        public void HitCoin() // 由道具通过 SendMessage() 调用
        {
            mGameModel.Coin.Value += mDoubleCoin;
        }

        public void HitSkillItem(SkillType skillType) // 由道具通过 SendMessage() 调用
        {
            ItemArgs eItemArgs = new ItemArgs()
            {
                SkillCount = 0,
                SkillType = skillType,
            };

            SendEvent(Consts.E_HitItem, eItemArgs);
        }

        // 吸铁石
        public void HitMagnet()
        {
            // 确保该协程单一性
            if (mMagnetCor != null)
                StopCoroutine(mMagnetCor);

            mMagnetCor = MutiplyCoroutine();
            mMagnetCollider.enabled = true;
            StartCoroutine(mMagnetCor);
        }

        private IEnumerator mMagnetCoroutine()
        {
            float timer = mSkillTime;
            while (timer > 0)
            {
                if (mGameModel.GamePlaying())
                    timer -= Time.deltaTime;
                yield return null;
            }
            //yield return new WaitForSeconds(mSkillTime);
            mMagnetCollider.enabled = false;
            mMagnetCor = null;
        }

        // 双倍金币时间
        public void HitMultiply()
        {
            // 确保该协程单一性
            if (mMultiplyCor != null)
                StopCoroutine(mMultiplyCor);

            mMultiplyCor = MutiplyCoroutine();
            mDoubleCoin = 2;
            StartCoroutine(mMultiplyCor);
        }

        private IEnumerator MutiplyCoroutine()
        {
            float timer = mSkillTime;
            while (timer > 0)
            {
                if (mGameModel.GamePlaying())
                    timer -= Time.deltaTime;
                yield return null;
            }
            //yield return new WaitForSeconds(mSkillTime);
            mDoubleCoin = 1;
            mMultiplyCor = null;
        }

        // 无敌
        public void HitInvincible()
        {
            // 确保该协程单一性
            if (mInvincibleCor != null)
                StopCoroutine(mInvincibleCor);

            mInvincibleCor = InvincibleCoroutine();
            StartCoroutine(mInvincibleCor);
        }

        // 加时
        public void HitAddTime()
        {
            mGameModel.GameTime.Value += mGameModel.AddTime.Value;
        }

        private IEnumerator InvincibleCoroutine()
        {
            mIsInvincible = true;
            float timer = mSkillTime;
            while (timer > 0)
            {
                if (mGameModel.GamePlaying())
                    timer -= Time.deltaTime;
                yield return null;
            }
            //yield return new WaitForSeconds(mSkillTime);
            mIsInvincible = false;
            mInvincibleCor = null;
        }
        #endregion

        #region 射门相关
        public void OnGoalClick()
        {
            mTrail.Show();
            mBall.Hide();

            if (mGoalCor != null)
                StopCoroutine(mGoalCor);

            mGoalCor = MoveBall();
            StartCoroutine(mGoalCor);

            SendMessage("MessagePlayShoot");
        }

        private IEnumerator MoveBall()
        {
            while (true)
            {
                mTrail.transform.Translate(transform.forward * 36f * Time.deltaTime);
                yield return null;
            }
        }

        // 球进了
        public void HitBallDoor() // 由 BallDoor 的 Trigger 调用
        {
            // 停止协程
            StopCoroutine(mGoalCor);
            // 归位
            mTrail.LocalPosition(mTrail.transform.parent.localPosition);
            // IsGoal 变为 true
            mIsGoal = true;
            mTrail.Hide();
            mBall.Show();

            // 播放特效
            mFXGoal.Play();
            // 播放音效
            Game.Instance.Sound.PlaySFX("Se_UI_Goal");
            // 进球得分
            mGameModel.GoalCount.Value++;
        }
        #endregion
        #endregion

        #region 帮助方法
        #endregion
    }
}
