using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class SubPool
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

        // 父物体位置信息
        private Transform mParentTrans;

        public SubPool(Transform parentTrans, GameObject obj)
        {
            mPrefab = obj;
            mParentTrans = parentTrans;
        }

        /// <summary>
        /// 取出物体
        /// </summary>
        /// <returns></returns>
        public GameObject Spawn()
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
                obj.transform.parent = mParentTrans;
                mObjects.Add(obj);
            }

            obj.SetActive(true);
            obj.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);

            return obj;
        }

        /// <summary>
        /// 回收物体
        /// </summary>
        /// <param name="obj"></param>
        public void UnSpawn(GameObject obj)
        {
            if (Contain(obj))
            {
                obj.SendMessage("OnUnSpawn", SendMessageOptions.DontRequireReceiver);
                obj.SetActive(false);
            }
        }

        /// <summary>
        /// 回收所有物体
        /// </summary>
        public void UnSpawnAll()
        {
            foreach (var obj in mObjects)
            {
                if (obj.activeSelf)
                {
                    UnSpawn(obj);
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
