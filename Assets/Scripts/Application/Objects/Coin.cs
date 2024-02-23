using QFramework;
using System.Collections;
using UnityEngine;

namespace BearRun
{
    public class Coin : Item
    {
        private float mFlySpeed = 10f;
        private bool mIsLoop = false;
        private GameObject mPlayer;

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
                mPlayer = other.gameObject;
                // 飞向玩家
                StartCoroutine(FlyToPlayer());
            }
        }

        private IEnumerator FlyToPlayer()
        {
            mIsLoop = true;
            while (mIsLoop)
            {
                transform.position = Vector3.Lerp(transform.position, mPlayer.transform.position, mFlySpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
