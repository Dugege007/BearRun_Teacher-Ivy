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
        // ȫ�ַ���
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

            // ��ʼ��
            // ע�� E_StartUp �¼������� StartUpController ��� View �� ���� Controller ��ע
            RegisterController(Consts.E_StartUp, typeof(StartUpController));

            // ��Ϸ����
            SendEvent(Consts.E_StartUp);

            // ��ת����
            Game.Instance.LoadLevel(4);
        }

        public void LoadLevel(int level)
        {
            // �����˳������¼�
            SceneArgs eSceneArgs = new()
            {
                // ��ȡ��ǰ��������ֵ
                SceneIndex = SceneManager.GetActiveScene().buildIndex
            };
            SendEvent(Consts.E_ExitScene, eSceneArgs);
            Debug.Log("�ѷ��� " + Consts.E_ExitScene + " �¼����뿪������" + eSceneArgs.SceneIndex);

            // �����³���
            SceneManager.LoadScene(level, LoadSceneMode.Single);
        }

        // �³���������ɺ����
        private void OnLevelWasLoaded(int level)
        {
            // ���ͼ����³����¼�
            SceneArgs eSceneArgs = new()
            {
                // ��ȡ��ǰ��������ֵ
                SceneIndex = level
            };

            SendEvent(Consts.E_EnterScene, eSceneArgs);
            Debug.Log("�ѷ��� " + Consts.E_EnterScene + " �¼�����ǰ����Ϊ��" + level);
        }

        // ע�� Controller
        private void RegisterController(string eventName, Type controllerType)
        {
            MVC.RegisterController(eventName, controllerType);
        }

        // �����¼�
        private void SendEvent(string eventName, object data = null)
        {
            MVC.SendEvent(eventName, data);
        }
    }
}
