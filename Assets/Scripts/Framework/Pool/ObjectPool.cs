using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        // 资源目录
        public string ResourceDir = "";

        // 管理所有的池子
        private Dictionary<string, SubPool> mPoolsDir = new Dictionary<string, SubPool>();

        /// <summary>
        /// 取物体
        /// </summary>
        /// <param name="name">物体名称</param>
        /// <param name="parentTrans">父物体位置信息</param>
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
        /// 回收物体
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
        /// 回收所有物体
        /// </summary>
        public void Clear()
        {
            foreach (SubPool p in mPoolsDir.Values)
                p.Clear();
        }

        // 新建一个池子
        private void RegisterNewPool(string prefabName, Transform parentTrans)
        {
            // 资源目录
            string path = ResourceDir + "/" + prefabName;
            // 加载预制体资源
            GameObject obj = Resources.Load<GameObject>(path);
            // 新建一个池子
            SubPool pool = new SubPool(parentTrans, obj);
            mPoolsDir.Add(pool.Name, pool);
        }
    }
}
