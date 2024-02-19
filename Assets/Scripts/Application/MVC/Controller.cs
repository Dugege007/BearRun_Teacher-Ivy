using System;

namespace BearRun
{
    public abstract class Controller
    {
        // 执行事件
        public abstract void Execute(object data);

        // 获取模型
        protected T GetModel<T>() where T : Model
        {
            return MVC.GetModel<T>() as T;
        }

        // 获取视图
        protected T GetView<T>() where T : View
        {
            return MVC.GetView<T>() as T;
        }

        // 注册 Model
        protected void RegisterModel(Model model)
        {
            MVC.RegisterModel(model);
        }

        // 注册 View
        protected void RegisterView(View view)
        {
            MVC.RegisterView(view);
        }

        // 注册 Controller
        protected void RegisterController(string eventName, Type controllerType)
        {
            MVC.RegisterController(eventName, controllerType);
        }
    }
}
