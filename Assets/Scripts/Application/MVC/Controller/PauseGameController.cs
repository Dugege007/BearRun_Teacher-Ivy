using UnityEngine;
using QFramework;

namespace BearRun
{
    public class PauseGameController : Controller
    {
        public override void Execute(object data)
        {
            UIPause uiPause = GetView<UIPause>();
            uiPause.Show();

            GameModel gameModel = GetModel<GameModel>();
            gameModel.IsPause.Value = true;
        }
    }
}
