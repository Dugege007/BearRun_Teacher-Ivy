using QFramework;

namespace BearRun
{
    public class GameModel : Model
    {
        #region ����
        #endregion

        #region �¼�
        #endregion

        #region �ֶ�
        public BindableProperty<bool> IsPlaying = new(true);
        public BindableProperty<bool> IsPause = new(false);
        public BindableProperty<float> SkillTime = new(5f);

        public BindableProperty<int> Score = new(0);
        public BindableProperty<int> Coin = new(0);
        public BindableProperty<int> Distance = new(0);
        public BindableProperty<float> GameTime = new(50f);
        public BindableProperty<float> AddTime = new(10f);

        #endregion

        #region ����
        public override string Name => Consts.M_GameModel;
        #endregion

        #region Unity�ص�
        #endregion

        #region �¼��ص�
        #endregion

        #region ����
        #endregion

        #region ��������
        #endregion
    }
}
