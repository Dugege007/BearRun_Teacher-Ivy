using System.Collections;
using UnityEngine;

namespace BearRun
{
    public class Effect : ReusableObject
    {
        public float RecycleTime = 1f;

        // 相当于 OnEnable()
        public override void OnAllocate()
        {
            StartCoroutine(RecycleCoroutine());
        }

        // 相当于 OnDisable()
        public override void OnRecycle()
        {
            // 关闭协程
            StopAllCoroutines();
        }

        private IEnumerator RecycleCoroutine()
        {
            // 等 1s
            yield return new WaitForSeconds(RecycleTime);
            // 回收
            Game.Instance.PoolManager.Recycle<Effect>(gameObject);
        }
    }
}
