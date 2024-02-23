using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BearRun
{
    public class UIResume : View
    {
        public Image CountTime;
        public Sprite[] CountSprite;

        public override string Name => Consts.V_UIResume;

        private void OnEnable()
        {
            StartCoroutine(StartCountCoroutine());
        }

        public override void HandleEvent(string eventName, object data)
        {
        }

        private IEnumerator StartCountCoroutine()
        {
            int i = 2;
            while (i >= 0)
            {
                CountTime.sprite = CountSprite[i];
                CountTime.SetNativeSize();
                i--;
                yield return new WaitForSeconds(1);
            }

            SendEvent(Consts.E_ContinueGame);
        }
    }
}
