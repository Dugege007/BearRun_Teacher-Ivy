using UnityEngine;
using QFramework;

namespace BearRun
{
    public class Multiply : Item
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
            Game.Instance.Sound.PlaySFX("Se_UI_Stars");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Player))
            {
                HitPlayer(other.transform.position);
                //other.SendMessage("HitMutiply", SendMessageOptions.RequireReceiver);
                other.SendMessage("HitSkillItem", SkillType.Multiply, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
