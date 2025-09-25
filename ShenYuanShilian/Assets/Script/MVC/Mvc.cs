using System;
using System.Collections.Generic;

public static class Mvc
{
    public static Dictionary<string, View> views = new();
    public static Dictionary<string, Model> models = new();
    //通过反射动态创建控制器
    public static Dictionary<string, Type> controllers = new();

    public static void RegisterView(View view)
    {
        if (views.ContainsKey(view.Name))
        {
            views.Remove(view.Name);
        }
        view.RegisterViewEvent();
        views[view.Name] = view;
    }

    public static void RegisterModel(Model model)
    {
        models[model.Name] = model;
    }

    public static void RegisterController(string eventName, Type type)
    {
        controllers[eventName] = type;
    }

    public static T GetModel<T>() where T : Model
    {
        foreach (var item in models.Values)
        {
            if (item is T)
                return (T)item;
        }
        return null;
    }

    public static T GetView<T>() where T : View
    {
        View view = null;
        foreach (var item in views.Values)
        {
            if (item is T)
                view = item;
        }
        if (view != null)
        {
            return (T)view;
        }
        return null;
    }

    public static void SendEvent(string eventName, object data = null)
    {
        //控制器的事件查询
        if (controllers.ContainsKey(eventName))
        {
            Type type = controllers[eventName];
            Controller controller = (Controller)Activator.CreateInstance(type);
            controller.Excuse(data);
        }

        //视图关心事件的处理
        foreach (var item in views.Values)
        {
            if (item.ContainEvent(eventName))
            {
                item.HandleEvent(data);
            }
        }
    }
}
