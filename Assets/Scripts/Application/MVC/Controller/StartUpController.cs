using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class StartUpController : Controller
    {
        public override void Execute(object data)
        {
            // ע������ Controller
            RegisterController(Consts.E_EnterScene, typeof(EnterSceneController));
            RegisterController(Consts.E_EndGame, typeof(EndGameController));
            RegisterController(Consts.E_PauseGame, typeof(PauseGameController));
            RegisterController(Consts.E_ResumeGame, typeof(ResumeGameController));
            RegisterController(Consts.E_ContinueGame, typeof(ContinueGameController));

            // ע�� Model
            RegisterModel(new GameModel());

            // ��ʼ��
            GameModel gameModel = GetModel<GameModel>();
            gameModel.Init();
        }
    }
}
