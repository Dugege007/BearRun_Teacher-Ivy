using UnityEngine;

namespace BearRun
{
    internal class EnterSceneController : Controller
    {
        public override void Execute(object data)
        {
            SceneArgs eSceneArgs = data as SceneArgs;

            if (eSceneArgs != null)
            {
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
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
