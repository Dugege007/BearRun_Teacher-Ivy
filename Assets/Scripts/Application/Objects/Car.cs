using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class Car : Obstacles
    {
        public bool CanMove = false;
        public float Speed = 10f;
        private bool mIsBlock = false;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            if (mIsBlock && CanMove)
            {
                transform.Translate(-transform.forward * Speed * Time.deltaTime);
            }
        }

        public override void OnAllocate()
        {
            base.OnAllocate();
        }

        public override void OnRecycle()
        {
            mIsBlock = false;
            base.OnRecycle();
        }

        public override void HitPlayer(Vector3 pos)
        {
            base.HitPlayer(pos);
        }

        // Åö×²µ½´¥·¢ÇøÓò
        public void HitTrigger()
        {
            mIsBlock = true;
        }
    }
}
