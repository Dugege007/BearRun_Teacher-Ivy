using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        // ��ԴĿ¼
        public string ResourceDir = "";

        #region BearRun
        //// �������еĳ���
        //private Dictionary<string, ObjectPool> mObjectPoolsDir = new Dictionary<string, ObjectPool>();

        ///// <summary>
        ///// ȡ����
        ///// </summary>
        ///// <param name="name">��������</param>
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
        ///// ��������
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
        ///// ������������
        ///// </summary>
        //public void RecycleAll()
        //{
        //    foreach (ObjectPool p in mObjectPoolsDir.Values)
        //        p.RecycleAll();
        //}

        //// �½�һ������
        //private void RegisterNewPool(string prefabName)
        //{
        //    // ��ԴĿ¼
        //    string path = ResourceDir + "/" + prefabName;
        //    // ����Ԥ������Դ
        //    GameObject obj = Resources.Load<GameObject>(path);
        //    // �½�һ������
        //    ObjectPool pool = new ObjectPool(obj);
        //    mObjectPoolsDir.Add(pool.Name, pool);

        //    //Debug.Log($"����� ��{pool.Name}�� �����ɹ�");
        //}
        #endregion

        #region My
        private Dictionary<string, MyPool<GameObject>> mMyPoolsDir = new Dictionary<string, MyPool<GameObject>>();

        public GameObject Allocate<T>(string name) where T : IReusable
        {
            // ���ݳ������ƣ����û�ҵ���Ӧ�ĳ��ӣ��򴴽��³���
            if (mMyPoolsDir.ContainsKey(name) == false)
                CreateNewObjPool(name);

            MyPool<GameObject> pool = mMyPoolsDir[name];
            GameObject obj = pool.Allocate();
            // �ȼ���ٵ��� OnAllocate()
            obj.Show();
            obj.GetComponent<T>().OnAllocate();

            return obj;
        }

        public GameObject Allocate(string name)
        {
            // ���ݳ������ƣ����û�ҵ���Ӧ�ĳ��ӣ��򴴽��³���
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
                Debug.Log("����ʧ��");
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
                Debug.Log("����ʧ��");
        }

        public void RecycleAll()
        {
            foreach (MyPool<GameObject> p in mMyPoolsDir.Values)
                p.Clear();
        }

        private void CreateNewObjPool(string prefabName)
        {
            // ��ԴĿ¼
            string path = ResourceDir + "/" + prefabName;

            // �½�һ������
            MyPool<GameObject> pool = new MyPool<GameObject>(
                () => 
                {
                    GameObject obj = Resources.Load<GameObject>(path).Instantiate();
                    obj.name = prefabName;
                    return obj;
                },
                obj => obj.Hide());

            mMyPoolsDir.Add(prefabName, pool);

            //Debug.Log("������ֵ䳤�ȣ�" + mMyPoolsDir.Count);
        }
        #endregion
    }
}
