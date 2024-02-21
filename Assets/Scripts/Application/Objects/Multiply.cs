using UnityEngine;
using QFramework;

namespace BearRun
{
    public class Multiply : Item
    {
        private Transform mEffectParentTrans;

        private void Awake()
        {
            mEffectParentTrans = GameObject.Find("Effects").transform;
        }

        public override void OnAllocate()
        {
            base.OnAllocate();
        }

        public override void OnRecycle()
        {
            base.OnRecycle();
        }

        public override void HitTrigger(Vector3 pos)
        {
            base.HitTrigger(pos);
            // 特效
            Game.Instance.PoolManager.Allocate<Effect>("FX_JinBi")
                .Position(pos + Vector3.up * 0.5f)
                .Parent(mEffectParentTrans);

            // 声音
            Game.Instance.Sound.PlaySFX("Se_UI_Stars");

            //TODO 回收
            //Game.Instance.ObjectPool.Recycle(gameObject);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Player))
            {
                HitTrigger(other.transform.position);
                other.SendMessage("HitMutiply", SendMessageOptions.RequireReceiver);
            }
        }
    }
}
