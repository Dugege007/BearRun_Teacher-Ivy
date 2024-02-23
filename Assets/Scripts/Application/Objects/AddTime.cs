using UnityEngine;
using QFramework;

namespace BearRun
{
    public class AddTime : Item
    {
        public override void OnAllocate()
        {
            base.OnAllocate();
        }

        public override void OnRecycle()
        {
            base.OnRecycle();
        }

        public override void HitPlayer(Vector3 pos)
        {
            base.HitPlayer(pos);
            // Ãÿ–ß
            Game.Instance.PoolManager.Allocate<Effect>("FX_JiaShi")
                .Position(new Vector3(0, pos.y, pos.z))
                .Parent(mEffectParentTrans);

            // …˘“Ù
            Game.Instance.Sound.PlaySFX("Se_UI_Time");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Player))
            {
                HitPlayer(other.transform.position);
                other.SendMessage("HitAddTime", SendMessageOptions.RequireReceiver);
            }
        }
    }
}
