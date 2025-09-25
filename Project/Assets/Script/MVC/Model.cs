public abstract class Model
{
    public abstract string Name { get; }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="name">事件名称</param>
    /// <param name="data">事件参数</param>
    public virtual void SendEvent(string name, object data = null)
    {
        Mvc.SendEvent(name, data);
    }
}
