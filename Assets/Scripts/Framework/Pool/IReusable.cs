namespace BearRun
{
    public interface IReusable
    {
        // ȡ��ʱ����
        void OnAllocate();
        // ����ʱ����
        void OnRecycle();
    }
}
