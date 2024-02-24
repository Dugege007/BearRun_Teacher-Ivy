using System;
using UnityEngine;

namespace BearRun
{
    public class PlayerAnim : View
    {
        private Animation anim;
        private Action PlayAnim;
        private GameModel mGameModel;

        public override string Name => Consts.V_PlayerAnim;


        private void Awake()
        {
            anim = GetComponent<Animation>();
            PlayAnim = PlayRun;
            mGameModel = GetModel<GameModel>();
        }

        private void Update()
        {
            if (PlayAnim != null)
            {
                if (mGameModel.GamePlaying())
                    PlayAnim();
                else
                    anim.Stop();
            }
        }

        public void AnimManager(InputDirection inputDir)
        {
            switch (inputDir)
            {
                case InputDirection.Null:
                    break;
                case InputDirection.Left:
                    PlayAnim = PlayLeft;
                    break;
                case InputDirection.Right:
                    PlayAnim = PlayRight;
                    break;
                case InputDirection.Up:
                    PlayAnim = PlayJump;
                    break;
                case InputDirection.Down:
                    PlayAnim = PlayRoll;
                    break;
                default:
                    break;
            }
        }

        private void PlayRun()
        {
            anim.Play("run");
        }

        private void PlayLeft()
        {
            anim.Play("left_jump");
            if (anim["left_jump"].normalizedTime > 0.95f)
                PlayAnim = PlayRun;
        }

        private void PlayRight()
        {
            anim.Play("right_jump");
            if (anim["right_jump"].normalizedTime > 0.95f)
                PlayAnim = PlayRun;
        }

        private void PlayRoll()
        {
            anim.Play("roll");
            if (anim["roll"].normalizedTime > 0.95f)
                PlayAnim = PlayRun;
        }

        private void PlayJump()
        {
            anim.Play("jump");
            if (anim["jump"].normalizedTime > 0.95f)
                PlayAnim = PlayRun;
        }

        public override void HandleEvent(string eventName, object data)
        {
            throw new NotImplementedException();
        }
    }
}
