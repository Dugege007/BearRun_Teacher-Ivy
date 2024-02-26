using UnityEngine;
using QFramework;

namespace BearRun
{
    internal class EnterSceneController : Controller
    {
        public override void Execute(object data)
        {
            SceneArgs eSceneArgs = data as SceneArgs;

            switch (eSceneArgs.SceneIndex)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    RegisterView(GameObject.FindWithTag(Tags.Player).GetComponent<PlayerMove>());
                    RegisterView(GameObject.FindWithTag(Tags.Player).GetComponent<PlayerAnim>());
                    RegisterView(GameObject.Find("Canvas").transform.Find("UIBoard").GetComponent<UIBoard>());
                    RegisterView(GameObject.Find("Canvas").transform.Find("UIPause").GetComponent<UIPause>());
                    RegisterView(GameObject.Find("Canvas").transform.Find("UIResume").GetComponent<UIResume>());
                    RegisterView(GameObject.Find("Canvas").transform.Find("UIGameOver").GetComponent<UIGameOver>());
                    RegisterView(GameObject.Find("Canvas").transform.Find("UIFinalScore").GetComponent<UIFinalScore>());

                    UIBoard uIBoard = GetView<UIBoard>();
                    UIResume uIResume = GetView<UIResume>();
                    UIGameOver uiGameOver = GetView<UIGameOver>();
                    UIFinalScore uiFinalScore = GetView<UIFinalScore>();

                    uIBoard.Show();
                    uIResume.Hide();
                    uiGameOver.Hide();
                    uiFinalScore.Hide();
                    break;
                default:
                    break;
            }
        }
    }
}
