using UnityEngine;
using UnityEngine.SceneManagement;

namespace BearRun
{
    [RequireComponent(typeof(ObjectPool))]
    [RequireComponent(typeof(Sound))]
    [RequireComponent(typeof(StaticData))]
    public class Game : MonoSingleton<Game>
    {
        // 全局访问
        [HideInInspector]
        public ObjectPool ObjectPool;

        [HideInInspector]
        public Sound Sound;

        [HideInInspector]
        public StaticData StaticData;

        private void Start()
        {
            ObjectPool = ObjectPool.Instance;
            Sound = Sound.Instance;
            StaticData = StaticData.Instance;

            DontDestroyOnLoad(gameObject);

            // 游戏启动
        }

        private void LoadLevel(int level)
        {
            // 发送退出场景事件
            SceneArgs eSceneArgs = new SceneArgs();
            // 获取当前场景索引值
            eSceneArgs.SceneIndex = SceneManager.GetActiveScene().buildIndex;
            SendEvent(Consts.EventExitScene, eSceneArgs);
            Debug.Log("已发送 " + Consts.EventExitScene + " 事件");

            // 加载新场景
            SceneManager.LoadScene(level, LoadSceneMode.Single);
        }

        // 新场景加载完成后调用
        private void OnLevelWasLoaded(int level)
        {
            // 发送加载新场景事件
            SceneArgs eSceneArgs = new SceneArgs();
            // 获取当前场景索引值
            eSceneArgs.SceneIndex = level;

            SendEvent(Consts.EventEnterScene, level);
            Debug.Log("已发送 " + Consts.EventEnterScene + " 事件，当前场景为：" + level);
        }

        // 发送事件
        private void SendEvent(string eventName, object data = null)
        {
            MVC.SendEvent(eventName, data);
        }
    }
}
