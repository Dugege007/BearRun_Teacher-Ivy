
namespace BearRun
{
    public class EndGameController : Controller
    {
        public override void Execute(object data)
        {
            GameModel gameModel = GetModel<GameModel>();

            gameModel.IsPlaying.Value = false;

            //TODO 显示游戏结束UI
        }
    }
}
