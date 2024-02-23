using QFramework;

namespace BearRun
{
    public class ContinueGameController : Controller
    {
        public override void Execute(object data)
        {
            UIResume uiResume = GetView<UIResume>();
            uiResume.Hide();

            GameModel gameModel = GetModel<GameModel>();
            gameModel.IsPause.Value = false;
            gameModel.IsPlaying.Value = true;
        }
    }
}
