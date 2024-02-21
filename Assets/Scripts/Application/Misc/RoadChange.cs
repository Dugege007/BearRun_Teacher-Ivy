using QFramework;
using UnityEngine;

namespace BearRun
{
    public class RoadChange : MonoBehaviour
    {
        private GameObject mRoadNow;
        private GameObject mRoadNext;
        private GameObject mParent;

        private void Start()
        {
            if (mParent == null)
            {
                mParent = new GameObject();
                mParent.transform.position = Vector3.zero;
                mParent.name = Tags.Road;
            }

            mRoadNow = Game.Instance.PoolManager.Allocate("Pattern_1")
                .Parent(mParent.transform);
            mRoadNext = Game.Instance.PoolManager.Allocate("Pattern_2")
                .Parent(mParent.transform);

            mRoadNext.transform.position += new Vector3(0, 0, 160f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Road))
            {
                // 回收
                Game.Instance.PoolManager.Recycle(other.gameObject);

                // 创建新的跑道
                AllocateNewRoad();
            }
        }

        private void AllocateNewRoad()
        {
            int randomIndex = Random.Range(1, 5);

            // 更新对象
            mRoadNow = mRoadNext;
            mRoadNext = Game.Instance.PoolManager.Allocate("Pattern_" + randomIndex)
                .Parent(mParent.transform);
            mRoadNext.transform.position = mRoadNow.transform.position + new Vector3(0, 0, 160f);
            // 生成新的游戏对象

        }
    }
}
