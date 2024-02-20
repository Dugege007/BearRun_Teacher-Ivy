using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    // ReusableObject ��Ҫʵ�� OnAllocate �� OnRecycle
    // �� ObjectPool �и��๦��
    public class Obstacles : ReusableObject
    {
        private Transform mParentTrans;

        private void Start()
        {
            mParentTrans = GameObject.Find("Effects").transform;
        }

        public override void OnAllocate()
        {

        }

        public override void OnRecycle()
        {

        }

        public void HitPlayer(Vector3 pos)
        {
            // ������Ч
            //GameObject effectObj = Game.Instance.ObjectPool.Allocate("FX_ZhuangJi", mParentTrans);
            //effectObj.transform.position = pos;
            Game.Instance.ObjectPool.Allocate("FX_ZhuangJi", mParentTrans)
                .Position(pos + Vector3.up * 0.5f)
                .Show();

            // ����
            Game.Instance.Sound.PlaySFX("Se_UI_Hit");
            // ����
            //Game.Instance.ObjectPool.Recycle(gameObject);
            Destroy(gameObject);
        }
    }
}
