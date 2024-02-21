using BearRun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class Item : ReusableObject
    {
        public float RotateSpeed = 60f;
        private bool mIsBlock = false;
        protected Transform mEffectParentTrans;

        private void Awake()
        {
            mEffectParentTrans = GameObject.Find("Effects").transform;
        }

        private void Update()
        {
            transform.Rotate(new Vector3(0, RotateSpeed * Time.deltaTime, 0));
        }

        public override void OnAllocate()
        {

        }

        public override void OnRecycle()
        {
            // ��������
            transform.localEulerAngles = Vector3.zero;
            mIsBlock = false;
        }

        // ��ײ�����
        public virtual void HitPlayer(Vector3 pos)
        {
            mIsBlock = true;

            //TODO ����
            //Game.Instance.ObjectPool.Recycle(gameObject);
            Destroy(gameObject);
        }
    }
}
