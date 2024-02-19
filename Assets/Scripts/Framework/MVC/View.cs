using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public abstract class View : MonoBehaviour
    {
        /// <summary>
        /// ���ֱ�ʶ
        /// </summary>
        public abstract string Name { get; }

        // �����¼����б�
        [HideInInspector]
        public List<string> AttentionList = new List<string>();

        public virtual void RegisterAttentionEvent()
        {

        }

        // �����¼�
        public abstract void HandleEvent(string eventName, object data);

        // �����¼�
        protected void SendEvent(string eventName, object data = null)
        {
            MVC.SendEvent(eventName, data);
        }

        // ��ȡģ��
        protected T GetModel<T>() where T : Model
        {
            return MVC.GetModel<T>() as T;
        }
    }
}
