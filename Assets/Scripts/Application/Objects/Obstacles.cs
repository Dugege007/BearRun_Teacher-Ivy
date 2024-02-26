using QFramework;
using UnityEngine;

namespace BearRun
{
    // ReusableObject 需要实现 OnAllocate 和 OnRecycle
    public class Obstacles : ReusableObject
    {
        protected Transform mEffectParentTrans;

        protected virtual void Awake()
        {
            mEffectParentTrans = GameObject.Find("Effects").transform;
        }

        public override void OnAllocate()
        {

        }

        public override void OnRecycle()
        {

        }

        public virtual void HitPlayer(Vector3 pos)
        {
            // 生成特效
            //GameObject effectObj = Game.Instance.ObjectPool.Allocate("FX_ZhuangJi", mParentTrans);
            //effectObj.transform.position = pos;
            Game.Instance.PoolManager.Allocate<Effect>("FX_ZhuangJi")
                .Parent(mEffectParentTrans)
                .Position(pos + Vector3.up * 0.5f);

            // 回收
            Game.Instance.PoolManager.Recycle(gameObject);
        }
    }
}
