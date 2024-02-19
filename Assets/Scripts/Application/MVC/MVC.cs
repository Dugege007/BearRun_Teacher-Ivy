using System;
using System.Collections.Generic;

namespace BearRun
{
    public static class MVC
    {
        // 资源
        // 数据集（名字，数据）
        public static Dictionary<string, Model> Models = new Dictionary<string, Model>();
        // 视图集（名字，视图）
        public static Dictionary<string, View> Views = new Dictionary<string, View>();
        // 命令集（事件名，类型）
        public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();

        // 注册 View
        public static void RegisterView(View view)
        {
            Views[view.Name] = view;
        }

        // 注册 Model
        public static void RegisterModel(Model model)
        {
            Models[model.Name] = model;
        }

        // 注册 Controller
        public static void RegisterController(string eventName, Type controllerType)
        {
            CommandMap[eventName] = controllerType;
        }

        // 获取 Model
        public static T GetModel<T>() where T : Model
        {
            foreach (var model in Models)
                if (model is T)
                    return model as T;

            return null;
        }

        // 获取 View
        public static T GetView<T>() where T : View
        {
            foreach (var view in Views)
                if (view is T)
                    return view as T;

            return null;
        }
    }
}
