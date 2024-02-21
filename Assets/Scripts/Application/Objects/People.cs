using QFramework;
using UnityEngine;

namespace BearRun
{
    public class People : Obstacles
    {
        public float Speed = 5f;
        public bool IsFly = false;
        //private float mFlyForce = 100f;
        private bool mIsBlock = false;
        //private Rigidbody mRigidbody;

        private Animation mAnim;

        protected override void Awake()
        {
            base.Awake();
            //mRigidbody = GetComponent<Rigidbody>();
            mAnim = GetComponentInChildren<Animation>();
        }

        private void Update()
        {
            if (mIsBlock)
                transform.position -= new Vector3(Speed, 0, 0) * Time.deltaTime;

            if (IsFly)
                transform.position += 2f * Time.deltaTime * new Vector3(0, Speed, Speed);
        }

        public override void OnAllocate()
        {
            base.OnAllocate();
            //StartCoroutine(RecycleDelay());

            mAnim.Play("run");
        }

        public override void OnRecycle()
        {
            base.OnRecycle();

            // 重置数据
            mAnim.transform.localPosition = Vector3.zero;
            mIsBlock = false;
            IsFly = false;
        }

        public override void HitPlayer(Vector3 pos)
        {
            // 生成特效
            Game.Instance.PoolManager.Allocate<Effect>("FX_ZhuangJi")
                .Position(pos + Vector3.up * 0.5f)
                .Parent(mEffectParentTrans);

            // 动画
            mAnim.Play("fly");

            mIsBlock = false;
            IsFly = true;
        }

        // 碰撞到触发区域
        public void HitTrigger()
        {
            mIsBlock = true;
        }

        //private IEnumerator RecycleDelay()
        //{
        //    yield return new WaitForSeconds(3f);
        //    Game.Instance.ObjectPool.Recycle(gameObject);
        //}
    }
}
