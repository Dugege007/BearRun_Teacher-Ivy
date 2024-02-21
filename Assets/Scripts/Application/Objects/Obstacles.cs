using QFramework;
using UnityEngine;

namespace BearRun
{
    // ReusableObject ��Ҫʵ�� OnAllocate �� OnRecycle
    // �� ObjectPool �и��๦��
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
            // �ر�Э��
            StopAllCoroutines();
        }

        public virtual void HitPlayer(Vector3 pos)
        {
            // ������Ч
            //GameObject effectObj = Game.Instance.ObjectPool.Allocate("FX_ZhuangJi", mParentTrans);
            //effectObj.transform.position = pos;
            Game.Instance.PoolManager.Allocate("FX_ZhuangJi")
                .Parent(mEffectParentTrans)
                .Position(pos + Vector3.up * 0.5f)
                .Show();

            // ����
            //Game.Instance.ObjectPool.Recycle(gameObject);
            Destroy(gameObject);
        }
    }
}
