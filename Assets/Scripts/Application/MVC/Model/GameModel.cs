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

        public BindableProperty<int> Coin = new(0);
        public BindableProperty<int> Distance = new(0);

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
