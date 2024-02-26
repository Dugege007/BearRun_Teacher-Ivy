
using UnityEngine;

namespace BearRun
{
    public class Road : ReusableObject
    {
        public override void OnAllocate()
        {
            
        }

        public override void OnRecycle()
        {
            // 回收 ItemHolder 下的物体
            Transform itemChild = transform.Find("ItemHolder");
            if (itemChild != null)
            {
                foreach (Transform item in itemChild)
                    if (item != null)
                        Game.Instance.PoolManager.Recycle(item.gameObject);
            }
        }
    }
}
