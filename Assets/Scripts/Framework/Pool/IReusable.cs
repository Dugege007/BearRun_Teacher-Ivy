using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public interface IReusable
    {
        // ȡ��ʱ����
        void OnSpawn();
        // ����ʱ����
        void OnUnSpawn();
    }
}
