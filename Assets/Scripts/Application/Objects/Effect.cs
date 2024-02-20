using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class Effect : ReusableObject
    {
        public float RecycleTime = 1f;

        // �൱�� OnEnable()
        public override void OnAllocate()
        {
            StartCoroutine(RecycleCoroutine());
        }

        // �൱�� OnDisable()
        public override void OnRecycle()
        {
            // �ر�Э��
            StopAllCoroutines();
        }

        private IEnumerator RecycleCoroutine()
        {
            // �� 1s
            yield return new WaitForSeconds(RecycleTime);
            // ����
            Game.Instance.ObjectPool.Recycle(gameObject);
        }
    }
}
