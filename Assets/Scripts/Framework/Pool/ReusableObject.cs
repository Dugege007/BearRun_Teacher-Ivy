using UnityEngine;

namespace BearRun
{
    public abstract class ReusableObject : MonoBehaviour, IReusable
    {
        public abstract void OnAllocate();

        public abstract void OnRecycle();
    }
}
