using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class Sound : MonoSingleton<Sound>
    {
        public string ResourcesDir = "";

        private AudioSource mBGM;
        private AudioSource mSFX;

        protected override void Awake()
        {
            base.Awake();

            mBGM = gameObject.AddComponent<AudioSource>();
            mBGM.playOnAwake = false;
            mBGM.loop = true;

            mSFX = gameObject.AddComponent<AudioSource>();
            mSFX.playOnAwake = false;
            mSFX.loop = false;
        }

        /// <summary>
        /// ���ű�������
        /// </summary>
        /// <param name="audioName"></param>
        public void PlayBGM(string audioName)
        {
            string oldName;
            if (mBGM.clip == null)
                oldName = "";
            else
                oldName = mBGM.clip.name;

            if (oldName != audioName)
            {
                string path = ResourcesDir + "/" + audioName;
                // ������Դ
                AudioClip clip = Resources.Load<AudioClip>(path);
                // ����
                if (clip != null)
                {
                    mBGM.clip = clip;
                    mBGM.Play();
                }
            }
        }

        /// <summary>
        /// ������Ч
        /// </summary>
        /// <param name="audioName"></param>
        public void PlaySFX(string audioName)
        {
            string path = ResourcesDir + "/" + audioName;
            // ������Դ
            AudioClip clip = Resources.Load<AudioClip>(path);

            mSFX.PlayOneShot(clip);
        }
    }
}
