using QFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BearRun
{
    public class PlayerMove : View
    {
        #region 常量
        private const float mMoveHorizontalSpeed = 12f;
        private const float mGravity = 9.8f;
        private const float mJumpHeight = 5f;

        private const float mSpeedAddDistance = 300f;
        private const float mSpeedAddRate = 0.5f;
        private const float mMaxSpeed = 40f;
        #endregion

        #region 事件
        #endregion

        #region 字段
        private float mSpeed = 10f;

        private CharacterController mCC;
        private InputDirection mInputDir = InputDirection.Null;
        private bool mActiveInput = false;
        private Vector3 mMousePos;

        // X 轴位置信息：0左边；1中间；2右边
        private int mNowIndex = 1;
        private int mTargetIndex = 1;
        private float mXDistance;
        private float mYDistance;

        private bool mIsSlide = false;
        private float mSlideTime = 0;

        private float mSpeedAddCount;

        private GameModel mGameModel;
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
        }

        private void Start()
        {
            //StartCoroutine(UpdateAction());
        }

        private void Update()
        {
            if (mGameModel.IsPause == false && mGameModel.IsPlaying)
            {
                mYDistance -= mGravity * Time.deltaTime;
                mCC.Move(Speed * Time.deltaTime * (transform.forward + new Vector3(0, mYDistance, 0)));
                MoveControl();
                UpdatePosition();
                UpdateSpeed();
            }
        }
        #endregion

        #region 事件回调
        public override void HandleEvent(string eventName, object data)
        {
        }
        #endregion

        #region 方法
        // 用 Update() 代替
        //private IEnumerator UpdateAction()
        //{
        //    while (true)
        //    {
        //        mCC.Move(Speed * Time.deltaTime * transform.forward);
        //        MoveControl();
        //        UpdatePosition();
        //        yield return null;
        //    }
        //}

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
                            transform.position = new Vector3(-2, transform.position.y, transform.position.z);
                            Debug.Log("移动到 左");
                            break;
                        case 1:
                            transform.position = new Vector3(0, transform.position.y, transform.position.z);
                            Debug.Log("移动到 中");
                            break;
                        case 2:
                            transform.position = new Vector3(2, transform.position.y, transform.position.z);
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
                        mXDistance = -2f;
                        SendMessage("AnimManager", mInputDir);
                    }
                    break;
                case InputDirection.Right:
                    if (mTargetIndex < 2)
                    {
                        mTargetIndex++;
                        mXDistance = 2f;
                        SendMessage("AnimManager", mInputDir);
                    }
                    break;
                case InputDirection.Up:
                    if (mCC.isGrounded)
                    {
                        mYDistance = mJumpHeight;
                        SendMessage("AnimManager", mInputDir);
                    }
                    break;
                case InputDirection.Down:
                    if (mIsSlide == false)
                    {
                        mIsSlide = true;
                        mSlideTime = 0.733f;
                        SendMessage("AnimManager", mInputDir);
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

        #region 帮助方法
        #endregion
    }
}
