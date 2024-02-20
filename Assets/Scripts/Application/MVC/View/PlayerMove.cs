using QFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BearRun
{
    public class PlayerMove : View
    {
        #region ����
        #endregion

        #region �¼�
        #endregion

        #region �ֶ�
        public float Speed = 10f;

        private CharacterController mCC;
        private InputDirection mInputDir = InputDirection.Null;
        private bool mActiveInput = false;
        private Vector3 mMousePos;

        // X ��λ����Ϣ��0��ߣ�1�м䣻2�ұ�
        private int mNowIndex = 1;
        private int mTargetIndex = 1;
        private float mXDistance;
        private float mMoveSpeed = 12f;

        private float mYDistance;
        private float gravity = 9.8f;
        private float mJumpHeight = 5f;
        #endregion

        #region ����
        public override string Name => Consts.V_PlayerMove;
        #endregion

        #region Unity�ص�
        private void Awake()
        {
            mCC = GetComponent<CharacterController>();
        }

        private void Start()
        {
            //StartCoroutine(UpdateAction());
        }

        private void Update()
        {
            mYDistance -= gravity * Time.deltaTime;
            mCC.Move(Speed * Time.deltaTime * (transform.forward + new Vector3(0, mYDistance, 0)));
            MoveControl();
            UpdatePosition();
        }
        #endregion

        #region �¼��ص�
        public override void HandleEvent(string eventName, object data)
        {
        }
        #endregion

        #region ����
        // �� Update() ����
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

        // �ƶ�
        private void MoveControl()
        {
            if (mTargetIndex != mNowIndex)
            {
                float newX = Mathf.Lerp(0, mXDistance, mMoveSpeed * Time.deltaTime);
                // ���㱾֡Ӧ���ƶ��ľ���
                Vector3 move = new Vector3(newX, 0, 0);

                // ʹ�� CharacterController �ƶ���ɫ
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
                            Debug.Log("�ƶ��� ��");
                            break;
                        case 1:
                            transform.position = new Vector3(0, transform.position.y, transform.position.z);
                            Debug.Log("�ƶ��� ��");
                            break;
                        case 2:
                            transform.position = new Vector3(2, transform.position.y, transform.position.z);
                            Debug.Log("�ƶ��� ��");
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        // ����λ��
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
                    break;
                default:
                    break;
            }
        }

        private void GetInputDirection()
        {
            // ����ʶ��
            mInputDir = InputDirection.Null;

            if (Input.GetMouseButtonDown(0))
            {
                mActiveInput = true;
                mMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) && mActiveInput)
            {
                Vector3 dir = Input.mousePosition - mMousePos;
                // �ƶ����볬��һ����ֵ
                if (dir.magnitude > 20f)
                {
                    // ˮƽ����
                    if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                    {
                        if (dir.x < 0) // ����
                            mInputDir = InputDirection.Left;
                        if (dir.x > 0) // ����
                            mInputDir = InputDirection.Right;
                    }
                    // ��ֱ����
                    if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
                    {
                        if (dir.y > 0) // ����
                            mInputDir = InputDirection.Up;
                        if (dir.y < 0) // ����
                            mInputDir = InputDirection.Down;
                    }

                    mActiveInput = false;
                }
            }

            // ����ʶ��
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
                mInputDir = InputDirection.Up;
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftControl))
                mInputDir = InputDirection.Down;
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                mInputDir = InputDirection.Left;
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                mInputDir = InputDirection.Right;
        }
        #endregion

        #region ��������
        #endregion
    }
}
