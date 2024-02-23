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
            // 防止重复注册
            if (Views.ContainsKey(view.Name))
                Views.Remove(view.Name);

            view.RegisterAttentionEvent();
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
            foreach (var model in Models.Values)
                if (model is T)
                    return model as T;

            return null;
        }

        // 获取 View
        public static T GetView<T>() where T : View
        {
            foreach (var view in Views.Values)
                if (view is T)
                    return view as T;

            return null;
        }

        // 发送事件
        public static void SendEvent(string eventName, object data = null)
        {
            // Controller 去执行
            if (CommandMap.ContainsKey(eventName))
            {
                Type t = CommandMap[eventName];
                // 控制器生成
                // 通过反射类 Activator 创建 Controller 实例
                Controller c = Activator.CreateInstance(t) as Controller;
                c.Execute(data);
            }

            // View 处理
            foreach (var v in Views.Values)
            {
                if (v.AttentionList.Contains(eventName))
                {
                    // 执行
                    v.HandleEvent(eventName, data);
                }
            }
        }

    }
}
