using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public abstract class View : MonoBehaviour
    {
        /// <summary>
        /// Ãû×Ö±êÊ¶
        /// </summary>
        public abstract string Name { get; }

        [HideInInspector]
        public List<string> AttentionList = new List<string>();

        public abstract void HandleEvent(string eventName, object data);
    }
}
