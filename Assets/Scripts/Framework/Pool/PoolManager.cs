using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        // ��ԴĿ¼
        public string ResourceDir = "";

        // �������еĳ���
        private Dictionary<string, ObjectPool> mPoolsDir = new Dictionary<string, ObjectPool>();

        /// <summary>
        /// ȡ����
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="parentTrans">������λ����Ϣ</param>
        /// <returns></returns>
        public GameObject Allocate(string name, Transform parentTrans)
        {
            ObjectPool pool = null;

            if (mPoolsDir.ContainsKey(name) == false)
                RegisterNewPool(name, parentTrans);

            pool = mPoolsDir[name];
            return pool.Allocate();
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="obj"></param>
        public void Recycle(GameObject obj)
        {
            ObjectPool pool = null;

            foreach (ObjectPool p in mPoolsDir.Values)
            {
                if (p.Contain(obj))
                {
                    pool = p;
                    break;
                }
            }

            if (pool != null)
                pool.Recycle(obj);
        }

        /// <summary>
        /// ������������
        /// </summary>
        public void Clear()
        {
            foreach (ObjectPool p in mPoolsDir.Values)
                p.Clear();
        }

        // �½�һ������
        private void RegisterNewPool(string prefabName, Transform parentTrans)
        {
            // ��ԴĿ¼
            string path = ResourceDir + "/" + prefabName;
            // ����Ԥ������Դ
            GameObject obj = Resources.Load<GameObject>(path);
            // �½�һ������
            ObjectPool pool = new ObjectPool(parentTrans, obj);
            mPoolsDir.Add(pool.Name, pool);
        }
    }
}
