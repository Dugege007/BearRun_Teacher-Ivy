using QFramework;

namespace BearRun
{
    public class ResumeGameController : Controller
    {
        public override void Execute(object data)
        {
            UIPause uiPause = GetView<UIPause>();
            UIResume uiResume = GetView<UIResume>();

            uiPause.Hide();
            uiResume.Show();
        }
    }
}
