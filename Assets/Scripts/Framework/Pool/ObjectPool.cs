using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class ObjectPool : MonoBehaviour
    {
        // ��ԴĿ¼
        public string ResourceDir = "";

        // �������еĳ���
        private Dictionary<string, SubPool> mPoolsDir = new Dictionary<string, SubPool>();

        // ȡ����
        public GameObject Spawn(string name, Transform parentTrans)
        {
            SubPool pool = null;

            if (mPoolsDir.ContainsKey(name) == false)
                RegisterNewPool(name, parentTrans);

            pool = mPoolsDir[name];
            return pool.Spawn();
        }

        // ��������
        public void UnSpawn(GameObject obj)
        {
            SubPool pool = null;

            foreach (SubPool p in mPoolsDir.Values)
            {
                if (p.Contain(obj))
                {
                    pool = p;
                    break;
                }
            }

            if (pool != null)
                pool.UnSpawn(obj);
        }

        // ������������
        public void UnSpawnAll()
        {
            foreach (SubPool p in mPoolsDir.Values)
                p.UnSpawnAll();
        }

        // �½�һ������
        private void RegisterNewPool(string prefabName, Transform parentTrans)
        {
            // ��ԴĿ¼
            string path = ResourceDir + "/" + prefabName;
            // ����Ԥ������Դ
            GameObject obj = Resources.Load<GameObject>(path);
            // �½�һ������
            SubPool pool = new SubPool(parentTrans, obj);
            mPoolsDir.Add(pool.Name, pool);
        }
    }
}
