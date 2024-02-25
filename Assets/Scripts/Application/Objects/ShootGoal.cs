using UnityEngine;
using QFramework;

namespace BearRun
{
    public class ShootGoal : ReusableObject
    {
        public float Speed = 10f;
        public bool IsFly = false;

        public Animation GoalKeeper;
        public Animation Door;

        private Transform mEffectParentTrans;

        private void Awake()
        {
            mEffectParentTrans = GameObject.Find("Effects").transform;
        }

        private void Update()
        {
            if (IsFly)
                GoalKeeper.transform.parent.parent.parent.transform.position += Time.deltaTime * new Vector3(0, Speed, Speed);
        }

        public override void OnAllocate()
        {
            GoalKeeper.Play("standard");
        }

        public override void OnRecycle()
        {
            GoalKeeper.transform.parent.parent.parent.Show();
            GoalKeeper.transform.parent.parent.parent.transform.localPosition = new Vector3(0, 0, -1.5f);
            IsFly = false;
        }

        // 进球了
        public void ShootAGoal()
        {
            GoalKeeper.transform.parent.parent.parent.Hide();
        }

        // 撞飞
        public void HitGoalKeeper(Vector3 pos)
        {
            // 生成特效
            Game.Instance.PoolManager.Allocate<Effect>("FX_ZhuangJi")
                .Position(pos + Vector3.up * 0.5f)
                .Parent(mEffectParentTrans);

            // 动画
            GoalKeeper.Play("fly");

            IsFly = true;
        }
    }
}
