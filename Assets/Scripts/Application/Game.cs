using UnityEngine;
using UnityEngine.SceneManagement;

namespace BearRun
{
    [RequireComponent(typeof(ObjectPool))]
    [RequireComponent(typeof(Sound))]
    [RequireComponent(typeof(StaticData))]
    public class Game : MonoSingleton<Game>
    {
        // ȫ�ַ���
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

            // ��Ϸ����
        }

        private void LoadLevel(int level)
        {
            // �����˳������¼�
            SceneArgs eSceneArgs = new SceneArgs();
            // ��ȡ��ǰ��������ֵ
            eSceneArgs.SceneIndex = SceneManager.GetActiveScene().buildIndex;
            SendEvent(Consts.EventExitScene, eSceneArgs);
            Debug.Log("�ѷ��� " + Consts.EventExitScene + " �¼�");

            // �����³���
            SceneManager.LoadScene(level, LoadSceneMode.Single);
        }

        // �³���������ɺ����
        private void OnLevelWasLoaded(int level)
        {
            // ���ͼ����³����¼�
            SceneArgs eSceneArgs = new SceneArgs();
            // ��ȡ��ǰ��������ֵ
            eSceneArgs.SceneIndex = level;

            SendEvent(Consts.EventEnterScene, level);
            Debug.Log("�ѷ��� " + Consts.EventEnterScene + " �¼�����ǰ����Ϊ��" + level);
        }

        // �����¼�
        private void SendEvent(string eventName, object data = null)
        {
            MVC.SendEvent(eventName, data);
        }
    }
}
