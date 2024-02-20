using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace BearRun
{
    public class RoadChange : MonoBehaviour
    {
        public GameObject mRoadNow;
        public GameObject mRoadNext;
        private GameObject mParent;

        private void Start()
        {
            if (mParent == null)
            {
                mParent = new GameObject();
                mParent.transform.position = Vector3.zero;
                mParent.name = Tags.Road;
            }

            mRoadNow = Game.Instance.ObjectPool.Allocate("Pattern_1", mParent.transform);
            mRoadNext = Game.Instance.ObjectPool.Allocate("Pattern_2", mParent.transform);
            mRoadNext.transform.position += new Vector3(0, 0, 160f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Road))
            {
                // 回收
                Game.Instance.ObjectPool.Recycle(other.gameObject);

                // 创建新的跑道
                AllocateNewRoad();
            }
        }

        private void AllocateNewRoad()
        {
            // 更新对象
            mRoadNow = mRoadNext;
            mRoadNext = Game.Instance.ObjectPool.Allocate("Pattern_2", mParent.transform);
            mRoadNext.transform.position = mRoadNow.transform.position + new Vector3(0, 0, 160f);
            // 生成新的游戏对象

        }
    }
}
