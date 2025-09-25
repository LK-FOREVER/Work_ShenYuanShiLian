public abstract class Controller
{
    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="data"></param>
    public abstract void Excuse(object data = null);

    /// <summary>
    /// 获取模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetModel<T>() where T : Model
    {
        return Mvc.GetModel<T>();
    }

    /// <summary>
    /// 获取视图
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetView<T>() where T : View
    {
        return Mvc.GetView<T>();
    }

    public void RegisterController(string eventName, System.Type type)
    {
        Mvc.RegisterController(eventName, type);
    }

    public void RegisterView(View view)
    {
        Mvc.RegisterView(view);
    }

    public void RegisterModel(Model model)
    {
        Mvc.RegisterModel(model);
    }
}
