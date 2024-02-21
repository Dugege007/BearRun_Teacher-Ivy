using QFramework;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace BearRun
{
    public class Coin : Item
    {
        private float mFlySpeed = 20f;
        private bool mIsLoop = false;

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
            // 特效
            Game.Instance.PoolManager.Allocate<Effect>("FX_JinBi")
                .Position(pos + Vector3.up * 0.5f)
                .Parent(mEffectParentTrans);

            // 声音
            Game.Instance.Sound.PlaySFX("Se_UI_JinBi");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Player))
            {
                HitPlayer(other.transform.position);
                other.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);
                mIsLoop = false;
            }
            if (other.gameObject.CompareTag(Tags.MagnetCollider))
            {
                // 飞向玩家
                StartCoroutine(FlyToPlayer(other.transform.position));
            }
        }

        private IEnumerator FlyToPlayer(Vector3 playerPos)
        {
            mIsLoop = true;
            while (mIsLoop)
            {
                transform.position = Vector3.Lerp(transform.position, playerPos, mFlySpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
