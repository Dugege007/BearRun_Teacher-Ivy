using UnityEngine;

namespace BearRun
{
    public class HitItemController : Controller
    {
        public override void Execute(object data)
        {
            GameModel gameModel = GetModel<GameModel>();
            PlayerMove player = GetView<PlayerMove>();
            UIBoard uiBoard = GetView<UIBoard>();

            ItemArgs eItemArgs = data as ItemArgs;
            if (eItemArgs != null)
            {
                switch (eItemArgs.SkillType)
                {
                    case SkillType.Magnet:
                        player.HitMagnet();
                        uiBoard.ShowMagnetInfo();
                        gameModel.MagnetCount.Value = Mathf.Max(gameModel.MagnetCount.Value - eItemArgs.SkillCount, 0);
                        break;

                    case SkillType.Multiply:
                        player.HitMultiply();
                        uiBoard.ShowMultiplyInfo();
                        gameModel.MultiplyCount.Value = Mathf.Max(gameModel.MultiplyCount.Value - eItemArgs.SkillCount, 0);
                        break;

                    case SkillType.Invincible:
                        player.HitInvincible();
                        uiBoard.ShowInvincibleInfo();
                        gameModel.InvincibleCount.Value = Mathf.Max(gameModel.InvincibleCount.Value - eItemArgs.SkillCount, 0);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
