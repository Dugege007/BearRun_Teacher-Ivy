namespace BearRun
{
    public abstract class Model
    {
        /// <summary>
        /// 名字标识
        /// </summary>
        public abstract string Name { get; }

        // 发送事件
        protected void SendEvent(string eventName, object data = null)
        {
            MVC.SendEvent(eventName, data);
        }
    }
}
