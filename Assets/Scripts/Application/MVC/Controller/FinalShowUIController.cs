using QFramework;

namespace BearRun
{
    public class FinalShowUIController : Controller
    {
        public override void Execute(object data)
        {
            UIBoard uIBoard = GetView<UIBoard>();
            UIGameOver uiGameOver = GetView<UIGameOver>();
            UIFinalScore uiFinalScore = GetView<UIFinalScore>();

            uIBoard.Hide();
            uiGameOver.Hide();
            uiFinalScore.Show();
        }
    }
}
