using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class StartUpController : Controller
    {
        public override void Execute(object data)
        {
            // ע�������¼�

            // ע�� Model
            RegisterModel(new GameModel());

            // ��ʼ��

        }
    }
}
