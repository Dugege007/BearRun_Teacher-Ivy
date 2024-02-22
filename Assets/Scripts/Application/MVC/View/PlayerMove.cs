using QFramework;
using System.Collections;
using UnityEngine;

namespace BearRun
{
    public class PlayerMove : View
    {
        #region ����
        private const float mMoveHorizontalSpeed = 12f;
        private const float mGravity = 9.8f;
        private const float mJumpHeight = 3f;

        private const float mSpeedAddDistance = 300f;
        private const float mSpeedAddRate = 0.5f;
        private const float mMaxSpeed = 40f;
        #endregion

        #region �¼�
        #endregion

        #region �ֶ�
        private GameModel mGameModel;
        private CharacterController mCC;
        private SphereCollider mMagnetCollider;

        // ǰ���ٶ�
        private float mSpeed = 10f;
        // ����
        private InputDirection mInputDir = InputDirection.Null;
        private bool mActiveInput = false;
        private Vector3 mMousePos;
        // X ��λ����Ϣ��0��ߣ�1�м䣻2�ұ�
        private int mNowIndex = 1;
        private int mTargetIndex = 1;
        private float mXDistance;
        // ��Ծ
        private float mYDistance;
        // ����
        private bool mIsSlide = false;
        private float mSlideTime = 0;
        // ����
        private float mSpeedAddCount;
        private float mMaskSpeed;
        private float mAddRate = 5f;
        // ײ��
        private bool mIsHit = false;

        // ���˲���
        //public Transform mPeopleGenTrans;

        // �Ե���
        private int mDoubleCoin = 1;
        private float mSkillTime;
        private IEnumerator mMultiplyCor;
        private IEnumerator mMagnetCor;
        private bool mIsInvincible = false;
        private IEnumerator mInvincibleCor;
        #endregion

        #region ����
        public override string Name => Consts.V_PlayerMove;
        public float Speed { get => mSpeed; set => mSpeed = Mathf.Min(value, mMaxSpeed); }
        #endregion

        #region Unity�ص�
        private void Awake()
        {
            mCC = GetComponent<CharacterController>();
            mGameModel = GetModel<GameModel>();
            mMagnetCollider = GetComponentInChildren<SphereCollider>();
            mMagnetCollider.enabled = false;

            mSkillTime = mGameModel.SkillTime;
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

            // ��ͣ
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                mGameModel.IsPause = !mGameModel.IsPause;
            }

            // ���˲���
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
                HitObstacle(); // ײ������
                Game.Instance.Sound.PlaySFX("Se_UI_Hit"); // ����
            }

            if (other.gameObject.CompareTag(Tags.BigFence))
            {
                if (mIsInvincible) return;

                if (mIsSlide) return;
                other.gameObject.SendMessage("HitPlayer", transform.position, SendMessageOptions.RequireReceiver);
                HitObstacle();
                Game.Instance.Sound.PlaySFX("Se_UI_Hit");
            }

            if (other.gameObject.CompareTag(Tags.Block))
            {
                // ��Ϸ����
                other.gameObject.SendMessage("HitPlayer", transform.position, SendMessageOptions.RequireReceiver);
                Game.Instance.Sound.PlaySFX("Se_UI_End");

                SendEvent(Consts.E_EndGame);
            }

            if (other.gameObject.CompareTag(Tags.BlockChild))
            {
                // ��Ϸ����
                other.transform.parent.parent.gameObject.SendMessage("HitPlayer", transform.position, SendMessageOptions.RequireReceiver);
                Game.Instance.Sound.PlaySFX("Se_UI_End");

                SendEvent(Consts.E_EndGame);
            }

            if (other.gameObject.CompareTag(Tags.BeforeTrigger)) // ����������
            {
                other.transform.parent.gameObject.SendMessage("HitTrigger", SendMessageOptions.RequireReceiver);

                // ��������
            }
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

        #region �ƶ�
        // �ƶ�
        private void MoveControl()
        {
            // �����ƶ�
            if (mTargetIndex != mNowIndex)
            {
                float newX = Mathf.Lerp(0, mXDistance, mMoveHorizontalSpeed * Time.deltaTime);
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

            // �����ƶ�
            if (mIsSlide)
            {
                mSlideTime -= Time.deltaTime;
                if (mSlideTime <= 0)
                    mIsSlide = false;
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
                        Game.Instance.Sound.PlaySFX("Se_UI_HuaDong");
                    }
                    break;
                case InputDirection.Right:
                    if (mTargetIndex < 2)
                    {
                        mTargetIndex++;
                        mXDistance = 2f;
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

        // �����ٶ�
        private void UpdateSpeed()
        {
            // �������
            mSpeedAddCount += Speed * Time.deltaTime;
            if (mSpeedAddCount > mSpeedAddDistance)
            {
                mSpeedAddCount = 0;
                Speed += mSpeedAddRate;
                Debug.Log("��ǰ�ٶȣ�" + Speed);
            }
        }
        #endregion

        #region ����
        // ײ������
        public void HitObstacle()
        {
            if (mIsHit) return;

            mMaskSpeed = Speed;
            mIsHit = true;
            Speed = 0;
            StartCoroutine(SlowAccelerate());
        }

        // ��������
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

        #region �Ե���
        // ���
        public void HitCoin()
        {
            Debug.Log("��ǰ��ң�");
        }

        // ˫�����ʱ��
        public void HitMutiply()
        {
            // ȷ����Э�̵�һ��
            if (mMultiplyCor != null)
                StopCoroutine(mMultiplyCor);

            mMultiplyCor = MutiplyCoroutine();
            mDoubleCoin = 2;
            StartCoroutine(mMultiplyCor);
        }

        private IEnumerator MutiplyCoroutine()
        {
            yield return new WaitForSeconds(mSkillTime);
            mDoubleCoin = 1;
            mMultiplyCor = null;
        }

        // ����ʯ
        private void HitMagnet()
        {
            // ȷ����Э�̵�һ��
            if (mMagnetCor != null)
                StopCoroutine(mMagnetCor);

            mMagnetCor = MutiplyCoroutine();
            mMagnetCollider.enabled = true;
            StartCoroutine(mMagnetCor);
        }

        private IEnumerator mMagnetCoroutine()
        {
            yield return new WaitForSeconds(mSkillTime);
            mMagnetCollider.enabled = false;
            mMagnetCor = null;
        }

        // ��ʱ
        private void HitAddTime()
        {
            //TODO ��ʱ
            Debug.Log("��ʱ");
        }

        // �޵�
        private void HitInvincible()
        {
            // ȷ����Э�̵�һ��
            if (mInvincibleCor != null)
                StopCoroutine(mInvincibleCor);

            mInvincibleCor = InvincibleCoroutine();
            StartCoroutine(mInvincibleCor);
        }

        private IEnumerator InvincibleCoroutine()
        {
            mIsInvincible = true;
            yield return new WaitForSeconds(mSkillTime);
            mIsInvincible = false;
            mInvincibleCor = null;
        }
        #endregion
        #endregion

        #region ��������
        #endregion
    }
}
