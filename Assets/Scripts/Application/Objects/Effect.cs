using QFramework;
using System.Collections;
using UnityEngine;

namespace BearRun
{
    public class Effect : ReusableObject
    {
        public float RecycleTime = 1f;

        public override void OnAllocate()
        {
            // ��ʼЭ��֮ǰ�豣֤�ȼ���
            gameObject.Show();
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
            Game.Instance.PoolManager.Recycle<Effect>(gameObject);
        }
    }
}
