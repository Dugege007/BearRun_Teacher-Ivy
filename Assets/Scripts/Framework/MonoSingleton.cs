using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T mInstance;
        public static T Instance => mInstance;

        protected virtual void Awake()
        {
            mInstance = this as T;
        }
    }
}
