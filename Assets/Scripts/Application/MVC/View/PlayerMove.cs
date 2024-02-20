using GraphProcessor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class PlayerMove : View
    {
        #region 常量
        #endregion

        #region 事件
        #endregion

        #region 字段
        public float Speed = 10f;
        private CharacterController mCC;
        private InputDirection mInputDir = InputDirection.Null;
        private bool mActiveInput = false;
        private Vector3 mMousePos;
        #endregion

        #region 属性
        public override string Name => Consts.V_PlayerMove;
        #endregion

        #region Unity回调
        private void Awake()
        {
            mCC = GetComponent<CharacterController>();
        }

        private void Start()
        {
            StartCoroutine(UpdateAction());
        }

        private void Update()
        {
        }
        #endregion

        #region 事件回调
        public override void HandleEvent(string eventName, object data)
        {
        }
        #endregion

        #region 方法
        private IEnumerator UpdateAction()
        {
            while (true)
            {
                mCC.Move(transform.forward * Speed * Time.deltaTime);
                GetInputDirection();

                yield return 0;
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
                        if (dir.x > 0) // 朝右
                            mInputDir = InputDirection.Right;
                        if (dir.x < 0) // 朝左
                            mInputDir = InputDirection.Left;
                    }
                    // 竖直方向
                    if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
                    {
                        if (dir.y > 0) // 朝上
                            mInputDir = InputDirection.Up;
                        if (dir.y < 0) // 朝下
                            mInputDir = InputDirection.Down;
                    }
                }

                Debug.Log(mInputDir);
            }

            if (Input.GetMouseButtonUp(0))
                mActiveInput = false;

            // 键盘识别
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
                mInputDir = InputDirection.Up;
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftControl))
                mInputDir = InputDirection.Down;
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                mInputDir = InputDirection.Left;
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.RightArrow))
                mInputDir = InputDirection.Right;
        }
        #endregion

        #region 帮助方法
        #endregion




    }
}
