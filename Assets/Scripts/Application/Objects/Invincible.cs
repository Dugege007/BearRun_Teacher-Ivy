using QFramework;
using UnityEngine;

namespace BearRun
{
    public class Invincible : Item
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
            Game.Instance.PoolManager.Allocate<Effect>("FX_JinBi")
                .Position(pos + Vector3.up * 0.5f)
                .Parent(mEffectParentTrans);

            // …˘“Ù
            Game.Instance.Sound.PlaySFX("Se_UI_Whist");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Player))
            {
                HitPlayer(other.transform.position);
                other.SendMessage("HitInvincible", SendMessageOptions.RequireReceiver);
            }
        }
    }
}
