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
        public GameObject Net;

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
            Door.Play("QiuMen_St");
            Net.Show();
            GoalKeeper.transform.parent.parent.parent.Show();
            GoalKeeper.transform.parent.parent.parent.transform.localPosition = new Vector3(0, 0, -1.5f);
        }

        public override void OnRecycle()
        {
            StopAllCoroutines();
            IsFly = false;
        }

        // 进球了
        public void ShootAGoal(float posX)
        {
            if (posX < 0)
                GoalKeeper.Play("left_flutter");
            else if (posX > 0)
                GoalKeeper.Play("right_flutter");
            else
                GoalKeeper.Play("flutter");

            ActionKit.Delay(0.6f, () =>
            {
                GoalKeeper.transform.parent.parent.parent.Hide();
            }).Start(this);
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

        public void HitDoor(int index)
        {
            // 动画
            switch (index)
            {
                case 0:
                    Door.Play("QiuMen_RR");
                    break;
                case 1:
                    Door.Play("QiuMen_St");
                    break;
                case 2:
                    Door.Play("QiuMen_LR");
                    break;
                default:
                    break;
            }

            // 球网消失
            Net.Hide();
        }
    }
}
