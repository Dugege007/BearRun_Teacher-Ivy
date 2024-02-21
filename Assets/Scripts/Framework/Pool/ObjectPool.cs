using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class ObjectPool
    {
        // 集合
        private List<GameObject> mObjects = new List<GameObject>();

        // 预设
        private GameObject mPrefab;

        // 名字
        //private string mName;
        public string Name
        {
            get { return mPrefab.name; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="obj">预制体</param>
        public ObjectPool(GameObject obj)
        {
            mPrefab = obj;
        }

        /// <summary>
        /// 取出物体
        /// </summary>
        /// <returns></returns>
        public GameObject Allocate()
        {
            GameObject obj = null;

            foreach (var o in mObjects)
            {
                if (o.activeSelf == false)
                {
                    obj = o; break;
                }
            }

            if (obj == null)
            {
                obj = Object.Instantiate(mPrefab);
                mObjects.Add(obj);
            }

            obj.SendMessage("OnAllocate", SendMessageOptions.DontRequireReceiver);
            obj.SetActive(true);

            return obj;
        }

        /// <summary>
        /// 回收物体
        /// </summary>
        /// <param name="obj"></param>
        public bool Recycle(GameObject obj)
        {
            if (Contain(obj))
            {
                obj.SendMessage("OnRecycle", SendMessageOptions.DontRequireReceiver);
                obj.SetActive(false);

                return true;
            }

            return false;
        }

        /// <summary>
        /// 回收所有物体
        /// </summary>
        public void RecycleAll()
        {
            foreach (var obj in mObjects)
            {
                if (obj.activeSelf)
                {
                    Recycle(obj);
                }
            }
        }

        /// <summary>
        /// 判断是否包含物体
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Contain(GameObject obj)
        {
            return mObjects.Contains(obj);
        }
    }
}
