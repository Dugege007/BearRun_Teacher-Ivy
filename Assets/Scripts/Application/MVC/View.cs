using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public abstract class View : MonoBehaviour
    {
        /// <summary>
        /// 名字标识
        /// </summary>
        public abstract string Name { get; }

        // 关心事件的列表
        [HideInInspector]
        public List<string> AttentionList = new List<string>();

        public virtual void RegisterAttentionEvent()
        {

        }

        // 处理事件
        public abstract void HandleEvent(string eventName, object data);

        // 发送事件
        protected void SendEvent(string eventName, object data = null)
        {
            MVC.SendEvent(eventName, data);
        }

        // 获取模型
        protected T GetModel<T>() where T : Model
        {
            return MVC.GetModel<T>() as T;
        }
    }
}
