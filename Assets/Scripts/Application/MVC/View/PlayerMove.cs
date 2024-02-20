using System.Collections;
using System.Collections.Generic;
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
            StartCoroutine(UpdateAction());
        }

        private void Update()
        {
        }
        #endregion

        #region �¼��ص�
        public override void HandleEvent(string eventName, object data)
        {
        }
        #endregion

        #region ����
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
                        if (dir.x > 0) // ����
                            mInputDir = InputDirection.Right;
                        if (dir.x < 0) // ����
                            mInputDir = InputDirection.Left;
                    }
                    // ��ֱ����
                    if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
                    {
                        if (dir.y > 0) // ����
                            mInputDir = InputDirection.Up;
                        if (dir.y < 0) // ����
                            mInputDir = InputDirection.Down;
                    }
                }

                Debug.Log(mInputDir);
            }
        }
        #endregion

        #region ��������
        #endregion




    }
}
