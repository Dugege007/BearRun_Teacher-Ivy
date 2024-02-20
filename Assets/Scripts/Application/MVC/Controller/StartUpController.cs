using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class StartUpController : Controller
    {
        public override void Execute(object data)
        {
            // 注册其他 Controller
            RegisterController(Consts.E_EnterScene, typeof(EnterSceneController));
            RegisterController(Consts.E_EndGame, typeof(EndGameController));

            // 注册 Model
            RegisterModel(new GameModel());

            // 初始化

        }
    }
}
