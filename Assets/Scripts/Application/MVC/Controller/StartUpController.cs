using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class StartUpController : Controller
    {
        public override void Execute(object data)
        {
            // 注册其他事件

            // 注册 Model
            RegisterModel(new GameModel());

            // 初始化

        }
    }
}
