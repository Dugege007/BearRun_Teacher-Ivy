namespace BearRun
{
    public interface IReusable
    {
        // 取出时调用
        void OnAllocate();
        // 回收时调用
        void OnRecycle();
    }
}
