using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    // ReusableObject 需要实现 OnAllocate 和 OnRecycle
    // 比 ObjectPool 有更多功能
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
            // 生成特效
            //GameObject effectObj = Game.Instance.ObjectPool.Allocate("FX_ZhuangJi", mParentTrans);
            //effectObj.transform.position = pos;
            Game.Instance.ObjectPool.Allocate("FX_ZhuangJi", mParentTrans)
                .Position(pos + Vector3.up * 0.5f)
                .Show();

            // 声音
            Game.Instance.Sound.PlaySFX("Se_UI_Hit");
            // 回收
            //Game.Instance.ObjectPool.Recycle(gameObject);
            Destroy(gameObject);
        }
    }
}
