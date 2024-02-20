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

            // ע�� Model
            RegisterModel(new GameModel());

            // ��ʼ��

        }
    }
}
