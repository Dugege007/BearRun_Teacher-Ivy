using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        // 资源目录
        public string ResourceDir = "";

        #region BearRun
        //// 管理所有的池子
        //private Dictionary<string, ObjectPool> mObjectPoolsDir = new Dictionary<string, ObjectPool>();

        ///// <summary>
        ///// 取物体
        ///// </summary>
        ///// <param name="name">物体名称</param>
        ///// <returns></returns>
        //public GameObject Allocate(string name)
        //{
        //    ObjectPool pool = null;

        //    if (mObjectPoolsDir.ContainsKey(name) == false)
        //        RegisterNewPool(name);

        //    pool = mObjectPoolsDir[name];
        //    return pool.Allocate();
        //}

        ///// <summary>
        ///// 回收物体
        ///// </summary>
        ///// <param name="obj"></param>
        //public void Recycle(GameObject obj)
        //{
        //    ObjectPool pool = null;

        //    foreach (ObjectPool p in mObjectPoolsDir.Values)
        //    {
        //        if (p.Contain(obj))
        //        {
        //            pool = p;
        //            break;
        //        }
        //    }

        //    pool?.Recycle(obj);
        //}

        ///// <summary>
        ///// 回收所有物体
        ///// </summary>
        //public void RecycleAll()
        //{
        //    foreach (ObjectPool p in mObjectPoolsDir.Values)
        //        p.RecycleAll();
        //}

        //// 新建一个池子
        //private void RegisterNewPool(string prefabName)
        //{
        //    // 资源目录
        //    string path = ResourceDir + "/" + prefabName;
        //    // 加载预制体资源
        //    GameObject obj = Resources.Load<GameObject>(path);
        //    // 新建一个池子
        //    ObjectPool pool = new ObjectPool(obj);
        //    mObjectPoolsDir.Add(pool.Name, pool);

        //    //Debug.Log($"对象池 “{pool.Name}” 创建成功");
        //}
        #endregion

        #region My
        private Dictionary<string, MyPool<GameObject>> mMyPoolsDir = new Dictionary<string, MyPool<GameObject>>();

        public GameObject Allocate<T>(string name) where T : IReusable
        {
            // 根据池子名称，如果没找到对应的池子，则创建新池子
            if (mMyPoolsDir.ContainsKey(name) == false)
                CreateNewObjPool(name);

            MyPool<GameObject> pool = mMyPoolsDir[name];
            GameObject obj = pool.Allocate();
            // 先激活，再调用 OnAllocate()
            obj.Show();
            obj.GetComponent<T>().OnAllocate();

            return obj;
        }

        public GameObject Allocate(string name)
        {
            // 根据池子名称，如果没找到对应的池子，则创建新池子
            if (mMyPoolsDir.ContainsKey(name) == false)
                CreateNewObjPool(name);

            MyPool<GameObject> pool = mMyPoolsDir[name];
            GameObject obj = pool.Allocate();

            obj.Show();

            return obj;
        }

        public void Recycle<T>(GameObject obj) where T : IReusable
        {
            MyPool<GameObject> pool = null;

            foreach (string pName in mMyPoolsDir.Keys)
            {
                if (pName == obj.name)
                {
                    pool = mMyPoolsDir[pName];
                    break;
                }
            }

            if (pool != null)
            {
                pool.Recycle(obj);
                obj.Hide();
                obj.GetComponent<T>().OnRecycle();
            }
            else
                Debug.Log("回收失败");
        }

        public void Recycle(GameObject obj)
        {
            MyPool<GameObject> pool = null;

            foreach (string pName in mMyPoolsDir.Keys)
            {
                if (pName == obj.name)
                {
                    pool = mMyPoolsDir[pName];
                    break;
                }
            }

            if (pool != null)
            {
                pool.Recycle(obj);
                obj.Hide();
            }
            else
                Debug.Log("回收失败");
        }

        public void RecycleAll()
        {
            foreach (MyPool<GameObject> p in mMyPoolsDir.Values)
                p.Clear();
        }

        private void CreateNewObjPool(string prefabName)
        {
            // 资源目录
            string path = ResourceDir + "/" + prefabName;

            // 新建一个池子
            MyPool<GameObject> pool = new MyPool<GameObject>(
                () => 
                {
                    GameObject obj = Resources.Load<GameObject>(path).Instantiate();
                    obj.name = prefabName;
                    return obj;
                },
                obj => obj.Hide());

            mMyPoolsDir.Add(prefabName, pool);

            //Debug.Log("对象池字典长度：" + mMyPoolsDir.Count);
        }
        #endregion
    }
}
