using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
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

            mRoadNow = Game.Instance.ObjectPool.Allocate("Pattern_1", mParent.transform);
            mRoadNext = Game.Instance.ObjectPool.Allocate("Pattern_2", mParent.transform);
            mRoadNext.transform.position += new Vector3(0, 0, 160f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Road))
            {
                // ����
                Game.Instance.ObjectPool.Recycle(other.gameObject);

                // �����µ��ܵ�
                AllocateNewRoad();
            }
        }

        private void AllocateNewRoad()
        {
            int randomIndex = Random.Range(1, 5);

            // ���¶���
            mRoadNow = mRoadNext;
            mRoadNext = Game.Instance.ObjectPool.Allocate("Pattern_" + randomIndex, mParent.transform);
            mRoadNext.transform.position = mRoadNow.transform.position + new Vector3(0, 0, 160f);
            // �����µ���Ϸ����

        }
    }
}
