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
    }
}
