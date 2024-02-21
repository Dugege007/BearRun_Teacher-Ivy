using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class MyPool<T> : Pool<T>
    {
        readonly Action<T> mResetMethod;

        // 预设
        private GameObject mPrefab;

        // 名字
        //private string mName;
        public string Name
        {
            get { return mPrefab.name; }
        }

        public MyPool(Func<T> factoryMethod, Action<T> resetMethod = null, int initCount = 0)
        {
            mFactory = new CustomObjectFactory<T>(factoryMethod);
            mResetMethod = resetMethod;

            for (var i = 0; i < initCount; i++)
            {
                mCacheStack.Push(mFactory.Create());
            }
        }

        public override T Allocate()
        {
            T obj = base.Allocate();
            return obj;
        }

        public override bool Recycle(T obj)
        {
            mResetMethod?.Invoke(obj);
            // 推到栈顶
            mCacheStack.Push(obj);
            return true;
        }

        /// <summary>
        /// 判断是否包含物体
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Contain(T obj)
        {
            return mCacheStack.Contains(obj);
        }
    }
}
