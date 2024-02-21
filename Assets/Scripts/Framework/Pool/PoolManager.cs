using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        // 资源目录
        public string ResourceDir = "";

        // 管理所有的池子
        private Dictionary<string, ObjectPool> mPoolsDir = new Dictionary<string, ObjectPool>();

        /// <summary>
        /// 取物体
        /// </summary>
        /// <param name="name">物体名称</param>
        /// <param name="parentTrans">父物体位置信息</param>
        /// <returns></returns>
        public GameObject Allocate(string name)
        {
            ObjectPool pool = null;

            if (mPoolsDir.ContainsKey(name) == false)
                RegisterNewPool(name);

            pool = mPoolsDir[name];
            return pool.Allocate();
        }

        /// <summary>
        /// 回收物体
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

            pool?.Recycle(obj);
        }

        /// <summary>
        /// 回收所有物体
        /// </summary>
        public void Clear()
        {
            foreach (ObjectPool p in mPoolsDir.Values)
                p.Clear();
        }

        // 新建一个池子
        private void RegisterNewPool(string prefabName)
        {
            // 资源目录
            string path = ResourceDir + "/" + prefabName;
            // 加载预制体资源
            GameObject obj = Resources.Load<GameObject>(path);
            // 新建一个池子
            ObjectPool pool = new ObjectPool(obj);
            mPoolsDir.Add(pool.Name, pool);
        }
    }
}
