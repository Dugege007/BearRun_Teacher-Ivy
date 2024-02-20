using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BearRun
{
    internal class EnterSceneController : Controller
    {
        public override void Execute(object data)
        {
            SceneArgs e = data as SceneArgs;
            switch (e.SceneIndex)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    RegisterView(GameObject.FindWithTag(Tags.Player).GetComponent<PlayerMove>());
                    break;
                default:
                    break;
            }
        }
    }
}
