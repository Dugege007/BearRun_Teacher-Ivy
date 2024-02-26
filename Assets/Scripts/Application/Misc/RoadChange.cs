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

            AddItem(mRoadNow);
            AddItem(mRoadNext);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Road))
            {
                // ����
                Game.Instance.PoolManager.Recycle(other.gameObject);

                // �����µ��ܵ�
                AllocateNewRoad();
            }
        }

        private void AllocateNewRoad()
        {
            int randomIndex = Random.Range(1, 5);

            // ���¶���
            mRoadNow = mRoadNext;
            mRoadNext = Game.Instance.PoolManager.Allocate("Pattern_" + randomIndex)
                .Parent(mParent.transform)
                .Position(mRoadNow.transform.position + new Vector3(0, 0, 160f));

            // �����µ���Ϸ����
            AddItem(mRoadNext);
        }

        // �����ϰ���
        public void AddItem(GameObject obj)
        {
            Transform itemChild = obj.transform.Find("ItemHolder");
            if (itemChild != null)
            {
                var patternManager = PatternManager.Instance;
                if (patternManager != null
                    && patternManager.PatternPrograms != null
                    && patternManager.PatternPrograms.Count > 0)
                {
                    var pattern = patternManager.PatternPrograms[Random.Range(0, patternManager.PatternPrograms.Count)];
                    if (pattern != null 
                        && pattern.PatternItems != null 
                        && pattern.PatternItems.Count > 0)
                    {
                        foreach ( var item in pattern.PatternItems )
                        {
                            Game.Instance.PoolManager.Allocate<ReusableObject>(item.PrefabName)
                                .Parent(itemChild)
                                .LocalPosition(item.Position)
                                .Show();
                        }
                    }
                }
            }
        }
    }
}
