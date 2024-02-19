using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class ObjectPool : MonoBehaviour
    {
        // 资源目录
        public string ResourceDir = "";

        // 管理所有的池子
        private Dictionary<string, SubPool> mPoolsDir = new Dictionary<string, SubPool>();

        // 取物体
        public GameObject Spawn(string name, Transform parentTrans)
        {
            SubPool pool = null;

            if (mPoolsDir.ContainsKey(name) == false)
                RegisterNewPool(name, parentTrans);

            pool = mPoolsDir[name];
            return pool.Spawn();
        }

        // 回收物体
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

        // 回收所有物体
        public void UnSpawnAll()
        {
            foreach (SubPool p in mPoolsDir.Values)
                p.UnSpawnAll();
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
