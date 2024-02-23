using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BearRun
{
    [RequireComponent(typeof(PoolManager))]
    [RequireComponent(typeof(Sound))]
    [RequireComponent(typeof(StaticData))]
    public class Game : MonoSingleton<Game>
    {
        // 全局访问
        [HideInInspector]
        public PoolManager PoolManager;

        [HideInInspector]
        public Sound Sound;

        [HideInInspector]
        public StaticData StaticData;

        private void Start()
        {
            PoolManager = PoolManager.Instance;
            Sound = Sound.Instance;
            StaticData = StaticData.Instance;

            DontDestroyOnLoad(gameObject);

            // 初始化
            // 注册 E_StartUp 事件，利用 StartUpController 完成 View 和 其他 Controller 的注
            RegisterController(Consts.E_StartUp, typeof(StartUpController));

            // 游戏启动
            SendEvent(Consts.E_StartUp);

            // 跳转场景
            Game.Instance.LoadLevel(4);
        }

        public void LoadLevel(int level)
        {
            // 发送退出场景事件
            SceneArgs eSceneArgs = new()
            {
                // 获取当前场景索引值
                SceneIndex = SceneManager.GetActiveScene().buildIndex
            };
            SendEvent(Consts.E_ExitScene, eSceneArgs);
            Debug.Log("已发送 " + Consts.E_ExitScene + " 事件，离开场景：" + eSceneArgs.SceneIndex);

            // 加载新场景
            SceneManager.LoadScene(level, LoadSceneMode.Single);
        }

        // 新场景加载完成后调用
        private void OnLevelWasLoaded(int level)
        {
            // 发送加载新场景事件
            SceneArgs eSceneArgs = new()
            {
                // 获取当前场景索引值
                SceneIndex = level
            };

            SendEvent(Consts.E_EnterScene, eSceneArgs);
            Debug.Log("已发送 " + Consts.E_EnterScene + " 事件，当前场景为：" + level);
        }

        // 注册 Controller
        private void RegisterController(string eventName, Type controllerType)
        {
            MVC.RegisterController(eventName, controllerType);
        }

        // 发送事件
        private void SendEvent(string eventName, object data = null)
        {
            MVC.SendEvent(eventName, data);
        }
    }
}
