using System;
using System.Collections.Generic;

public static class EventTriggerExt
{
    public static void TriggerEvent(this object sender, string eventName)
    {
        EventManager.Instance.TriggerEvent(eventName, sender);
    }
    public static void TriggerEvent(this object sender, string eventName, EventArgs args)
    {
        EventManager.Instance.TriggerEvent(eventName, sender, args);
    }
}

public class EventManager : SingletonBase<EventManager>
{
    private readonly Dictionary<string, EventHandler> handlerDic = new();
    public void AddListener(string eventName, EventHandler handler)
    {
        if (handlerDic.ContainsKey(eventName))
            handlerDic[eventName] += handler;
        else
            handlerDic.Add(eventName, handler);
    }
    public void RemoveListener(string eventName, EventHandler handler)
    {
        if (handlerDic.ContainsKey(eventName))
            handlerDic[eventName] -= handler;
    }
    public void TriggerEvent(string eventName, object sender)
    {
        if (handlerDic.ContainsKey(eventName))
            handlerDic[eventName]?.Invoke(sender, EventArgs.Empty);
    }
    public void TriggerEvent(string eventName, object sender, EventArgs args)
    {
        if (handlerDic.ContainsKey(eventName))
            handlerDic[eventName]?.Invoke(sender, args);
    }
    public void Clear()
    {
        handlerDic.Clear();
    }
}
