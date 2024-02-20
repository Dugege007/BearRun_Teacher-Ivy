using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        // ��ԴĿ¼
        public string ResourceDir = "";

        // �������еĳ���
        private Dictionary<string, SubPool> mPoolsDir = new Dictionary<string, SubPool>();

        /// <summary>
        /// ȡ����
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="parentTrans">������λ����Ϣ</param>
        /// <returns></returns>
        public GameObject Allocate(string name, Transform parentTrans)
        {
            SubPool pool = null;

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
                pool.Recycle(obj);
        }

        /// <summary>
        /// ������������
        /// </summary>
        public void Clear()
        {
            foreach (SubPool p in mPoolsDir.Values)
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
            SubPool pool = new SubPool(parentTrans, obj);
            mPoolsDir.Add(pool.Name, pool);
        }
    }
}
